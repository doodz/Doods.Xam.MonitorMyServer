using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using Doods.Framework.Std.Services;
using Doods.Xam.MonitorMyServer.Resx.Webmin.package_updates;
using Doods.Xam.MonitorMyServer.Resx.Webmin.software;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Doods.Openmediavault.Mobile.Std.Resources
{
[ContentProperty(nameof(Text))]
    public class WebminTranslateExtension : TranslateService, IMarkupExtension
    {
        private const string ResourceId = "Doods.Xam.MonitorMyServer.Resx.Webmin.package_updates";

        private static readonly IDictionary<string, ResourceManager> ResourcesManagersLst =
            new Dictionary<string, ResourceManager>
            {
                {
                    nameof(Webmin_package_updates),
                    new ResourceManager("Doods.Xam.MonitorMyServer.Resx.Webmin.package_updates.Webmin_package-updates",
                        typeof(Webmin_package_updates).Assembly)
                },
                {
                    nameof(Webmin_software),
                    new ResourceManager("Doods.Xam.MonitorMyServer.Resx.Webmin.software.Webmin_software",
                        typeof(Webmin_software).Assembly)
                },

            };

        private readonly CultureInfo ci = null;

        public WebminTranslateExtension() : base(Resource.ResourceManager)
        {
        }


        //static readonly Lazy<ResourceManager> ResMgr = new Lazy<ResourceManager>(
        //    () => new ResourceManager(ResourceId, IntrospectionExtensions.GetTypeInfo(typeof(TranslateExtension)).Assembly));

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return string.Empty;

            if (Text.Contains(nameof(Webmin_package_updates)))
            {
                var array = Text.Split(new[] {"::"}, StringSplitOptions.RemoveEmptyEntries);
                if (ResourcesManagersLst.TryGetValue(array[0], out var manager))
                {
                    
                        return Translate(array[1], manager);
                }
            }
            if (Text.Contains(nameof(Webmin_software)))
            {
                var array = Text.Split(new[] { "::" }, StringSplitOptions.RemoveEmptyEntries);
                if (ResourcesManagersLst.TryGetValue(array[0], out var manager))
                {

                    return Translate(array[1], manager);
                }
            }
            return Translate(Text);
        }
    }
}