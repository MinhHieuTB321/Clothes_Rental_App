using ComboService.Application.Commons;
using ComboService.Infrastructures;
using ComboService.WebApi;
using ComboService.WebApi.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var _env = builder.Environment;
var configuration = builder.Configuration.Get<AppConfiguration>();
{

    builder.Services.AddInfrastructureServices(configuration!);
    builder.Services.AddWebAPIService(configuration!);
}

var app = builder.Build();
{
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

    app.UseMiddleware<ExceptionMiddleware>();
    app.UseMiddleware<PerformanceMiddleware>();


    // app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    //PrepDb.PrepPopulation(app, _env.IsProduction());

    app.Run();

}