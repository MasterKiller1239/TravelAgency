using Microsoft.EntityFrameworkCore;
using TravelAgency.Data;
using TravelAgency.Services;
using TravelAgency.Services.Interfaces;
var builder = WebApplication.CreateBuilder(args);
Console.WriteLine("Choose data source: 1 - Firebase | 2 - SQL Server");
var choice = Console.ReadLine();

if (choice == "1")
{
    // Firebase
    var firebaseProjectId = "travelagency-6f8ba";
    var credentialsPath = Path.Combine(Directory.GetCurrentDirectory(), "Secrets", "serviceAccountKey.json");

    builder.Services.AddSingleton<IDataSource>(new FirestoreDataSource(firebaseProjectId, credentialsPath));
}
else if (choice == "2")
{
    // SQL
    builder.Services.AddDbContext<TravelAgencyContext>(options =>
        options.UseSqlServer("Server=localhost;Database=TravelAgencyDB;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=true;").EnableSensitiveDataLogging()
           .EnableDetailedErrors());

    builder.Services.AddScoped<IDataSource, SqlDataSource>();
}
else
{
    Console.WriteLine("Invalid choice. Exiting...");
    return;
}

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Trips}/{action=Index}/{id?}");

app.Run();
