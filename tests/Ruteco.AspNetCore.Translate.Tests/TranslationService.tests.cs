using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Ruteco.AspNetCore.Translate.Tests
{
    [TestFixture]
    public class TranslateionServiecTests
    {
        private IServiceProvider _provider;
        private ITranslationService _service;

        [OneTimeSetUp]
        public void SetUp()
        {
            IServiceCollection services = new ServiceCollection();
            var path = System.IO.Directory.GetParent(Assembly.GetAssembly(typeof(TranslateionServiecTests))
                           .Location) + "\\i18n";

            services.AddTranslations(path);
            _provider = services.BuildServiceProvider();
        }

        
        [Test]
        public void Should_Initialize_translation_service()
        {
            _service = _provider.GetService<ITranslationService>();


            Assert.IsTrue(_service.IsInitialized);
            Assert.AreEqual(3, _service.Languages.Count());
        }

        [Test]
        public void Should_get_translation_for_different_languages()
        {
            _service = _provider.GetService<ITranslationService>();

            var en = _service.Get("en", "Sports.Football");
            var gb = _service.Get("en-gb", "Sports.Football");
            var ru = _service.Get("ru", "Sports.Football");


            Assert.AreEqual("Football", gb);
            Assert.AreEqual("Soccer", en);
            Assert.AreEqual("Футбол", ru);
        }


        [Test]
        public void Should_return_key_if_no_translation ()
        {
            _service = _provider.GetService<ITranslationService>();

            var en = _service.Get("en", "Sports.MuayThai");
            
            Assert.AreEqual("Sports.MuayThai", en);
        }

        [Test]
        public void Should_return_key_if_no_language()
        {
            _service = _provider.GetService<ITranslationService>();

            var he = _service.Get("he", "Sports.Football");

            Assert.AreEqual("Sports.Football", he);
        }
    }
}
