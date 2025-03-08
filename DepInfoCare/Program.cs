using DepInfoCare.Data;
using DepInfoCare.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddRazorPages()
    .AddRazorPagesOptions(options =>
    {
        // Authentication
        options.Conventions.AddPageRoute("/Authentication/Login", "/login");
        options.Conventions.AddPageRoute("/Authentication/Logout", "/logout");

        // Facility
        options.Conventions.AddAreaPageRoute("Pages", "/Facility/Index", "/facility");
        options.Conventions.AddAreaPageRoute("Pages", "/Facility/Add", "/facility/add");
        options.Conventions.AddPageRoute("/Facility/Add", "/facility/edit/{id:int}");
        options.Conventions.AddPageRoute("/Facility/Delete", "/facility/delete/{id:int}");

        // Patients
        options.Conventions.AddPageRoute("/Patient/Index", "/facility/{facilityId:int}");
        options.Conventions.AddPageRoute("/Patient/Detail", "/facility/{facilityId:int}/{patientId:int}");
        options.Conventions.AddPageRoute("/Patient/Add", "/facility/{facilityId:int}/add");
        options.Conventions.AddPageRoute("/Patient/Add", "/facility/{facilityId:int}/edit/{patientId:int}");
        options.Conventions.AddPageRoute("/Patient/Delete", "/facility/{facilityId:int}/delete/{patientId:int}");
    });

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services
    .AddAuthentication("Cookies")
    .AddCookie("Cookies", config =>
    {
        config.Cookie.Name = "DepInfoCare.Cookie";
        config.LoginPath = "/login";
    });

builder.Services.AddDbContext<DepInfoCareDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DepInfoCare")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DepInfoCareDbContext>();
    dbContext.Database.EnsureCreated();

    if (!dbContext.Users.Any())
    {
        var passwordHasher = new PasswordHasher<UserModel>();
        var user = new UserModel
        {
            Username = "admin",
            Role = "Administrator"
        };
        user.PasswordHash = passwordHasher.HashPassword(user, "password");

        dbContext.Users.Add(user);
        dbContext.SaveChanges();
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapRazorPages();

app.Run();
