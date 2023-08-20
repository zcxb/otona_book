using System.Text.Json.Serialization;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OtonaBookApi.Controllers;
using OtonaBookApi.DataAccess;
using OtonaBookApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddDbContextPool<OtonaBookContext>(options =>
    {
        options.UseNpgsql("Host=localhost;Port=5455;Database=otona_book;Username=zcxb;Password=123456");
    });
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.RequestBody | HttpLoggingFields.RequestMethod | HttpLoggingFields.RequestPath | HttpLoggingFields.RequestQuery | HttpLoggingFields.ResponseBody;
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});
builder.Services.AddControllers().AddControllersAsServices().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
builder.Services.Replace(ServiceDescriptor.Transient<IControllerActivator, AreaControllerBaseActivator>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<GlobalExceptionMiddleware>();
app.MapControllers();

app.Run();

