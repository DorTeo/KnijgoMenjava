using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using KnjigoMenjava.Data;
using KnijgoMenjava.Models;
using KnjigoMenjava.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("AzureContext")));


builder.Services.AddDefaultIdentity<Uporabnik>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddControllersWithViews();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();



app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
