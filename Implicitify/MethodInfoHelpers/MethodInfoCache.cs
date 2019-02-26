using Implicitify.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Implicitify.MethodInfoHelpers
{
    public static class MethodInfoCacheHelper
    {
        /// <summary>
        /// Caches a method info of a specific type from an equality comparer.
        /// </summary>
        public static IDictionary<MethodInfo, MethodInfo> CreateMethodsCache(
            Type type, IEqualityComparer<MethodInfo> equalityComparer)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type
                .GetMethods()
                .ToDictionary(t => t, equalityComparer);
        }

        public static IIndexedGetter<Type, IDictionary<MethodInfo, MethodInfo>> CreateCache(
            IEqualityComparer<MethodInfo> compareMethodInfo)
        {
            return new Dictionary<Type, IDictionary<MethodInfo, MethodInfo>>()
                .ToSelfGrowing((type) => CreateMethodsCache(type, compareMethodInfo));
        }
    }
}
