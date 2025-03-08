using DepInfoCare.Data;
using DepInfoCare.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages()
    .AddRazorPagesOptions(options =>
    {
        options.Conventions.AddPageRoute("/Authentication/Login", "/login");
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
        var passwordHasher = new PasswordHasher<User>();
        var user = new User
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
