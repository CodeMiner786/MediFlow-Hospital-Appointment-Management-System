using HealthcareHospitalManagement.Application.Handlers.QueryHandlers.Auth;
using HealthcareHospitalManagement.Application.Helpers.MapperService;
using HealthcareHospitalManagement.Application.Helpers.ValidationsService;
using HealthcareHospitalManagement.Domain.Interfaces.Identity;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.Repositories.Identity;
using HealthcareHospitalManagement.Infrastructure.Seeder;
using HealthcareHospitalManagement.Infrastructure.ServiceExtensions.JwtService;
using HealthcareHospitalManagement.Infrastructure.ServiceExtensions.RepositoryRegistration;
using HealthcareHospitalManagement.Infrastructure.ServiceExtensions.UnitOfWorkService;
using MediFlow_API.Middlewares.ExceptionThrow;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

namespace MediFlow_API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ── Database ──────────────────────────────────────────────
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString,
                b => b.MigrationsAssembly("HealthcareHospitalManagement.Infrastructure")));

            builder.Services.AddScoped<HealthcareHospitalManagement.Application.Interfaces.IApplicationDbContext>(provider =>
                provider.GetRequiredService<ApplicationDbContext>());

            // ── Custom Auth Services ──────────────────────────────────
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
            builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

            // ── App Layer Services ────────────────────────────────────
            builder.Services.AddMapperProfiles();
            builder.Services.AddRepositories();
            builder.Services.AddUnitOfWorks();
            builder.Services.AddApplicationValidators();

            // ✅ AdminSeeder register
            builder.Services.AddScoped<AdminSeederService>();

            // ── Controllers + JSON Enum Support ──────────────────────
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            // ── JWT Settings ──────────────────────────────────────────
            builder.Services.Configure<JwtSettings>(
                builder.Configuration.GetSection("JwtSettings"));

            // ── MediatR ───────────────────────────────────────────────
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(GetUserProfileQueryHandler).Assembly);
            });

            // ── JWT Authentication ────────────────────────────────────
            var jwtSettings = builder.Configuration
                .GetSection("JwtSettings")
                .Get<JwtSettings>()!;

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                                                Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,

                    // ✅ এটা যোগ করা হয়েছে
                    // কারণ: JWT token এ ClaimTypes.Role দিয়ে role store হচ্ছে
                    // এটা না থাকলে [Authorize(Roles="Admin")] কাজ করে না
                    // User.IsInRole() ও কাজ করে না
                    RoleClaimType = ClaimTypes.Role
                };
            });

            // ── Swagger ───────────────────────────────────────────────
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MediFlow API", Version = "v1" });
                opt.UseInlineDefinitionsForEnums();
                opt.DescribeAllParametersInCamelCase();

                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token (Format: Bearer your_token)",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id   = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            // ─────────────────────────────────────────────────────────
            var app = builder.Build();
            // ─────────────────────────────────────────────────────────

            // ✅ Seeder — app.Run() এর আগে একবার চলবে
            using (var scope = app.Services.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetRequiredService<AdminSeederService>();
                await seeder.SeedAsync();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}