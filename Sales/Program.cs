using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Sale.Domain;
using Sale.Domain.Entities;
using Sale.Repository.Core;
using Sale.Repository.ProductRepository;
using Sale.Service.Core;
using Sale.Service.Dtos;
using Sale.Service.EmailService;
using Sale.Service.ProductService;
using Sales;
using System.Reflection;
using System.Text;
using Swashbuckle.AspNetCore.Filters;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
	options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Name = "Authorization",
		Description = "Bearer Authentication with JWT Token",
		Type = SecuritySchemeType.ApiKey,
	});
	options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddDbContext<SaleContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("ConStr"));
});


//builder.Services.ADDIDE<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//.AddRoles<IdentityRole>()
//.AddEntityFrameworkStores<UserAccountContext>()
//.AddDefaultTokenProviders();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
	options.Password.RequireDigit = true;
	options.Password.RequireLowercase = true;
	options.Password.RequiredLength = 8;
	options.Password.RequireUppercase = true;
	options.Password.RequireNonAlphanumeric = true;

	// User setting
	options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_";

}).AddEntityFrameworkStores<SaleContext>().AddDefaultTokenProviders();





builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt =>
{
	jwt.SaveToken = true;
	jwt.RequireHttpsMetadata = false;
	jwt.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateAudience = true,
		ValidateIssuer = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		ValidAudience = builder.Configuration["Jwt:Audience"],
		RequireSignedTokens = true,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
	};
});

//builder.Services.AddAuthorization(options =>
//{
//	options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
//});

//// repo setting
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
var repositoryTypes = typeof(IRepository<>).Assembly.GetTypes().Where(x => !string.IsNullOrEmpty(x.Namespace) && x.Namespace.StartsWith("Sale.Repository") && x.Name.EndsWith("Repository"));
foreach (var repo in repositoryTypes.Where(t => t.IsInterface))
{
	var impl = repositoryTypes.FirstOrDefault(c => c.IsClass && repo.Name.Substring(1) == c.Name);
	if (impl != null)
	{
		builder.Services.AddScoped(repo, impl);
	}
}

builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
var serviceTypes = typeof(IService<>).Assembly.GetTypes().Where(x => !string.IsNullOrEmpty(x.Namespace) && x.Namespace.StartsWith("Sale.Service") && x.Name.EndsWith("Service"));
foreach (var serc in serviceTypes.Where(t => t.IsInterface))
{
	var impl = serviceTypes.FirstOrDefault(c => c.IsClass && serc.Name.Substring(1) == c.Name);
	if (impl != null)
	{
		builder.Services.AddScoped(serc, impl);
	}
}
builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));


builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.Configure<SMTP>(builder.Configuration.GetSection("SMTPConfig"));

builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(type => type.ToString());
});
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAnyOrigin",
		 builder =>
		 {
			 builder.WithOrigins("*")
					 .AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader();
		 });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();


app.UseCors(x => x
	 .AllowAnyMethod()
	 .AllowAnyHeader()
	 .AllowCredentials()
	 .SetIsOriginAllowed(origin => true));


app.MapControllers();

app.Run();
