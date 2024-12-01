using Microsoft.EntityFrameworkCore;
using ReservationSystem.Areas.User.Services;
using ReservationSystem.Data;
using ReservationSystem.Data.Seed;
using MyReservationSystem.Services;
using System.Globalization;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Get the connection string for the database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Configure the DbContext with SQL Server
builder.Services.AddDbContext<ReservationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add controllers with views to the services container
builder.Services.AddControllersWithViews();

// Register the ReservationService
builder.Services.AddScoped<ReservationService>();

// Register MenuService with HttpClient
builder.Services.AddHttpClient<MenuService>();

builder.Logging.AddConsole(); // For logging to the console

// Register Automatic Reservation Status Update set Reservations to completed for past dates
builder.Services.AddHostedService<ReservationStatusUpdater>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ReservationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/User/Account/Login"; // This ensures users are redirected to the login page when they try to access a protected area.
});

// Set the default culture for the application
var cultureInfo = new CultureInfo("en-AU");
cultureInfo.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"; // Australian date format
cultureInfo.DateTimeFormat.ShortTimePattern = "HH:mm";      // 24-hour time format
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var app = builder.Build();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ReservationDbContext>();
    Sittings.Seed(context); // Call the seeder
}

// Configure the HTTP request pipeline for development and production
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    await RoleSeeder.SeedRoles(serviceProvider); // Seed roles
    await RoleSeeder.AssignRoles(serviceProvider); // Assign roles to admin user
}

// Enable areas routing
app.MapControllerRoute(
    name: "areaRoute",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"); // Default route for areas (Admin/User)

app.MapControllerRoute(
    name: "userDefault",
    pattern: "{area=User}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "menuDefault",
    pattern: "{controller=Menu}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "userAccountLogin",
    pattern: "{area=User}/Account/Login",
    defaults: new { controller = "Account", action = "Login" });

app.MapControllerRoute(
    name: "userAccountRegister",
    pattern: "{area=User}/Account/Register",
    defaults: new { controller = "Account", action = "Register" });

app.Run();