using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Ruteco.AspNetCore.Translate
{
    public class TranslationService: ITranslationService
    {

        public bool IsInitialized => TranslationServiceBuilder.Initialized;
        public string DictionariesLocation => TranslationServiceBuilder.DictionariesLocation;
        public IEnumerable<string> Languages => TranslationServiceBuilder.Dictionaries.Select(x => x.Key);


        public TranslationService()
        {

        }


        public string Get(string language, string key)
        {
            if (!TranslationServiceBuilder.Initialized)
            {
                throw new TranslationServiceException("The translation service not initialized");
            }

            try
            {
                var dictionary = TranslationServiceBuilder.Dictionaries[language];
                return dictionary.SelectToken(key).ToString();
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                return key;
            }

        }
    }
}
