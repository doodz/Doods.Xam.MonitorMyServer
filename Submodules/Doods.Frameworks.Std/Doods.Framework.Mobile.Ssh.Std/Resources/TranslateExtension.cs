using System;
using System.Globalization;
using System.Resources;
using Doods.Framework.Std.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Doods.Framework.Mobile.Ssh.Std.Resources
{
    [ContentProperty("Text")]
    public class TranslateExtension : TranslateService, IMarkupExtension
    {
        private const string ResourceId = "Doods.Framework.Mobile.Ssh.Std.Resources.Resource";
        private readonly CultureInfo ci = null;

        public TranslateExtension() : base(new ResourceManager(ResourceId, typeof(Resource).Assembly))
        {
        }

        //static readonly Lazy<ResourceManager> ResMgr = new Lazy<ResourceManager>(
        //    () => new ResourceManager(ResourceId, IntrospectionExtensions.GetTypeInfo(typeof(TranslateExtension)).Assembly));

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return string.Empty;
            return Translate(Text);
        }
    }
}