using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using ScheduleAppFp.Models.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

 
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.User.RequireUniqueEmail = true;  
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

 
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
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
 
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    
    await SeedAdminRoles(roleManager, userManager);
}

app.Run();

 
async Task SeedAdminRoles(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
{
   
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        var adminRole = new IdentityRole("Admin");
        await roleManager.CreateAsync(adminRole);
    }

  
    if (!await roleManager.RoleExistsAsync("User"))
    {
        var userRole = new IdentityRole("User");
        await roleManager.CreateAsync(userRole);
    }

 
    var adminEmails = new List<string>
    {
        "charbelfares78@gmail.com",
        "johndoe@gmail.com",
 
    };
    foreach (var email in adminEmails)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user != null)
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }

   
    var usersWithoutRole = await userManager.GetUsersInRoleAsync("User");
    var allUsers = await userManager.Users.ToListAsync();

     
    foreach (var user in allUsers.Except(usersWithoutRole))
    {
        await userManager.AddToRoleAsync(user, "User");
    }
}
