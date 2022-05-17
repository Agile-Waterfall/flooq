using System.Reflection;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog;
using Flooq.Identity.Services;
using Flooq.Identity.Models;
using Flooq.Identity.Data;

namespace Flooq.Identity;

internal static class HostingExtensions
{
  private static void InitializeDatabase(IApplicationBuilder app)
  {
    using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
    {
      serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

      var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
      context.Database.Migrate();
      if (!context.Clients.Any())
      {
        foreach (var client in Config.Clients)
        {
          context.Clients.Add(client.ToEntity());
        }
        context.SaveChanges();
      }

      if (!context.IdentityResources.Any())
      {
        foreach (var resource in Config.IdentityResources)
        {
          context.IdentityResources.Add(resource.ToEntity());
        }
        context.SaveChanges();
      }

      if (!context.ApiScopes.Any())
      {
        foreach (var resource in Config.ApiScopes)
        {
          context.ApiScopes.Add(resource.ToEntity());
        }
        context.SaveChanges();
      }
    }
  }
  public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
  {
    var identityServerIssuer = builder.Configuration.GetValue<string>("IDENTITY_SERVER_ISSUER");
    var migrationsAssembly = typeof(Program).Assembly.GetName().Name;

    builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
    builder.Services.AddDbContext<FlooqIdentityContext>(options =>
      options.UseNpgsql(builder.Configuration.GetConnectionString("FlooqIdentityDatabase"), sql => sql.MigrationsAssembly(migrationsAssembly))
    );

    builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
      .AddEntityFrameworkStores<FlooqIdentityContext>()
      .AddDefaultTokenProviders();

    builder.Services.AddIdentityServer(options =>
      {
        options.Events.RaiseErrorEvents = true;
        options.Events.RaiseInformationEvents = true;
        options.Events.RaiseFailureEvents = true;
        options.Events.RaiseSuccessEvents = true;
        options.EmitStaticAudienceClaim = true;
      })
      .AddInMemoryIdentityResources(Config.IdentityResources)
      .AddInMemoryApiScopes(Config.ApiScopes)
      .AddInMemoryClients(Config.Clients)
      .AddAspNetIdentity<ApplicationUser>()
      .AddProfileService<FlooqProfileService>();

    builder.Services.AddCors(setup =>
    {
      setup.AddDefaultPolicy(policy =>
      {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.WithOrigins("http://localhost:3500", "http://localhost:3000", "http://localhost:8080", "https://editor-staging.flooq.io", "https://executor-staging.flooq.io", "https://api-staging.flooq.io", "https://editor.flooq.io", "https://executor.flooq.io");
        policy.AllowCredentials();
      });
    });

    builder.Services.AddAuthentication(options =>
      {
        options.DefaultScheme = IdentityServerConstants.DefaultCookieAuthenticationScheme;
      })
      .AddGitHub(options =>
      {
        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

        var githubClient = builder.Configuration.GetSection("Authentication:Github");

        options.ClientId = githubClient.GetValue<string>("ClientId");
        options.ClientSecret = githubClient.GetValue<string>("ClientSecret");
        options.CallbackPath = "/signin-github";
        options.Scope.Add("read:user");
      })
      .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
      {
        options.IncludeErrorDetails = true;
        options.Authority = identityServerIssuer;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateAudience = false,
          ValidTypes = new[] { "at+jwt" }
        };
      });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddControllers();
    builder.Services.AddSwaggerGen(options =>
    {
      options.SwaggerDoc("v1", new() { Title = "Flooq Identity", Version = "v1" });

      var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
      options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

      options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
      {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
          ClientCredentials = new OpenApiOAuthFlow
          {
            TokenUrl = new Uri(identityServerIssuer + "/connect/token"),
            Scopes = new Dictionary<string, string> { { "read_all", "Read All Access" } }
          },
        }
      });

      options.AddSecurityDefinition("oauth2-user", new OpenApiSecurityScheme
      {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
          AuthorizationCode = new OpenApiOAuthFlow
          {
            AuthorizationUrl = new Uri(identityServerIssuer + "/connect/authorize"),
            TokenUrl = new Uri(identityServerIssuer + "/connect/token"),
            Scopes = new Dictionary<string, string> { { "read", "Read Access" }, { "write", "Write Access" } }
          },
        }
      });

      options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" },
                },
                new[] { "read", "read_all" }
            }
        });

      options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2-user" },
                },
                new[] { "read", "write" }
            }
        });
    });
    builder.Services.AddAuthorization(options =>
    {
      options.AddPolicy("read", policy =>
      {
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "read");
      });

      options.AddPolicy("write", policy =>
      {
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "write");
      });

      options.AddPolicy("read_all", policy =>
      {
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "read_all");
      });
    });

    return builder.Build();
  }

  public static WebApplication ConfigurePipeline(this WebApplication app)
  {
    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
      app.UseDeveloperExceptionPage();
      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Flooq Identity v1");
      });
    }

    using (var scope = app.Services.CreateScope())
    {
      var db = scope.ServiceProvider.GetRequiredService<FlooqIdentityContext>();
      var usrMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
      if (db.Database.IsRelational())
      {
        db.Database.Migrate();
        SeedData.EnsureSeedData(db, usrMgr);
      }
    }

    app.UseStaticFiles();
    app.UseRouting();
    
    app.UseAuthentication();
    app.UseAuthorization();

    app.Use((context, next) =>
    {
      context.Request.Scheme = "https";
      return next();
    });

    app.UseIdentityServer();
    app.MapRazorPages().RequireAuthorization();
    app.MapControllers().RequireAuthorization();

    return app;
  }
}
