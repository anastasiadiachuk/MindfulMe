using Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MindfulMe.Middlewares;
using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProgramDbContext>(options =>
    options.UseMySql("Server=localhost;Database=mindfulme;User=root;Password=12Cjkysirj90;Port=3306;",
        new MySqlServerVersion(new Version(8, 0, 33))));
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IOfferService, OfferService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/login";
    });

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();