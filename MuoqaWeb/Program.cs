using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MuoqaWeb;
using MuoqaBD;

var builder = WebApplication.CreateBuilder(args);

Startup startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);


builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<MuoqaBDConf>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.WebHost.ConfigureKestrel((context, options) => //esto detecta solo si esta en desarrollo o en producci�n
{
    options.Configure(context.Configuration.GetSection("Kestrel"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
