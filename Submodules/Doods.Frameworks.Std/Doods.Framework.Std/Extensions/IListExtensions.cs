using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Doods.Framework.Std.Extensions
{
    public static class IListExtensions
    {
        /// <summary>
        ///     Ajoute tous les éléments de @items à @list
        /// </summary>
        /// <param name="list">Liste cible</param>
        /// <param name="items">Éléments à ajouter</param>
        public static void AddRange(this IList list, IEnumerable items)
        {
            foreach (var i in items) list.Add(i);
        }

        /// <summary>
        ///     Ajoute tous les éléments de @items à @list
        /// </summary>
        /// <param name="list">Liste cible</param>
        /// <param name="items">Éléments à ajouter</param>
        public static void AddRange<TValue>(this IList<TValue> list, IEnumerable<TValue> items)
        {
            foreach (var i in items) list.Add(i);
        }

        /// <summary>
        ///     Supprime tous les éléments de @list pour lesquels @predicate retourne true,
        ///     ou tous les éléments si @predicate est null
        /// </summary>
        /// <param name="list">Liste</param>
        /// <param name="predicate">Clause de vérification des éléments à supprimer</param>
        public static void RemoveAll(this IList list, Predicate<object> predicate)
        {
            IEnumerable<object> ra;

            if (predicate != null)
                ra = list.Cast<object>().Where(o => predicate(o)).ToArray();
            else
                ra = list.Cast<object>().ToArray();

            foreach (var r in ra) list.Remove(r);
        }

        /// <summary>
        ///     Supprime tous les éléments de @list
        /// </summary>
        /// <param name="list"></param>
        public static void RemoveAll(this IList list)
        {
            RemoveAll(list, null);
        }


        /// <summary>
        ///     Ajoute tous les éléments de @items à @list
        /// </summary>
        /// <param name="list">Liste cible</param>
        /// <param name="items">Éléments à ajouter</param>
        public static void AddRange<TValue>(this IList<TValue> list, params TValue[] items)
        {
            list.AddRange((IEnumerable<TValue>) items);
        }
    }
}