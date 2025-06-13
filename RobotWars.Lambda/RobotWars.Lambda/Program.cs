using Microsoft.OpenApi.Models;
using RobotWars.Lambda.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddRobotWarsServices(); 


builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Robot Wars API",
        Version = "v1",
        Description = "API for controlling robots in a battle arena."
    });
   
    options.OperationFilter<TextPlainRequestFilter>();
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Robot Wars API v1");
        c.RoutePrefix = string.Empty; 
    });
}

app.Run();
