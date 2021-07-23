using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Doods.Framework.Std.Extensions
{
    /// <summary>
    ///     Extensions du type <see cref="T:IEnumerable`1" />
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        ///     Null ou vide
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e">Énumération</param>
        /// <returns>true si l'enum est null ou vide</returns>
        public static bool IsEmpty<T>(this IEnumerable<T> e)
        {
            return e == null || !e.Any();
        }

        /// <summary>
        ///     Au moins deux éléments dans la liste
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e"></param>
        /// <returns></returns>
        private static bool AuMoinsDeux<T>(this IEnumerable<T> e)
        {
            if (e.IsNotEmpty())
            {
                var i = 1;
                var enumerator = e.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    if (i == 2) return true;
                    i++;
                }
            }

            return false;
        }

        public static bool Many<T>(this IEnumerable<T> e, Func<T, bool> keySelector)
        {
            return e.Where(keySelector).AuMoinsDeux();
        }

        public static bool Many<T>(this IEnumerable<T> e)
        {
            return e.AuMoinsDeux();
        }


        public static IEnumerable<X> WhereTypeIs<X, T>(this IEnumerable<T> e)
        {
            if (e.IsEmpty()) return EnumExt.Empty<X>();
            return e.Where(a => a is X).Cast<X>().ToList();
        }

        /// <summary>
        ///     Non null et non vide
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e">Énumération</param>
        /// <returns>true si l'enum n'est pas null, et comporte au moins un élément</returns>
        public static bool IsNotEmpty<T>(this IEnumerable<T> e)
        {
            return e != null && e.Any();
        }


        /// <summary>
        ///     Crée un dictionnaire en lecture seule à partir d'une énumération
        /// </summary>
        /// <typeparam name="T">Type des éléments de l'énumération</typeparam>
        /// <typeparam name="TKey">Type de clé</typeparam>
        /// <typeparam name="TValue">Type de valeur</typeparam>
        /// <param name="e">Énumération</param>
        /// <param name="keySelector">Sélecteur de clé à partir d'un élément</param>
        /// <param name="elementSelector">Sélecteur de valeur à partir d'un élément</param>
        /// <returns>Dictionnaire</returns>
        public static IReadOnlyDictionary<TKey, TValue> ToReadOnlyDictionary<T, TKey, TValue>(this IEnumerable<T> e,
            Func<T, TKey> keySelector, Func<T, TValue> elementSelector)
        {
            return new ReadOnlyDictionary<TKey, TValue>(e.ToDictionary(keySelector, elementSelector));
        }

        /// <summary>
        ///     Crée un dictionnaire en lecture seule à partir d'une énumération
        /// </summary>
        /// <typeparam name="T">Type des éléments de l'énumération</typeparam>
        /// <typeparam name="TKey">Type de clé</typeparam>
        /// <param name="e">Énumération</param>
        /// <param name="keySelector">Sélecteur de clé à partir d'un élément</param>
        /// <returns>Dictionnaire</returns>
        public static IReadOnlyDictionary<TKey, T> ToReadOnlyDictionary<T, TKey>(this IEnumerable<T> e,
            Func<T, TKey> keySelector)
        {
            return new ReadOnlyDictionary<TKey, T>(e.ToDictionary(keySelector, i => i));
        }

        /// <summary>
        ///     Découpe une énumération en plusieurs énumérations, de même taille
        /// </summary>
        /// <typeparam name="T">Type des éléments de l'énumération</typeparam>
        /// <param name="source">Énumération</param>
        /// <param name="chunksize">Taille</param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunksize)
        {
            var pos = 0;
            while (source.Skip(pos).Any())
            {
                yield return source.Skip(pos).Take(chunksize);
                pos += chunksize;
            }
        }

        /// <summary>
        ///     Découpe une énumération en plusieurs énumérations, de même taille
        /// </summary>
        /// <typeparam name="T">Type des éléments de l'énumération</typeparam>
        /// <param name="source">Énumération</param>
        /// <param name="chunksize">Taille</param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> SplitOnLineContains<T>(this IEnumerable<T> source, string split)
        {
            var result = new List<IEnumerable<T>>();
            var pos = new List<int>();

            var s = source.ToList();

            foreach (var t in s)
                if (t.ToString().Contains(split))
                    pos.Add(s.IndexOf(t));

            if (pos.IsEmpty())
            {
                result.Add(source);
            }

            else
            {
                var temp = 0;
                foreach (var p in pos)
                {
                    result.Add(source.Skip(temp).Take(p - temp - 1));
                    temp = p + 1;
                }
            }

            return result;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            foreach (var element in source)
                if (seenKeys.Add(keySelector(element)))
                    yield return element;
        }

        /// <summary>
        ///     Concatène les membres de l'énumération, en utilisation le séparateur @separateur
        /// </summary>
        /// <typeparam name="T">Type des éléments de l'énumération</typeparam>
        /// <param name="source">Énumération</param>
        /// <param name="separateur">Séparateur</param>
        /// <returns></returns>
        public static string Join<T>(this IEnumerable<T> source, string separateur)
        {
            return source.IsEmpty() ? string.Empty : string.Join(separateur, source);
        }

        /// <summary>
        ///     Recherche récursive depth-first
        /// </summary>
        /// <typeparam name="TIn">Type des éléments de l'énum entrante</typeparam>
        /// <typeparam name="TOut">Type des éléments de l'énum résultante</typeparam>
        /// <typeparam name="TKey">Type des clés</typeparam>
        /// <param name="items">Énumération initiale</param>
        /// <param name="childrenSelector">Sélecteur d'enfants, appelé pour chaque élément de l'énumération en cours de traitement</param>
        /// <param name="valueSelector">Sélecteur de valeur, appelé pour chaque élément traité</param>
        /// <param name="keySelector">
        ///     Sélecteur de clé optionnel - si défini, seuls les éléments ayant des clés distinctes seront
        ///     retournés
        /// </param>
        /// <returns>Énumération "plate" résultante</returns>
        /// <remarks>
        ///     Parcours depth first - une entrée organisée ainsi:
        ///     a
        ///     b       c
        ///     d       e
        ///     Donnera cette liste:
        ///     a, b, d, e, c
        /// </remarks>
        public static IEnumerable<TOut> SearchDepthFirst<TIn, TOut, TKey>(this IEnumerable<TIn> items,
            Func<TIn, IEnumerable<TIn>> childrenSelector, Func<TIn, TOut> valueSelector, Func<TIn, TKey> keySelector,
            Predicate<TOut> predicate = null)
        {
            if (items.IsNotEmpty())
            {
                var keys = keySelector != null ? new HashSet<TKey>() : null;

                var stack = new Stack<TIn>(items.Reverse());

                while (stack.Count > 0)
                {
                    var i = stack.Pop();

                    if (keySelector == null || keys.Add(keySelector(i)))
                    {
                        var value = valueSelector(i);

                        if (predicate == null || predicate(value))
                        {
                            yield return value;

                            var children = childrenSelector(i);

                            if (children != null)
                                foreach (var j in children.Reverse())
                                    stack.Push(j);
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     Recherche récursive depth-first
        /// </summary>
        /// <typeparam name="TIn">Type des éléments de l'énum entrante</typeparam>
        /// <typeparam name="TOut">Type des éléments de l'énum résultante</typeparam>
        /// <param name="items">Énumération initiale</param>
        /// <param name="childrenSelector">Sélecteur d'enfants, appelé pour chaque élément de l'énumération en cours de traitement</param>
        /// <param name="valueSelector">Sélecteur de valeur, appelé pour chaque élément traité</param>
        /// <returns>Énumération "plate" résultante</returns>
        /// <remarks>
        ///     Parcours depth first - une entrée organisée ainsi:
        ///     a
        ///     b       c
        ///     d       e
        ///     Donnera cette liste:
        ///     a, b, d, e, c
        /// </remarks>
        public static IEnumerable<TOut> SearchDepthFirst<TIn, TOut>(this IEnumerable<TIn> items,
            Func<TIn, IEnumerable<TIn>> childrenSelector, Func<TIn, TOut> valueSelector)
        {
            return items.SearchDepthFirst<TIn, TOut, int>(childrenSelector, valueSelector, null);
        }

        /// <summary>
        ///     Recherche récursive depth-first
        /// </summary>
        /// <typeparam name="TValue">Type des éléments de l'énum</typeparam>
        /// <param name="items">Énumération initiale</param>
        /// <param name="childrenSelector">Sélecteur d'enfants, appelé pour chaque élément de l'énumération en cours de traitement</param>
        /// <returns>Énumération "plate" résultante</returns>
        /// <remarks>
        ///     Parcours depth first - une entrée organisée ainsi:
        ///     a
        ///     b       c
        ///     d       e
        ///     Donnera cette liste:
        ///     a, b, d, e, c
        /// </remarks>
        public static IEnumerable<TValue> SearchDepthFirst<TValue>(this IEnumerable<TValue> items,
            Func<TValue, IEnumerable<TValue>> childrenSelector)
        {
            return items.SearchDepthFirst(childrenSelector, i => i);
        }


        /// <summary>
        ///     Recherche récursive breadth-first
        /// </summary>
        /// <typeparam name="TIn">Type des éléments de l'énum entrante</typeparam>
        /// <typeparam name="TOut">Type des éléments de l'énum résultante</typeparam>
        /// <typeparam name="TKey">Type des clés</typeparam>
        /// <param name="items">Énumération initiale</param>
        /// <param name="childrenSelector">Sélecteur d'enfants, appelé pour chaque élément de l'énumération en cours de traitement</param>
        /// <param name="valueSelector">Séleccteur de valeur, appelé pour chaque élément traité</param>
        /// <param name="keySelector">
        ///     Sélecteur de clé optionnel - si défini, seuls les éléments ayant des clés distinctes seront
        ///     retournés
        /// </param>
        /// <returns>Énumération "plate" résultante</returns>
        /// <remarks>
        ///     Parcours breadth first - une entrée organisée ainsi:
        ///     a
        ///     b       c
        ///     d       e
        ///     Donnera cette liste:
        ///     a, b, c, d, e
        /// </remarks>
        public static IEnumerable<TOut> SearchBreadthFirst<TIn, TOut, TKey>(this IEnumerable<TIn> items,
            Func<TIn, IEnumerable<TIn>> childrenSelector, Func<TIn, TOut> valueSelector, Func<TIn, TKey> keySelector,
            Predicate<TOut> predicate = null)
        {
            if (items.IsNotEmpty())
            {
                var keys = keySelector != null ? new HashSet<TKey>() : null;

                var q = new Queue<TIn>(items);

                while (q.Count > 0)
                {
                    var i = q.Dequeue();

                    if (keySelector == null || keys.Add(keySelector(i)))
                    {
                        var value = valueSelector(i);

                        if (predicate == null || predicate(value))
                        {
                            yield return value;

                            var children = childrenSelector(i);

                            if (children != null)
                                foreach (var j in children)
                                    q.Enqueue(j);
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     Recherche récursive breadth-first
        /// </summary>
        /// <typeparam name="TIn">Type des éléments de l'énum entrante</typeparam>
        /// <typeparam name="TOut">Type des éléments de l'énum résultante</typeparam>
        /// <param name="items">Énumération initiale</param>
        /// <param name="childrenSelector">Sélecteur d'enfants, appelé pour chaque élément de l'énumération en cours de traitement</param>
        /// <param name="valueSelector">Séleccteur de valeur, appelé pour chaque élément traité</param>
        /// <returns>Énumération "plate" résultante</returns>
        /// <remarks>
        ///     Parcours breadth first - une entrée organisée ainsi:
        ///     a
        ///     b       c
        ///     d       e
        ///     Donnera cette liste:
        ///     a, b, c, d, e
        /// </remarks>
        public static IEnumerable<TOut> SearchBreadthFirst<TIn, TOut>(this IEnumerable<TIn> items,
            Func<TIn, IEnumerable<TIn>> childrenSelector, Func<TIn, TOut> valueSelector)
        {
            return items.SearchBreadthFirst<TIn, TOut, int>(childrenSelector, valueSelector, null);
        }

        /// <summary>
        ///     Recherche récursive breadth-first
        /// </summary>
        /// <typeparam name="TValue">Type des éléments de l'énum</typeparam>
        /// <param name="items">Énumération initiale</param>
        /// <param name="childrenSelector">Sélecteur d'enfants, appelé pour chaque élément de l'énumération en cours de traitement</param>
        /// <returns>Énumération "plate" résultante</returns>
        /// <remarks>
        ///     Parcours breadth first - une entrée organisée ainsi:
        ///     a
        ///     b       c
        ///     d       e
        ///     Donnera cette liste:
        ///     a, b, c, d, e
        /// </remarks>
        public static IEnumerable<TValue> SearchBreadthFirst<TValue>(this IEnumerable<TValue> items,
            Func<TValue, IEnumerable<TValue>> childrenSelector)
        {
            return items.SearchBreadthFirst(childrenSelector, i => i);
        }
    }
}