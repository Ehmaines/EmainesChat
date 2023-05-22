using EmainesChat.API;
using EmainesChat.Infra;
using EmainesChat.Business.Commands;
using static System.Net.Mime.MediaTypeNames;
using FluentValidation.AspNetCore;
using FluentValidation;
using EmainesChat.API.SignalRControllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var services = builder.Services;

services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

DependencyInjectionConfig.Configure(services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:4200") // Especifique a origem exata do seu aplicativo cliente.
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials(); // Permite credenciais na solicitação.
});

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<MessageHub>("/messageHub");
    endpoints.MapControllers();
});

//app.MapControllers();

app.Run();
