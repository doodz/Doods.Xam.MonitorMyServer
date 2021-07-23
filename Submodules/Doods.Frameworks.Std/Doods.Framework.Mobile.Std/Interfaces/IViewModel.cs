using System.Windows.Input;
using Doods.Framework.Mobile.Std.Mvvm;

namespace Doods.Framework.Mobile.Std.Interfaces
{
    public interface IViewModel
    {
        ICommand CmdState { get; }
        ViewModelState ViewModelState { get; }
        IColorPalette ColorPalette { get; }
    }
}