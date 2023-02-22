using API.Filters;
using Application.Abstract;
using Application.Commands.Users;
using DataAccess;
using DataAccess.Repositories;
using MediatR;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Identity.Web;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Application.Commands.Blogs;
using Application.Interfaces;
using Domain.Settings;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

builder.Services.AddControllers(options => options.Filters.Add(new ExceptionHandler()));
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new OpenApiInfo { Title = "BlogWebsite-API", Version = "v1" });

    o.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
    {
        Description = "OAuth2 AuthorizationCodeFlow",
        Name = "OAuth2",
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri(builder.Configuration["SwaggerAzureAD:AuthorizatoinUrl"]),
                TokenUrl = new Uri(builder.Configuration["SwaggerAzureAD:TokenUrl"]),
                Scopes = new Dictionary<string, string>
            {
                { builder.Configuration["SwaggerAzureAD:Scope"], "Access API as User" }
            }
            }
        }
    });

    o.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "OAuth2" }
        },
        new[] { builder.Configuration["SwaggerAzureAD:Scope"] }
    }
});
});




builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddMediatR(typeof(IAssemblyMarker));
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policy =>
{ policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
});
    
    });


builder.Services.Configure<CloudinaryOptions>(builder.Configuration.GetSection("CloudinaryOptions"));

//builder.Services.Configure<MySettingsSection>(
//    builder.Configuration.GetSection(
//        nameof(MySettingsSection)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(o =>
    {
        o.OAuthClientId(builder.Configuration["SwaggerAzureAD:ClientId"]);

        o.OAuthUsePkce();

        // o.OAuthScopeSeparator(" "); // For more than one scope
    });
}

app.UseHttpsRedirection();

app.UseCors();
app.UseAuthentication();

app.UseAuthorization();
//app.UseMyMiddleware();
//app.UseMyMiddleware2();

app.MapControllers();

app.Run();
app.Run();

