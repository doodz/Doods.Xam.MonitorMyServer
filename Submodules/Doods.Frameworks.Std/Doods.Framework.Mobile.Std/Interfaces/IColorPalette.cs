using Xamarin.Forms;

namespace Doods.Framework.Mobile.Std.Interfaces
{
    public interface IColorPalette
    {
        Color Primary { get; }

        Color Accent { get; }

        Color Transparent { get; }

        Color TextGray { get; }

        Color Previsionnel { get; }

        Color Realise { get; }

        Color Red { get; }

        Color Orange { get; }

        Color LightGreen { get; }

        void SetConfiguration(ResourceDictionary resource);
    }
}