using System.Text.Json.Serialization;
using BackendApp.Helpers;
using BackendApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var env = builder.Environment;
ConfigurationManager configuration = builder.Configuration;

services.AddDbContext<DataContext>();
services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("WebApiDatabase")));

services.AddCors();
services.AddControllers().AddJsonOptions(x =>
{
  // serialize enums as strings in api responses (e.g. Role)
  x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

  // ignore omitted parameters on models to enable optional params (e.g. User update)
  x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// configure DI for application services
services.AddScoped<IUserService, UserService>();

// For Identity
services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Adding Authentication
services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
.AddJwtBearer(options =>
{
  options.SaveToken = true;
  options.RequireHttpsMetadata = false;
  options.TokenValidationParameters = new TokenValidationParameters()
  {
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidAudience = configuration["JWT:ValidAudience"],
    ValidIssuer = configuration["JWT:ValidIssuer"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
  };
});

var securityScheme = new OpenApiSecurityScheme()
{
  Name = "Authorization",
  Type = SecuritySchemeType.ApiKey,
  Scheme = "Bearer",
  BearerFormat = "JWT",
  In = ParameterLocation.Header,
  Description = "JSON Web Token based security",
};

var securityReq = new OpenApiSecurityRequirement()
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] {}
    }
};

var contact = new OpenApiContact()
{
  Name = "Mohamad Lawand",
  Email = "hello@mohamadlawand.com",
  Url = new Uri("http://www.mohamadlawand.com")
};

var license = new OpenApiLicense()
{
  Name = "Free License",
  Url = new Uri("http://www.mohamadlawand.com")
};

var info = new OpenApiInfo()
{
  Version = "v1",
  Title = "Minimal API - JWT Authentication with Swagger demo",
  Description = "Implementing JWT Authentication in Minimal API",
  TermsOfService = new Uri("http://www.example.com"),
  Contact = contact,
  License = license
};
// builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", info);
    o.AddSecurityDefinition("Bearer", securityScheme);
    o.AddSecurityRequirement(securityReq);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
