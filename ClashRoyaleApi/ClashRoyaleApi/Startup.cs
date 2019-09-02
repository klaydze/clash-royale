using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ClashRoyaleApi.Core.Contracts;
using ClashRoyaleApi.Infrastructure;
using ClashRoyaleApi.Infrastructure.Extensions;
using ClashRoyaleApi.Infrastructure.Filters;
using ClashRoyaleApi.Infrastructure.Mapper;
using ClashRoyaleApi.Infrastructure.Models;
using ClashRoyaleApi.Infrastructure.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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

            services.AddDbContext<ClashRoyaleContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ClashRoyaleConnString"))
            );

            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.ConfigureRepositoryWrapper();
            services.ConfigureAppServices();
            
            services.AddRouting(options =>
                options.LowercaseUrls = true);

            services
                .AddMvc(options =>
                {
                    options.Filters.Add<ApiExceptionFilter>();
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // This will use our own error model "ApiError" when displaying error message
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errorResponse = new ApiError(context.ModelState);
                    return new BadRequestObjectResult(errorResponse);
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
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
            app.UseMvc();
        }
    }
}
