using System;
using System.Collections.Generic;

namespace Implicitify.Utils
{
    public static class Extensions
    {
        public static IIndexedGetter<TKey, TValue> ToSelfGrowing<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary, Func<TKey, TValue> getValueFromKey)
        {
            return new SelfGrowingDictionary<TKey, TValue>(dictionary, getValueFromKey);
        }
    }
}
