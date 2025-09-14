using B1TestTask.Infrastructure.Extensions;
using B1TestTask2.Api.Middlewares;
using B1TestTask2.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddServices();
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
