using System.Windows.Input;
using Doods.Framework.Mobile.Std.Enum;
using Xamarin.Forms;

namespace Doods.Framework.Mobile.Std.controls
{
    public class CommandItem
    {
        public CommandItem(CommandId id)
        {
            Id = id;
        }

        /// <summary>
        ///     Command
        /// </summary>
        public ICommand Command { get; set; }

        public CommandPlacement Placement { get; set; } = CommandPlacement.All;

        public CommandId Id { get; }

        /// <summary>
        ///     Texte
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        ///     Indique si l'élément est actif
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        ///     Indique si l'élément est principale
        /// </summary>
        public bool IsPrimary { get; set; }

        /// <summary>
        ///     Icone
        /// </summary>
        public FileImageSource Icon { get; set; }

        /// <summary>
        ///     Nom du paramètre pour le CommandParameter
        /// </summary>
        public string CommandParameterName { get; set; }

        /// <summary>
        ///     Source du binding sur CommandParameterName pour le CommandParameter
        /// </summary>
        public object CommandParameterSourceForBinding { get; set; }

        public void TryExecute(object param)
        {
            Command?.Execute(param);
        }
    }
}