using System.Reflection;
using Flooq.Api.Domain;
using Flooq.Api.Metrics.Services;
using Flooq.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

var identityServerIssuer = builder.Configuration.GetValue<string>("IDENTITY_SERVER_ISSUER");

// Add services
builder.Services.AddDbContext<FlooqContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("FlooqDatabase")));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
  options.SwaggerDoc("v1", new() { Title = "Flooq API", Version = "v1" });

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

builder.Services.AddScoped<IVersionService, VersionService>();
builder.Services.AddScoped<IDataFlowService, DataFlowService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
      options.Authority = identityServerIssuer;
      options.RequireHttpsMetadata = false;
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateAudience = false
      };
    });

builder.Services.AddAuthorization(options =>
{
  options.AddPolicy("read", policy =>
  {
    policy.RequireAuthenticatedUser();
    policy.RequireClaim("scope", "read");
  });

  options.AddPolicy("write", policy =>
  {
    policy.RequireAuthenticatedUser();
    policy.RequireClaim("scope", "write");
  });
  
  options.AddPolicy("read_all", policy =>
  {
    policy.RequireAuthenticatedUser();
    policy.RequireClaim("scope", "read_all");
  });
});
builder.Services.AddScoped<ILinearizedGraphService, LinearizedGraphService>();
builder.Services.AddScoped<IDataFlowMetricsService, DataFlowMetricsService>();
builder.Services.AddScoped<ILinearizedGraphMetricsService, LinearizedGraphMetricsService>();
builder.Services.AddHealthChecks();

// Build app
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
  var db = scope.ServiceProvider.GetRequiredService<FlooqContext>();
  if (db.Database.IsRelational())
  {
    db.Database.Migrate();
  }
}

app.MapHealthChecks("/health");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseSwagger();
  app.UseSwaggerUI(c =>
  {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Flooq API v1");
  });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
  endpoints.MapMetrics();
});
app.MapControllers();
app.Run();

public partial class Program { }