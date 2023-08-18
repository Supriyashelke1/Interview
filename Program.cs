using Interview.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IO;

void ConfigureServices(IServiceCollection services)
{
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conn= builder.Configuration.GetConnectionString("InterviewDbConnection");



    // Other services

    services.AddScoped<IAppointmentRepository, AppointmentRepository>();
    services.AddScoped<IBookingRepository, BookingRepository>();

    // ...


    services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Cookie expiration time
            options.LoginPath = "/Login"; // Path to the login page
            options.AccessDeniedPath = "/AccessDenied"; // Path to the access denied page
            options.SlidingExpiration = true;
        });


var connection = String.Empty;
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
    connection= builder.Configuration.GetConnectionString("InterviewDbConnection");
}
else
{
    connection = Environment.GetEnvironmentVariable("InterviewDbConnection");
}


builder.Services.AddDbContext<InterviewDbContext>(q => q.UseSqlServer(connection));
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
app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();

//app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.MapControllerRoute(
    name: "Appointment",
    pattern: "{controller=Appointment}/{action=Index}/{id?}");

app.MapGet("/Appointment", (InterviewDbContext context) =>
{
    return context.Appointments.ToList();
})
.WithName("GetAppointments")
.WithOpenApi();

app.MapPost("/Appointment", (Appointment appointment, InterviewDbContext context) =>
{
    context.Add(appointment);
    context.SaveChanges();
})
.WithName("CreateAppointment")
.WithOpenApi();

app.Run();
}