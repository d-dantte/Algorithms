using System;
using System.Collections.Generic;
using System.Linq;

namespace Codility01.StringMapImplementation
{
    public class StringMap<TValue> : IStringMap<TValue>
        where TValue : class
    {
        private readonly Dictionary<string, TValue> _internalDictionary = new Dictionary<string, TValue>();

        #region List Implementation
        //private readonly List<KeyValuePair<string, TValue>> _internalList = new List<KeyValuePair<string, TValue>>();
        #endregion

        /// <summary> Returns number of elements in a map</summary>
        public int Count => _internalDictionary.Count;

        #region list implementation
        //public int Count => _internalList.Count;
        #endregion

        /// <summary>
        /// If <c>GetValue</c> method is called but a given key is not in a map then <c>DefaultValue</c> is returned.
        /// </summary>
        public TValue DefaultValue { get; set; }

        /// <summary>
        /// Adds a given key and value to a map.
        /// If the given key already exists in a map, then the value associated with this key should be overriden.
        /// </summary>
        /// <returns>true if the value for the key was overriden otherwise false</returns>
        /// <exception cref="System.ArgumentNullException">If the key is null</exception>
        /// <exception cref="System.ArgumentException">If the key is an empty string</exception>
        /// <exception cref="System.ArgumentNullException">If the value is null</exception>
        public bool AddElement(string key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            else if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Empty Key", nameof(key));

            var wasOverriden = _internalDictionary.ContainsKey(key);
            _internalDictionary[key] = value ?? throw new ArgumentNullException(nameof(value));

            return wasOverriden;

            #region  List implementation
            //var map = _internalList.FirstOrDefault(kvp => kvp.Key.Equals(key, StringComparison.InvariantCulture));
            //var wasOverriden = !default(KeyValuePair<string, TValue>).Equals(map);
            //if(wasOverriden)
            //{
            //    _internalList.Remove(map);
            //    _internalList.Add(new KeyValuePair<string, TValue>(key, value));
            //    return wasOverriden;
            //}
            //else
            //{
            //    _internalList.Add(new KeyValuePair<string, TValue>(key, value));
            //    return wasOverriden;
            //}
            #endregion
        }

        /// <summary>
        /// Removes a given key and associated value from a map.
        /// </summary>
        /// <returns>true if the key was in the map and was removed otherwise false</returns>
        /// <exception cref="System.ArgumentNullException">If the key is null</exception>
        /// <exception cref="System.ArgumentException">If the key is an empty string</exception>
        public bool RemoveElement(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            else if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Empty Key", nameof(key));

            return _internalDictionary.Remove(key);

            #region List implementation
            //var stringMap = _internalList.FirstOrDefault(map => map.Key.Equals(key, StringComparison.InvariantCulture));
            //return _internalList.Remove(stringMap);
            #endregion
        }

        /// <summary>
        /// Returns the value associated with a given key.
        /// </summary>
        /// <returns>The value associated with a given key or <c>DefaultValue</c> if the key does not exist in a map</returns>
        /// <exception cref="System.ArgumentNullException">If a key is null</exception>
        /// <exception cref="System.ArgumentException">If a key is an empty string</exception>
        public TValue GetValue(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            else if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Empty Key", nameof(key));

            if (!_internalDictionary.ContainsKey(key))
                return DefaultValue;

            return _internalDictionary[key];

            #region List Implementation
            //var stringMap = _internalList.FirstOrDefault(map => map.Key.Equals(key, StringComparison.InvariantCulture));
            //if (!default(KeyValuePair<string, TValue>).Equals(stringMap))
            //    return stringMap.Value;

            //return DefaultValue;
            #endregion
        }
    }
}
