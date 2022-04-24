using System.Reflection;
using Flooq.Api.Domain;
using Flooq.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddDbContext<FlooqContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("FlooqDatabase")));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
  options.SwaggerDoc("v1", new() {Title = "Flooq API", Version = "v1"});
  
  var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
  options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddScoped<IVersionService, VersionService>();
builder.Services.AddScoped<IDataFlowService, DataFlowService>();
builder.Services.AddHealthChecks();

// Add configurations
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

app.MapHealthChecks("/health");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseSwagger();
  app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Flooq API v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program { }