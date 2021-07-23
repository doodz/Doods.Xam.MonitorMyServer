using System.Text.RegularExpressions;

namespace Doods.Framework.Std.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        ///     Vérifie si la chaîne spécifiée est un entier
        /// </summary>
        /// <param name="value">La chaîne a vérifier</param>
        /// <returns>Vrai si c'est un entier, faux sinon</returns>
        public static bool IsInteger(this string value)
        {
            int testValue;
            return int.TryParse(value, out testValue);
        }

        /// <summary>
        ///     Convertie la chaine spécifiée en un entier
        /// </summary>
        /// <param name="value">La chaîne a convertir</param>
        /// <returns>La valeur convertie si réussi, 0 sinon</returns>
        public static int ToInteger(this string value)
        {
            int testValue;
            return int.TryParse(value, out testValue) ? testValue : 0;
        }

        /// <summary>
        ///     Convertie la chaine spécifiée en un entier nullable
        /// </summary>
        /// <param name="value">La chaîne a convertir</param>
        /// <returns>La valeur convertie si réussi, null sinon</returns>
        public static int? ToNullableInteger(this string value)
        {
            int testValue;
            return int.TryParse(value, out testValue) ? (int?) testValue : null;
        }

        /// <summary>
        ///     Convertie la chaine spécifiée en un entier 64 nullable
        /// </summary>
        /// <param name="value">La chaîne a convertir</param>
        /// <returns>La valeur convertie si réussi, null sinon</returns>
        public static long? ToNullableLong(this string value)
        {
            long testValue;
            return long.TryParse(value, out testValue) ? (long?) testValue : null;
        }

        /// <summary>
        ///     Convertie la chaine spécifiée en un long
        /// </summary>
        /// <param name="value">La chaîne a convertir</param>
        /// <returns>La valeur convertie si réussi, 0 sinon</returns>
        public static long ToLong(this string value)
        {
            long testValue;
            return long.TryParse(value, out testValue) ? testValue : 0;
        }

        /// <summary>
        ///     Convetie la chaine spécifié en un <see cref="short" />
        /// </summary>
        /// <param name="value">La chaine a convertir</param>
        /// <returns>La valeur <see cref="short" /> convertie</returns>
        public static short ToShort(this string value)
        {
            short testValue;
            return short.TryParse(value, out testValue) ? testValue : (short) 0;
        }

        /// <summary>
        ///     Convertie la chaine spécifiée en un réel
        /// </summary>
        /// <param name="value">La chaîne a convertir</param>
        /// <returns>La valeur convertie si réussi, 0 sinon</returns>
        public static decimal ToDecimal(this string value)
        {
            return value.ToNullableDecimal() ?? 0;
        }


        public static double ToDouble(this string value)
        {
            double tempVal;

            value = value.Trim();

            if (string.IsNullOrEmpty(value))
                return 0d;

            if (value.Contains(",") && value.Contains("."))
                value = value.Remove(value.IndexOf(","), 1);

            value = value.Replace(',', '.');

            if (double.TryParse(value, out tempVal))
                return tempVal;

            value = value.Replace('.', ',');

            if (double.TryParse(value, out tempVal))
                return tempVal;

            return 0d;
        }

        /// <summary>
        ///     Convertie la chaine spécifiée en un réel
        ///     returnValue = 1: conversion valeur positive, -1 conversion valeur negative, 0 conversion non aboutie
        /// </summary>
        /// <param name="value">La chaîne a convertir</param>
        /// <param name="returnValue"></param>
        /// <returns>La valeur convertie si réussi, 0 sinon</returns>
        public static decimal ToDecimal(this string value, out int returnValue)
        {
            decimal tempVal;

            value = value.Trim();

            if (string.IsNullOrEmpty(value))
            {
                returnValue = 1;
                return 0M;
            }

            // suppression du point et de la virgule s'ils sont en fin fde ligne
            if (value.EndsWith(",") || value.EndsWith(".")) value = value.Substring(0, value.Length - 1);


            // suppression du separateur du miliers
            if (value.Contains(",") && value.Contains("."))
                value = value.Remove(value.IndexOf(","), 1);

            // separateur decimal égale (.)
            value = value.Replace(',', '.');
            if (decimal.TryParse(value, out tempVal))
            {
                returnValue = 1;
                if (tempVal < 0) returnValue = -1;
                return tempVal;
            }

            // separateur decimal égale (,)
            value = value.Replace('.', ',');

            if (decimal.TryParse(value, out tempVal))
            {
                returnValue = 1;
                if (tempVal < 0) returnValue = -1;
                return tempVal;
            }

            returnValue = 0;
            return 0M;
        }

        /// <summary>
        ///     Convertie la chaine spécifiée en un réel nullable
        /// </summary>
        /// <param name="value">La chaîne a convertir</param>
        /// <returns>La valeur convertie si réussi, null sinon</returns>
        public static decimal? ToNullableDecimal(this string value)
        {
            decimal tempVal;

            value = value.Trim();

            if (string.IsNullOrEmpty(value))
                return 0M;

            if (value.Contains(",") && value.Contains("."))
                value = value.Remove(value.IndexOf(","), 1);

            value = value.Replace(',', '.');

            if (decimal.TryParse(value, out tempVal))
                return tempVal;

            value = value.Replace('.', ',');

            if (decimal.TryParse(value, out tempVal))
                return tempVal;

            return null;
        }

        public static string ToUpperFirstLetter(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;
            var a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);

            return new string(a);
        }


        public static string RemoveAllWhitespace(this string s, char replace)
        {
            return RemoveAllWhitespace(s, $"{replace}");
        }

        public static string RemoveAllWhitespace(this string s, string replace)
        {
            var r = replace;
            if (string.IsNullOrWhiteSpace(replace))
                r = "_";
            return Regex.Replace(s, @"\s+", r);
        }

        public static bool ToBoolean(this string s)
        {
            bool r;
            int i;
            if (string.IsNullOrEmpty(s)) return false;
            if (bool.TryParse(s, out r)) return r;
            if (int.TryParse(s, out i)) return i != 0;

            return false;
        }
    }
}