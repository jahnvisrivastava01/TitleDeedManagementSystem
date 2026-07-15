using TitleDeedManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using TitleDeedManagementSystem.Repositories;
using TitleDeedManagementSystem.Services;
using Serilog;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using TitleDeedManagementSystem.Helpers;
using TitleDeedManagementSystem.Repositories.Interfaces;
using TitleDeedManagementSystem.Repositories.Implementations;
using TitleDeedManagementSystem.Services.Interfaces;
using TitleDeedManagementSystem.Services.Implementations;


Log.Logger = new LoggerConfiguration()
             .WriteTo.Console()
             .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
             .CreateLogger();


var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
        ));
builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMasterDataService, MasterDataService>();
builder.Services.AddScoped<PasswordHelper>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICollateralRepository, CollateralRepository>();
builder.Services.AddScoped<ITitleDeedRepository, TitleDeedRepository>();
builder.Services.AddScoped<IDataEntryService, DataEntryService>();
builder.Services.AddScoped<ITitleDeedEntryRepository, TitleDeedEntryRepository>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
      options.LoginPath = "/Auth/LoginBasic"; // Set the login path
      options.LogoutPath = "/Auth/Logout"; // Set the logout path
      options.AccessDeniedPath = "/Auth/AccessDenied"; // Set the access denied path
      options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Set the cookie expiration time
      options.SlidingExpiration = true; // Enable sliding expiration
    });

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=LoginBasic}/{id?}");

app.Run();
