using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AuthenticationService;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
		builder.Services.AddAuthorizationBuilder();

		builder.Services.AddDbContext<AppDbContext>(options => {
			options.UseSqlite("DataSource=app.db");
		});

		builder.Services.AddIdentityCore<MyUser>()
			.AddEntityFrameworkStores<AppDbContext>()
			.AddApiEndpoints();

		var app = builder.Build();

		app.MapIdentityApi<MyUser>();

		app.MapGet("/", (ClaimsPrincipal user) => $"Hello {user.Identity!.Name}!").RequireAuthorization();

		app.Run();
	}
}

class MyUser : IdentityUser{ }

class AppDbContext : IdentityDbContext<MyUser>
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}
}
