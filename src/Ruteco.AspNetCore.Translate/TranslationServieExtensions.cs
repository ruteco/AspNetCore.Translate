using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ruteco.AspNetCore.Translate
{
    public static class TranslationServieExtensions
    {
        public static ITranslationServiceBuilder AddTranslationsBuilder(this IServiceCollection services)
        {
            return new TranslationServiceBuilder(services);
        }

        /// <summary>
        /// Add ITranslationService and initializes it with path to dictionaries
        /// </summary>
        /// <param name="services">Dependency injection services collection</param>
        /// <param name="dictionariesLocation">Absolute path to directory where .json dictionaries stored</param>
        /// <returns></returns>
        public static ITranslationServiceBuilder AddTranslations(this IServiceCollection services, string dictionariesLocation)
        {
            var builder = services.AddTranslationsBuilder();
            builder.Services.TryAddTransient<ITranslationService, TranslationService>();
            builder.Init(dictionariesLocation);

            return builder;

        }
    }
}
