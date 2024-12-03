using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using LinksApp.Data;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Configure database context
builder.Services.AddDbContext<LinkContext>(options =>
{
    options.UseMySql(
        Env.GetString("CONNECTION_STRING"),
        ServerVersion.AutoDetect(Env.GetString("CONNECTION_STRING"))
    );
});

// Configure session
builder.Services.AddSession(options => 
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Required for GDPR compliance
});

// Add controllers and views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Enable session middleware
app.UseSession();

// Enable authorization (if used)
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
