using System;
using System.Net;
using System.Text;
using Kaizen.Core.Security;
using Kaizen.Core.Services;
using Kaizen.Domain.Data;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Kaizen.Infrastructure.Identity;
using Kaizen.Infrastructure.Repositories;
using Kaizen.Infrastructure.Security;
using Kaizen.Infrastructure.Services;
using Kaizen.Infrastructure.Services.Configuration;
using Kaizen.Infrastructure.Services.MailTemplates;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Kaizen.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private const string TermsOfServiceUri = "https://cla.dotnetfoundation.org/";
        private const string ContactUrl = "https://github.com/cantte/";
        private const string LicenseUrl = "https://www.byasystems.co/license";

        public static void AddIdentityConfig(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddErrorDescriber<SpanishIdentityErrorDescriber>()
                .AddDefaultTokenProviders();
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
            IConfiguration configuration)
        {
            byte[] key = Encoding.ASCII.GetBytes(configuration["AppSettings:Key"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Ecolplag API",
                    Description = "Ecolplag API - ASP.NET Core Web",
                    TermsOfService = new Uri(TermsOfServiceUri),
                    Contact = new OpenApiContact
                    {
                        Name = "Carlos Andrés Pavajeau Max",
                        Email = "cantte098@gmail.com",
                        Url = new Uri(ContactUrl)
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Dotnet foundation license",
                        Url = new Uri(LicenseUrl)
                    }
                });

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }

        public static void ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IMailService, MailService>();
        }

        public static void ConfigureMailTemplates(this IServiceCollection services)
        {
            services.AddScoped<IMailTemplate, MailTemplate>();
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitWork, UnitWork>();
            services.AddScoped<IClientsRepository, ClientsRepository>();
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<IEmployeesRepository, EmployeesRepository>();
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<IEquipmentsRepository, EquipmentsRepository>();
            services.AddScoped<IServicesRepository, ServicesRepository>();
            services.AddScoped<IActivitiesRepository, ActivitiesRepository>();
            services.AddScoped<IServiceRequestsRepository, ServiceRequestsRepository>();
            services.AddScoped<IWorkOrdersRepository, WorkOrdersRepository>();
            services.AddScoped<IServiceInvoicesRepository, ServiceInvoicesRepository>()
                .AddScoped<IProductInvoicesRepository, ProductInvoicesRepository>();
            services.AddScoped<IStatisticsRepository, StatisticsRepository>();
            services.AddScoped<INotificationsRepository, NotificationsRepository>();
            services.AddScoped<ICertificatesRepository, CertificatesRepository>();
        }

        public static void ConfigureTokenGenerator(this IServiceCollection services)
        {
            services.AddScoped<ITokenGenerator, TokenGenerator>();
        }

        public static void LoadMailSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailSettings>(m =>
            {
                m.Host = configuration["Mail:Host"];
                m.Port = int.Parse(configuration["Mail:Port"]);
                m.EnableSsl = true;
                m.UseDefaultCredentials = false;

                string username = configuration["Mail:User"];
                string password = configuration["Mail:Password"];

                m.Credential = new NetworkCredential(username, password);
            });
        }
    }
}
