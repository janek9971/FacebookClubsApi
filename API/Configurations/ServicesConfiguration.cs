using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Parser.Repositories;
using ParserModel.Repositories;

namespace API.Configurations
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IParserRootRepository, ParserRootRepository>();
            services.AddScoped<IParseUtilitiesRepository, ParseUtilitiesRepository>();

            services.AddSingleton<IChromeDriverSetupRepository, ChromeDriverSetupRepository>();
            services.AddScoped<IParseWebRepository, ParseWebRepository>();
            return services;
        }
        //public static IServiceCollection ConfigureSupervisor(this IServiceCollection services)
        //{
        //    services.AddScoped<IPlaylistSupervisor, PlaylistSupervisor>();


        //    return services;
        //}
    }
}
