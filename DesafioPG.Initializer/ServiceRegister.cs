using DesafioPG.Services.IServices;
using DesafioPG.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioPG.Initializer
{
    public static class ServiceRegister
    {
        /// <summary>
        /// Mapping registry Services
        /// </summary>
        /// <param name="services"></param>
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IMarvelService, MarvelService>();
        }
    }
}