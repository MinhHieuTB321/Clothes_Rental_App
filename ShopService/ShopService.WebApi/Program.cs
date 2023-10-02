using ShopService.Application.Commons;
using ShopService.Application.GlobalExceptionHandling.Utility;
using ShopService.Infrastructures;
using ShopService.WebApi;
using ShopService.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


var configuration = builder.Configuration.Get<AppConfiguration>();
builder.Services.AddServices(builder.Configuration.GetConnectionString("ClothesRentalDB")!);
builder.Services.AddWebAPIService(configuration!);

//
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
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
