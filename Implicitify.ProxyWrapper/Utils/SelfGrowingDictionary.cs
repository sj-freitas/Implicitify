using System;
using System.Collections.Generic;

namespace Implicitify.ProxyWrapper.Utils
{
    /// <summary>
    /// An IndexGetter that automatically generates a value derrived from the
    /// key when it's not present. The getValueFromKey function will supply
    /// the derrived value from the key.
    /// </summary>
    /// <typeparam name="TKey">The key type.</typeparam>
    /// <typeparam name="TValue">The value type.</typeparam>
    public class SelfGrowingDictionary<TKey, TValue> : IIndexedGetter<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> _dictionary;
        private readonly Func<TKey, TValue> _getValueFromKey;

        public SelfGrowingDictionary(IDictionary<TKey, TValue> dictionary,
            Func<TKey, TValue> getValueFromKey)
        {
            _dictionary = dictionary;
            _getValueFromKey = getValueFromKey;
        }

        public TValue this[TKey key]
        {
            get
            {
                if (!_dictionary.TryGetValue(key, out TValue value))
                {
                    value = _getValueFromKey(key);
                    _dictionary[key] = value;
                }

                return value;
            }
        }
    }
}
