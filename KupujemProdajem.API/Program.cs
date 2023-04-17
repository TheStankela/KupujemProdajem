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

builder.Services.AddDbContext<KupujemProdajemDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("KupujemProdajemConnection"))
);
builder.Services.AddIdentity<UserModel, IdentityRole>()
    .AddEntityFrameworkStores<KupujemProdajemDbContext>();

//builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
//                .AddEntityFrameworkStores<KupujemProdajemDbContext>();

builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
