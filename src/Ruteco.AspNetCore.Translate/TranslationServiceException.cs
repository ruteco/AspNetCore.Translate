using System;
using System.Collections.Generic;
using System.Text;

namespace Ruteco.AspNetCore.Translate
{
    public class TranslationServiceException : Exception
    {
        public TranslationServiceException(string message) : base(message)
        {

        }
    }
}
