using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using IdentityServerHost;
using IdentityServerHost.Models;
using IdentityServerAspNetIdentity.Data;
using IdentityServerAspNetIdentity;

namespace IdentityServer;

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
    var migrationsAssembly = typeof(Program).Assembly.GetName().Name;
    builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

    builder.Services.AddDbContext<FlooqIdentityContext>(options =>
      options.UseNpgsql(builder.Configuration.GetConnectionString("FlooqIdentityDatabase"), sql => sql.MigrationsAssembly(migrationsAssembly))
    );

    builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
      .AddEntityFrameworkStores<FlooqIdentityContext>()
      .AddDefaultTokenProviders();

    // builder.Services.AddIdentityServer()
    //     .AddConfigurationStore(options =>
    //     {
    //       options.ConfigureDbContext = b => b.UseNpgsql(builder.Configuration.GetConnectionString("FlooqIdentityDatabase"), sql => sql.MigrationsAssembly(migrationsAssembly));
    //     })
    //     .AddOperationalStore(options =>
    //     {
    //       options.ConfigureDbContext = b => b.UseNpgsql(builder.Configuration.GetConnectionString("FlooqIdentityDatabase"), sql => sql.MigrationsAssembly(migrationsAssembly));
    //     })
    //     .AddTestUsers(TestUsers.Users);

    builder.Services.AddIdentityServer(options =>
      {
        options.Events.RaiseErrorEvents = true;
        options.Events.RaiseInformationEvents = true;
        options.Events.RaiseFailureEvents = true;
        options.Events.RaiseSuccessEvents = true;

        // see https://docs.duendesoftware.com/identityserver/v5/fundamentals/resources/
        options.EmitStaticAudienceClaim = true;
      })
      .AddInMemoryIdentityResources(Config.IdentityResources)
      .AddInMemoryApiScopes(Config.ApiScopes)
      .AddInMemoryClients(Config.Clients)
      .AddAspNetIdentity<ApplicationUser>();


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
        });

    return builder.Build();
  }

  public static WebApplication ConfigurePipeline(this WebApplication app)
  {
    app.UseSerilogRequestLogging();
    if (app.Environment.IsDevelopment())
    {
      app.UseDeveloperExceptionPage();
    }
    // InitializeDatabase(app);

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

    app.Use((context, next) =>
    {
      context.Request.Scheme = "https";
      return next();
    });

    app.UseIdentityServer();
    app.UseAuthorization();
    app.MapRazorPages().RequireAuthorization();

    return app;
  }
}
