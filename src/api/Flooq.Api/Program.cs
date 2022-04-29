using System.Reflection;
using Flooq.Api.Domain;
using Flooq.Api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FlooqContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("FlooqDatabase")));
// builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<FlooqContext>();
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
        TokenUrl = new Uri("https://localhost:5001/connect/token"),
        Scopes = new Dictionary<string, string> { { "flooqapi", "API - full access" } }
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
        AuthorizationUrl = new Uri("https://localhost:5001/connect/authorize"),
        TokenUrl = new Uri("https://localhost:5001/connect/token"),
        Scopes = new Dictionary<string, string> { { "flooqapi", "API - full access" } }
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
            new[] { "flooqapi" }
        }
    });

  options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2-user" },
            },
            new[] { "flooqapi" }
        }
    });
});

builder.Services.AddScoped<IVersionService, VersionService>();
builder.Services.AddScoped<IDataFlowService, DataFlowService>();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
      options.Authority = "https://localhost:5001";
      // TODO: Add setting for authority
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateAudience = false
      };
    });

builder.Services.AddAuthorization(options =>
{
  options.AddPolicy("ApiScope", policy =>
  {
    policy.RequireAuthenticatedUser();
    policy.RequireClaim("scope", "flooqapi");
  });
});
builder.Services.AddScoped<ILinearizedGraphService, LinearizedGraphService>();

builder.Configuration.AddEnvironmentVariables();
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
