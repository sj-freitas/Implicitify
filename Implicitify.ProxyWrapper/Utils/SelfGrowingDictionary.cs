using System;
using System.Collections.Generic;

namespace Implicitify.ProxyWrapper.Utils
{
    public static class Extensions
    {
        public static IIndexedGetter<TKey, TValue> ToSelfGrowing<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary, Func<TKey, TValue> getValueFromKey)
        {
            return new SelfGrowingDictionary<TKey, TValue>(dictionary, getValueFromKey);
        }
    }

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
