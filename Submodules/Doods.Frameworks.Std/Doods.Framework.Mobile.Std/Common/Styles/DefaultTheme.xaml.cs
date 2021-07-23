using Xamarin.Forms;
using Xamarin.Forms.Xaml;
[assembly: ExportFont("fa-solid-900.ttf", Alias = "FontAwesome")]
namespace Doods.Framework.Mobile.Std.Common.Styles
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DefaultTheme : ResourceDictionary
    {
        public DefaultTheme()
        {
            InitializeComponent();
        }
    }
}