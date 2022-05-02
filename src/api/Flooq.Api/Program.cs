using System.Reflection;
using Flooq.Api.Domain;
using Flooq.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();
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
        TokenUrl = new Uri(Environment.GetEnvironmentVariable("IDENTITY_SERVER_ISSUER") + "/connect/token"),
        Scopes = new Dictionary<string, string> { { "read", "Read Access" } }
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
        AuthorizationUrl = new Uri(Environment.GetEnvironmentVariable("IDENTITY_SERVER_ISSUER") + "/connect/authorize"),
        TokenUrl = new Uri(Environment.GetEnvironmentVariable("IDENTITY_SERVER_ISSUER") + "/connect/token"),
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
            new[] { "read" }
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

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
      options.Authority = Environment.GetEnvironmentVariable("IDENTITY_SERVER_ISSUER");
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
});
builder.Services.AddScoped<ILinearizedGraphService, LinearizedGraphService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
  var db = scope.ServiceProvider.GetRequiredService<FlooqContext>();
  if (db.Database.IsRelational())
  {
    db.Database.Migrate();
  }
}

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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program { }
