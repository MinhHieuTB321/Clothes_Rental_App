using UserService.Application.Commons;
using UserService.Application.GlobalExceptionHandling.Utility;
using UserService.Infrastructures;
using UserService.WebApi;
using UserServices.WebAPI.Middlewares;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = builder.Configuration.Get<AppConfiguration>();
var _env = builder.Environment;
builder.Services.AddInfrastructuresService(configuration!);

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

PrepDb.PrepPopulation(app, _env.IsProduction());
app.Run();
