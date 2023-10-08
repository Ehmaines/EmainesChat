using EmainesChat.API;
using EmainesChat.Infra;
using EmainesChat.Business.Commands;
using static System.Net.Mime.MediaTypeNames;
using FluentValidation.AspNetCore;
using FluentValidation;
using EmainesChat.API.SignalRControllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using EmainesChat.Business;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Routing.Tree;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var services = builder.Services;


services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

var key = Encoding.ASCII.GetBytes(Settings.Secret);

services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
  {
      options.RequireHttpsMetadata = false;
      options.SaveToken = true;
      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateIssuer = false,
          ValidateAudience = false
      };
  });

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

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<MessageHub>("/messageHub");
    endpoints.MapControllers();
});

//app.MapControllers();

app.Run();
