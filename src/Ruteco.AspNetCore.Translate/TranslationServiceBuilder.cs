using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;

namespace Ruteco.AspNetCore.Translate
{
    public class TranslationServiceBuilder : ITranslationServiceBuilder
    {

        private static bool _initialized = false;
        public static bool Initialized => _initialized;

        private static string _dictionariesLocation;
        public static string DictionariesLocation => _dictionariesLocation;

        private static Dictionary<string, JObject> _dictionaries = new Dictionary<string, JObject>();
        public static Dictionary<string, JObject> Dictionaries => _dictionaries;


        public IServiceCollection Services { get; }

        public TranslationServiceBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public bool Init(string dictionariesLocation)
        {
            TranslationServiceBuilder._dictionariesLocation = dictionariesLocation;
            try
            {
                var files = System.IO.Directory.GetFiles(dictionariesLocation);

                var jsonFiles = files.Where(x => x.EndsWith(".json"));
                TranslationServiceBuilder._dictionaries = new Dictionary<string, JObject>();

                foreach (var file in jsonFiles)
                {
                    var fileInfo = new FileInfo(file);
                    var lang = fileInfo.Name.Split('.')[0];
                    var content = System.IO.File.ReadAllText(file);
                    var jObject = JObject.Parse(content);

                    TranslationServiceBuilder._dictionaries.Add(lang, jObject);
                }

                TranslationServiceBuilder._initialized = true;

                return true;
            }
            catch (Exception exp)
            {
                return false;
            }
        }
    }
}
