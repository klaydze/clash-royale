using AutoMapper;
using ClashRoyaleApi.Infrastructure;
using ClashRoyaleApi.Infrastructure.Extensions;
using ClashRoyaleApi.Infrastructure.Filters;
using ClashRoyaleApi.Infrastructure.Mapper;
using ClashRoyaleApi.Infrastructure.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Claims;

namespace ClashRoyaleApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Get the default values that will be use in paginating the results
            services.Configure<PagingOptions>(Configuration.GetSection("DefaultPagingOptions"));

            // Get the default azure storage blob settings
            services.Configure<AzureBlobStorageSettings>(Configuration.GetSection("AzureBlobStorageSettings"));

            // Get the default Sts settings
            services.Configure<StsSettings>(Configuration.GetSection("DefaultStsSettings"));

            services.AddDbContext<ClashRoyaleContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ClashRoyaleConnString"),
                sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                })
            );

            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.ConfigureRepositoryWrapper();
            services.ConfigureAppServices();

            services.AddRouting(options =>
                options.LowercaseUrls = true);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = Configuration.GetValue<string>("DefaultStsSettings:authority");
                    options.Audience = Configuration.GetValue<string>("DefaultStsSettings:audience");
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ReadOnlyPolicy", policy =>
                {
                    policy.RequireAuthenticatedUser()
                        .RequireClaim("scope", "cr_api.read_only");
                });

                options.AddPolicy("ReadWritePolicy", policy =>
                {
                    policy.RequireAuthenticatedUser()
                        .RequireClaim(ClaimTypes.Role, "user_read_write", "user_admin");
                });

                options.AddPolicy("AdminPolicy", policy =>
                {
                    policy.RequireAuthenticatedUser()
                        .RequireClaim("scope", "cr_api.admin")
                        .RequireClaim(ClaimTypes.Role, "user_admin");
                });
            });

            services
                .AddMvc(options =>
                {
                    options.Filters.Add<ApiExceptionFilter>();
                    options.EnableEndpointRouting = false;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            // This will use our own error model "ApiError" when displaying error message
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errorResponse = new ApiError(context.ModelState);
                    return new BadRequestObjectResult(errorResponse);
                };
            });

            services.AddRouting();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName == "Development")
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}