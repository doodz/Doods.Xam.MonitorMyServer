using System.Threading.Tasks;

namespace Doods.Framework.Mobile.Std.Interfaces
{
    public interface INotificator
    {
        /// <summary>
        ///     Affiche un toast
        /// </summary>
        /// <param name="description">Contenu du toast</param>
        /// <param name="title">Titre</param>
        /// <remarks>Le toast est lancé sur le Thread UI</remarks>
        Task Toast(string description, string title = null);

        Task Notify(string description, string title = null, int? id = null);
    }
}