using System.Collections.Generic;

namespace Doods.Framework.Std.Utilities
{
    public static class CollectionUtils
    {
        /// <summary>
        ///     Determines whether the collection is <c>null</c> or empty.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns>
        ///     <c>true</c> if the collection is <c>null</c> or empty; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty<T>(ICollection<T> collection)
        {
            if (collection != null) return collection.Count == 0;

            return true;
        }
    }
}