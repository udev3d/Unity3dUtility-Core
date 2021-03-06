using System;
using System.Collections.Generic;
using System.Security.Cryptography;

using Random = UnityEngine.Random;

namespace MuffinDev.Core
{

    ///<summary>
    /// Extensions for List or IList objects.
    ///</summary>
    public static class ListExtension
	{

        /// <summary>
        /// Adds the given item to the list only if it's not already in.
        /// </summary>
        /// <param name="_Item">The item to add.</param>
        /// <returns>Returns true if the item has been added, or false if the item is already in the list.</returns>
        public static bool AddOnce<T>(this IList<T> _List, T _Item)
        {
            if (_List.Contains(_Item))
                return false;

            _List.Add(_Item);
            return true;
        }

        /// <summary>
        /// Checks if the given index is in this list's range.
        /// </summary>
        public static bool IsInRange<T>(this IList<T> _List, int _Index)
        {
            return _Index > 0 && _Index < _List.Count;
        }

        /// <summary>
        /// A shortcut for using string.Join() method on lists.
        /// </summary>
        /// <param name="_Separator">The character that separates each elements in the output text.</param>
        /// <returns>Returns the output text, using string.Join() method.</returns>
        public static string Join<T>(this IList<T> _List, string _Separator)
        {
            return string.Join(_Separator, _List);
        }

        /// <summary>
        /// Creates a sub-list of the given list that contains only the elements that should be displayed using the given pagination
        /// settings.
        /// </summary>
        /// <param name="_List">The list that is paginated.</param>
        /// <param name="_Page">The current page.</param>
        /// <param name="_NbElementsPerPage">The number of elements displayed per page.</param>
        /// <returns>Returns the sub-list of the elements to display.</returns>
        public static T[] Paginate<T>(this IList<T> _List, int _Page, int _NbElementsPerPage = Pagination.DEFAULT_NB_ELEMENTS_PER_PAGE)
        {
            return Pagination.Paginate(_List, _Page, _NbElementsPerPage);
        }

        /// <summary>
        /// Creates a sub-list of the given list that contains only the elements that should be displayed using the given pagination
        /// settings.
        /// </summary>
        /// <param name="_List">The list that is paginated.</param>
        /// <param name="_Pagination">The Pagination infos of the operation.</param>
        /// <param name="_Page">The current page.</param>
        /// <param name="_NbElementsPerPage">The number of elements displayed per page.</param>
        /// <returns>Returns the sub-list of the elements to display.</returns>
        public static T[] Paginate<T>(this IList<T> _List, out Pagination _Pagination, int _Page, int _NbElementsPerPage = Pagination.DEFAULT_NB_ELEMENTS_PER_PAGE)
        {
            return Pagination.Paginate(_List, out _Pagination, _Page, _NbElementsPerPage);
        }

        /// <summary>
        /// Shuffles the list in-place, using UnityEngine.Random().
        /// Original version at https://stackoverflow.com/questions/273313/randomize-a-listt
        /// </summary>
        public static void Shuffle<T>(this IList<T> _List)
        {
            int n = _List.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                T value = _List[k];
                _List[k] = _List[n];
                _List[n] = value;
            }
        }

        /// <summary>
        /// Shuffles the list in-place, using Cryptography random number generators.
        /// This method is slower than Shuffle(), but provides a better randomness quality.
        /// Original version at https://stackoverflow.com/questions/273313/randomize-a-listt
        /// </summary>
        public static void ShuffleCrypto<T>(this IList<T> _List)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = _List.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = _List[k];
                _List[k] = _List[n];
                _List[n] = value;
            }
        }

    }

}