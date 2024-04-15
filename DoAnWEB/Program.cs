using DoAnWEB.Models;
using DoAnWEB.Repositories;
using Microsoft.EntityFrameworkCore;
using DoAnWEB.Data;
using Microsoft.AspNetCore.Identity;
using DoAnWEB.Areas.Identity.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;


var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddAuthentication()
   .AddGoogle(options =>
   {
       IConfigurationSection googleAuthNSection =
       config.GetSection("Authentication:Google");
       options.ClientId = googleAuthNSection["ClientId"];
       options.ClientSecret = googleAuthNSection["ClientSecret"];
   });

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<AuthDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
	options.Password.RequireUppercase = false;
});

builder.Services.AddScoped<ISongRepository, EFSongRepository>();
builder.Services.AddScoped<IGenreRepository, EFGenreRepository>();

builder.Services.AddLogging();
var app = builder.Build();

// Khởi tạo Roles và tài khoản Admin
using (var scope = app.Services.CreateScope())
{
	var serviceProvider = scope.ServiceProvider;
	var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
	var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

	// Tạo Roles
	if (!await roleManager.RoleExistsAsync("Admin"))
	{
		await roleManager.CreateAsync(new IdentityRole("Admin"));
	}

	if (!await roleManager.RoleExistsAsync("Listener"))
	{
		await roleManager.CreateAsync(new IdentityRole("Listener"));
	}

	// Kiểm tra và tạo tài khoản Admin
	var adminEmail = "musicweb@admin.com";
	var adminUser = await userManager.FindByEmailAsync(adminEmail);

	if (adminUser == null)
	{
		var admin = new ApplicationUser { 
			UserName = adminEmail, 
			Email = adminEmail,
			FirstName = "Admin",
			LastName = "User"
		};
		var result = await userManager.CreateAsync(admin, "Admin@123");
		if (result.Succeeded)
		{
			await userManager.AddToRoleAsync(admin, "Admin");
		}
	}
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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
app.Run();

