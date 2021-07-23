using System;
using System.Globalization;
using System.Resources;

namespace Doods.Framework.Std.Services
{
    public class TranslateService : ITranslateService
    {
        private readonly ResourceManager _resourceManager;

        public TranslateService(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        public string Translate(string resourceName)
        {
            return Translate(resourceName, _resourceManager);
        }


        public string Translate(string resourceName, ResourceManager resourceManager)
        {
            return Translate(resourceName, resourceManager, CultureInfo.CurrentCulture);
        }

        public string Translate(string resourceName, ResourceManager resourceManager, CultureInfo culture)
        {
            var translation = resourceManager.GetString(resourceName, culture);

            //var translation = ResMgr.Value.GetString(Text, ci);
            if (translation == null)
            {
#if DEBUG
                throw new ArgumentException(
                    $"Key '{resourceName}' was not found in resources '{resourceManager.GetType().Assembly}' for culture '{culture.Name}'.");
#else
                translation = resourceName; // HACK: returns the key, which GETS DISPLAYED TO THE USER
#endif
            }

            return translation;
        }
    }
}