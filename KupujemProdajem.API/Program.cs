using KupujemProdajem.Application.Interfaces;
using KupujemProdajem.Application.Services;
using KupujemProdajem.Domain.Models;
using KupujemProdajem.Domain.Repositories;
using KupujemProdajem.Infrastructure.Context;
using KupujemProdajem.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAdRepository, AdRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPhotoService, PhotoService>();

builder.Services.AddDbContext<KupujemProdajemDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("KupujemProdajemConnection"))
);
builder.Services.AddIdentity<UserModel, IdentityRole>()
    .AddEntityFrameworkStores<KupujemProdajemDbContext>()
    .AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.None;
});
//builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
//                .AddEntityFrameworkStores<KupujemProdajemDbContext>();


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

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
