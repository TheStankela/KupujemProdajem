using IdentityServer4.AccessTokenValidation;
using KupujemProdajem.Application.Interfaces;
using KupujemProdajem.Application.Mediator.Queries.AdQueries;
using KupujemProdajem.Application.Services;
using KupujemProdajem.Domain.Helpers;
using KupujemProdajem.Domain.Interfaces;
using KupujemProdajem.Domain.Models;
using KupujemProdajem.Domain.Repositories;
using KupujemProdajem.Infrastructure.Context;
using KupujemProdajem.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAdRepository, AdRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IPhotoRepository, PhotoRepository>();
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

//MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(GetAllAdsQuery)));

//Database context
builder.Services.AddDbContext<KupujemProdajemDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("KupujemProdajemConnection"))
);
//Identity
builder.Services.AddIdentity<UserModel, IdentityRole>()
    .AddEntityFrameworkStores<KupujemProdajemDbContext>()
    .AddDefaultTokenProviders();

//Without this configuration API calls that need authorization return 404 
builder.Services.ConfigureApplicationCookie(o =>
{
    o.Events = new CookieAuthenticationEvents()
    {
        OnRedirectToLogin = (ctx) =>
        {
            if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
            {
                ctx.Response.StatusCode = 401;
            }

            return Task.CompletedTask;
        },
        OnRedirectToAccessDenied = (ctx) =>
        {
            if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
            {
                ctx.Response.StatusCode = 403;
            }

            return Task.CompletedTask;
        }
    };
});

//Adding CORS
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(options =>
    options.WithOrigins("http://localhost:4200", "http://localhost:4200/login")
          .AllowAnyHeader()
          .AllowAnyMethod()
          .AllowCredentials());

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
