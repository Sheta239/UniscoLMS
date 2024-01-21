using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Text;
using UniscoLMS.DataBaseModels;
using UniscoLMS.Extensions;
using UniscoLMS.Services;
using ViewModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UniscoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UniscoDatabase")));
builder.Services.AddScoped<PaymobService>();
builder.Services.AddScoped<adapterService>();
// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JwtSettings:ValidAudience"],
        ValidIssuer = builder.Configuration["JwtSettings:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]))
    };
});

builder.Services.AddSwaggerGen(swagger =>
{
    swagger.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    swagger.IgnoreObsoleteActions();
    swagger.IgnoreObsoleteProperties();
    swagger.CustomSchemaIds(type => type.FullName);

    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "JWT Token Authentication API",
        Description = "ASP.NET Core 6 Web API"
    });
    // To Enable authorization using Swagger (JWT)  
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwaggerAuthorized();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Exception handler
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exception = context.Features?.Get<IExceptionHandlerPathFeature>()?.Error;

        if (exception is UniscoException)
        {
            var oneClassException = (UniscoException)exception;
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            await context.Response.WriteAsJsonAsync(oneClassException.GetErrorModel());
            return;
        }

        context.Response.StatusCode = (int)HttpStatusCode.OK;
        await context.Response.WriteAsJsonAsync(new UniscoException("UniscoException: " + exception?.Message).GetErrorModel());
    });
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
