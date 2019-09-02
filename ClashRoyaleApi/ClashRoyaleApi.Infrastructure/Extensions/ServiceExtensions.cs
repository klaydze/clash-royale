using ClashRoyaleApi.Core.Contracts;
using ClashRoyaleApi.Infrastructure.Repository;
using ClashRoyaleApi.Infrastructure.SearchExpressionProviders.Contracts;
using ClashRoyaleApi.Infrastructure.SearchExpressionProviders.Implementations;
using ClashRoyaleApi.Infrastructure.Services.Contracts;
using ClashRoyaleApi.Infrastructure.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace ClashRoyaleApi.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Configure repository wrapper.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        /// <summary>
        /// Add all the services that the app uses.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureAppServices(this IServiceCollection services)
        {
            services.AddScoped<ISearchExpressionProvider, SearchExpressionProvider>();

            services.AddScoped<ICardService, CardService>();
            services.AddScoped<IArenaService, ArenaService>();
        }
    }
}
