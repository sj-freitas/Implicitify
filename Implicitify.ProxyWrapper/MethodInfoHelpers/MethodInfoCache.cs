using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Implicitify.ProxyWrapper.MethodInfoHelpers
{
    public static class MethodInfoCache<T>
    {
        public static readonly IEqualityComparer<MethodInfo> _equalityComparer =
            new MethodInfoSignatureEqualityComparer();

        public static IDictionary<MethodInfo, MethodInfo> Methods =
            MethodInfoCacheHelper.CreateMethodsCache(typeof(T), _equalityComparer);
    }

    public static class MethodInfoCacheHelper
    {
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
    }
}
