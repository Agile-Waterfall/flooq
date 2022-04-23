using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Flooq.Authentication.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("FlooqDatabase");;

builder.Services.AddDbContext<FlooqAuthenticationContext>(options =>
    options.UseNpgsql(connectionString));;

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<FlooqAuthenticationContext>();;
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
  .AddEntityFrameworkStores<FlooqAuthenticationContext>();
builder.Services.AddRazorPages();

builder.Services.AddAuthentication(options => { })
  .AddGitHub(options =>
  {
    options.ClientId = "2ff1d7ecd82f0813741c";
    options.ClientSecret = "ec0597b6bb44488c1030095adb5abd92a6ff098d";
  });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseMigrationsEndPoint();
}
else
{
  app.UseExceptionHandler("/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();