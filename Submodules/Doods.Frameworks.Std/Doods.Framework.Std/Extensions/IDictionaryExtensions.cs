using System;
using System.Collections.Generic;
using System.Linq;

namespace Doods.Framework.Std.Extensions
{
    /// <summary>
    ///     Extensions du type <see cref="T:IDictionary" />
    /// </summary>
    public static class IDictionaryExtensions
    {
        /// <summary>
        ///     Retourne une valeur existante par clé, en la créant via un sélecteur fourni par l'appelant
        ///     si elle n'existe pas déjà dans le dictionnaire
        /// </summary>
        /// <typeparam name="TKey">Type de clé</typeparam>
        /// <typeparam name="TValue">Type de valeur</typeparam>
        /// <param name="dictionary">Dictionnaire</param>
        /// <param name="key">Clé</param>
        /// <param name="valueSelector">
        ///     Sélecteur de valeur, appelé si la clé n'existe pas dans le dictionnaire. La clé est passée
        ///     en paramètre.
        /// </param>
        /// <param name="create">Indique si les créations (appels à @valueSelector) sont autorisées</param>
        /// <returns>Valeur correspondant à la clé</returns>
        public static TValue GetOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key,
            Func<TKey, TValue> valueSelector, bool create = true)
        {
            TValue value;

            if (!dictionary.TryGetValue(key, out value))
                if (create)
                {
                    value = valueSelector(key);
                    dictionary.Add(key, value);
                }

            return value;
        }

        public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key,
            TValue @default = default)
        {
            TValue value;
            if (!dictionary.TryGetValue(key, out value)) value = @default;
            return value;
        }


        public static TValue GetOrCreate<TValue>(this IDictionary<string, object> dictionary, string key,
            Func<string, TValue> valueSelector, bool create = true)
        {
            object temp;
            var value = default(TValue);

            if (dictionary.TryGetValue(key, out temp))
                value = (TValue) temp;
            {
                if (create)
                {
                    value = valueSelector(key);
                    dictionary.Add(key, value);
                }
            }

            return value;
        }


        public static TValue Get<TValue>(this IDictionary<string, object> dictionary, string key)
        {
            var value = default(TValue);
            object temp;

            if (dictionary.TryGetValue(key, out temp))
                value = (TValue) temp;

            return value;
        }

        /// <summary>
        ///     Ajoute une liste de (clé, valeur) à un dictionnaire
        /// </summary>
        /// <typeparam name="TKey">Type de clé</typeparam>
        /// <typeparam name="TValue">Type de valeur</typeparam>
        /// <param name="dictionary">Dictionnaire</param>
        /// <param name="values">Paires de (clé, valeur) à ajouter</param>
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            IEnumerable<KeyValuePair<TKey, TValue>> values)
        {
            foreach (var pair in values)
                dictionary.Add(pair);
        }

        /// <summary>
        ///     Ajoute une liste de (clé, valeur) à un dictionnaire
        /// </summary>
        /// <typeparam name="TKey">Type de clé</typeparam>
        /// <typeparam name="TValue">Type de valeur</typeparam>
        /// <param name="dictionary">Dictionnaire</param>
        /// <param name="values">Valeurs</param>
        /// <param name="keySelector">Sélecteur de clé, pour une valeur donnée</param>
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<TValue> values,
            Func<TValue, TKey> keySelector)
        {
            foreach (var value in values)
                dictionary.Add(keySelector(value), value);
        }

        /// <summary>
        ///     Définit une liste de (clé, valeur) sur un dictionnaire
        /// </summary>
        /// <typeparam name="TKey">Type de clé</typeparam>
        /// <typeparam name="TValue">Type de valeur</typeparam>
        /// <param name="dictionary">Dictionnaire</param>
        /// <param name="values">Paires de (clé, valeur) à définir</param>
        public static void SetRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            IEnumerable<KeyValuePair<TKey, TValue>> values)
        {
            foreach (var pair in values)
                dictionary[pair.Key] = pair.Value;
        }

        /// <summary>
        ///     Définit une liste de (clé, valeur) sur un dictionnaire
        /// </summary>
        /// <typeparam name="TKey">Type de clé</typeparam>
        /// <typeparam name="TValue">Type de valeur</typeparam>
        /// <param name="dictionary">Dictionnaire</param>
        /// <param name="values">Paires de (clé, valeur) à définir</param>
        public static void SetRange<TKey, TValue>(this IDictionary<TKey, IEnumerable<TValue>> dictionary,
            IEnumerable<IGrouping<TKey, TValue>> values)
        {
            foreach (var value in values)
                dictionary[value.Key] = value;
        }


        /// <summary>
        ///     Définit une liste de (clé, valeur) sur un dictionnaire
        /// </summary>
        /// <typeparam name="TKey">Type de clé</typeparam>
        /// <typeparam name="TValue">Type de valeur</typeparam>
        /// <param name="dictionary">Dictionnaire</param>
        /// <param name="values">Valeurs</param>
        /// <param name="keySelector">Sélecteur de clé, pour une valeur donnée</param>
        public static void SetRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<TValue> values,
            Func<TValue, TKey> keySelector)
        {
            foreach (var value in values)
                dictionary[keySelector(value)] = value;
        }

        public static void AddOrSet<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key)) dictionary[key] = value;
            else dictionary.Add(key, value);
        }
    }
}