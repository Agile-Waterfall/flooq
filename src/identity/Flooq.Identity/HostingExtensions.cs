using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Serilog;
using IdentityServerHost;

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

    builder.Services.AddIdentityServer()
      .AddInMemoryIdentityResources(Config.IdentityResources)
      .AddInMemoryApiScopes(Config.ApiScopes)
      .AddInMemoryClients(Config.Clients)
      .AddTestUsers(TestUsers.Users);


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
