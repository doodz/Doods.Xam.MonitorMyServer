using System;
using System.Resources;
using Doods.Framework.Std.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Doods.Framework.Mobile.Std.Resources
{
    [ContentProperty(nameof(Text))]
    public class TranslateExtension : TranslateService, IMarkupExtension
    {
        public TranslateExtension() : base(new ResourceManager("Doods.Framework.Mobile.Std.Resources.Resource",
            typeof(Resource).Assembly))
        {
        }

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return string.Empty;
            return Translate(Text);
        }
    }
}