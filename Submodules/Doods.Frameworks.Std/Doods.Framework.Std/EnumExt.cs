using System.Collections.Generic;
using System.Linq;

namespace Doods.Framework.Std
{
    /// <summary>
    ///     Méthodes utilitaires et extensions autour de <see cref="T:System.Collections.Generic.IEnumerable`1" />
    /// </summary>
    public static class EnumExt
    {
        /// <summary>
        ///     Retourne une énumération vide de type <typeparamref name="TValue" />
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static IEnumerable<TValue> Empty<TValue>()
        {
            yield break;
        }

        /// <summary>
        ///     Retourne une énumération vide de type <typeparamref name="TValue" />
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static IReadOnlyCollection<TValue> EmptyReadOnly<TValue>()
        {//toto
            return Empty<TValue>().ToList();
        }

        /// <summary>
        ///     Retourne un lookup vide de type <typeparamref name="TValue" />, indexé par un type <typeparamref name="TKey" />
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        public static ILookup<TKey, TValue> EmptyLookup<TValue, TKey>()
        {
            return Empty<TValue>().ToLookup(v => default(TKey));
        }


        /// <summary>
        ///     Retourne une énumération contenant un seul élément de type <typeparamref name="TValue" />, sauf s'il est null -
        ///     dans ce cas, retourne une énumration vide de type
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static IEnumerable<TValue> SingleNotNull<TValue>(TValue item) where TValue : class
        {
            if (item == null) yield break;
            yield return item;
        }

        /// <summary>
        ///     Retourne une énumération contenant un seul élément de type <typeparamref name="TValue" />
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static IEnumerable<TValue> Single<TValue>(TValue item)
        {
            yield return item;
        }

        /// <summary>
        ///     Retourne une énumération contenant un seul élément de type <typeparamref name="TValue" />
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static IReadOnlyCollection<TValue> SingleReadOnly<TValue>(TValue item)
        {
            return Single(item).ToList();
        }

        /// <summary>
        ///     Retourne une énumération constituée des paramètres passés par l'appelant
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static IEnumerable<TValue> Enum<TValue>(params TValue[] items)
        {
            return items;
        }

        /// <summary>
        ///     Retourne une énumération constituée des paramètres passés par l'appelant, en excluant les paramètres null
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static IEnumerable<TValue> EnumNotNull<TValue>(params TValue[] items) where TValue : class
        {
            foreach (var i in items)
                if (i != null)
                    yield return i;
        }
    }
}