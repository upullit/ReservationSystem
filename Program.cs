using Microsoft.EntityFrameworkCore;
using ReservationSystem.Areas.User.Services;
using ReservationSystem.Data;
using MyReservationSystem.Services;
using System.Globalization; // Import the namespace for CultureInfo

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

// Register Automatic Reservation Status Update set Reservations to completed for past dates
builder.Services.AddHostedService<ReservationStatusUpdater>();


// Set the default culture for the application
var cultureInfo = new CultureInfo("en-AU");
cultureInfo.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"; // Australian date format
cultureInfo.DateTimeFormat.ShortTimePattern = "HH:mm";      // 24-hour time format
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var app = builder.Build();

// Seed the database
//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<ReservationDbContext>();
//    Sittings.Seed(context); // Call the seeder
//}

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
app.UseAuthorization();

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


app.Run();
