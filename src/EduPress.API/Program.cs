using EduPress.API;
using EduPress.Application;
using EduPress.Application.Common.Email;
using EduPress.Application.Helpers;
using EduPress.Application.Helpers.BasicAuth;
using EduPress.DataAccess;
using EduPress.DataAccess.Persistence;
using Microsoft.AspNetCore.Authentication;
using OfficeOpenXml;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.ComponentModel;
using HealthChecks.NpgSql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();

builder.Services.AddControllers()
    .AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddHttpContextAccessor();

builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration"));
builder.Services.AddApplication(builder.Environment, builder.Configuration)
                .AddDataAccess(builder.Configuration);

//builder.Services.AddAuth(builder.Configuration);
//builder.Services.AddSwagger();
builder.Services.AddSwaggerGenBasicAuth();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

builder.Services.AddAuthentication()
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(
        BasicAuthenticationDefaults.AuthenticationScheme, null
    );

var app = builder.Build();

using var scope = app.Services.CreateScope();

await AutomatedMigration.MigrateAsync(scope.ServiceProvider);

app.UseSwagger();
app.UseSwaggerUI();

// HTTPS ni Docker muhitida o'chirish
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
