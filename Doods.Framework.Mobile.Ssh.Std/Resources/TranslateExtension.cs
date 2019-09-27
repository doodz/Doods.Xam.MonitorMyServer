using System;
using System.Globalization;
using System.Resources;
using Doods.Framework.Std.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Doods.Framework.Mobile.Ssh.Std.Resources
{
    [ContentProperty("Text")]
    public class TranslateExtension : TranslateService,IMarkupExtension
    {
        readonly CultureInfo ci = null;
        const string ResourceId = "Doods.Framework.Mobile.Ssh.Std.Resources.Resource";

        //static readonly Lazy<ResourceManager> ResMgr = new Lazy<ResourceManager>(
        //    () => new ResourceManager(ResourceId, IntrospectionExtensions.GetTypeInfo(typeof(TranslateExtension)).Assembly));

        public string Text { get; set; }

        public TranslateExtension():base(new ResourceManager(ResourceId, typeof(Resource).Assembly))
        {
            
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return string.Empty;
            return this.Translate(Text);
        }
    }
}
