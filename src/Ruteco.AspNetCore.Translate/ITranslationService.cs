using System;
using System.Collections.Generic;

namespace Ruteco.AspNetCore.Translate
{
    public interface ITranslationService
    {
        bool IsInitialized { get; }
        string DictionariesLocation { get; }
        IEnumerable<string> Languages { get; }

        string Get(string language, string key);

    }
}