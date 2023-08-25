using InforceShortener.Configuration;
using InforceShortener.Data;
using InforceShortener.Data.Models;
using InforceShortener.Interfaces;
using InforceShortener.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MySqlConnector;

namespace InforceShortener
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Constants.ISSUER,

                        ValidateAudience = true,
                        ValidAudience = Constants.AUDIENCE,

                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(Constants.LIFETIME),

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = AuthorizationService.GetSymmetricSecurityKey()
                    };
                });

            var mySqlConnectionbuilder = new MySqlConnectionStringBuilder(builder.Configuration.GetConnectionString("DefaultConnection"));

            builder.Services.AddDbContext<DataContext>(
                options => options.UseLazyLoadingProxies().UseMySql(
                    mySqlConnectionbuilder.ConnectionString,
                    new MySqlServerVersion(new Version(8, 0)),
                    x => x.MigrationsAssembly("InforceShortener.Data")
                )
            );

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });
            });

            builder.Services.AddScoped<IUrlService, UrlService>();
            builder.Services.AddScoped<IWebContentService, WebContentService>();
            builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
            builder.Services.AddScoped<Repository<User>>();
            builder.Services.AddScoped<Repository<UrlRecord>>();
            builder.Services.AddScoped<Repository<WebContent>>();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}