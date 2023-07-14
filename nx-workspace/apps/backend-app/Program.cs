using System.Text.Json.Serialization;
using BackendApp.Helpers;
using BackendApp.Services;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var env = builder.Environment;

services.AddDbContext<DataContext>();
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

// builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
