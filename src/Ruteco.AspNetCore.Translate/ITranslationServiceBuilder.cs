using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Ruteco.AspNetCore.Translate
{
    public interface ITranslationServiceBuilder
    {
        IServiceCollection Services { get; }
        bool Init(string dictionariesPath);
    }
}
