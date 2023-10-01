using UserService.Application.Commons;
using UserService.Application.GlobalExceptionHandling.Utility;
using UserService.Infrastructures;
using UserService.WebApi;
using UserServices.WebAPI.Middlewares;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var configuration = builder.Configuration.Get<AppConfiguration>();
var databaseConnection= builder.Configuration["DatabaseConnection"];

builder.Services.AddInfrastructuresService(databaseConnection!);

builder.Services.AddWebAPIService(configuration!);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BasketAPI v1"));
}

app.UseCors();

app.UseMiddleware(typeof(GlobalErrorHandlingMiddleware));
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<PerformanceMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
