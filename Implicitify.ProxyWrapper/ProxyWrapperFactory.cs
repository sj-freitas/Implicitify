using Castle.DynamicProxy;
using Implicitify.ProxyWrapper.MethodInfoHelpers;
using Implicitify.ProxyWrapper.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Implicitify.ProxyWrapper
{
    public class ProxyWrapperFactory : IProxyWrapperFactory
    {
        private readonly IIndexedGetter<Type, IDictionary<MethodInfo, MethodInfo>> _methodInfosCache;
        private readonly IProxyGenerator _generator;

        public ProxyWrapperFactory(IProxyGenerator generator)
            : this(generator, new MethodInfoEqualityComparer())
        {
        }

        public ProxyWrapperFactory(IProxyGenerator generator,
            IEqualityComparer<MethodInfo> compareMethodInfo)
            : this(generator, MethodInfoCacheHelper.CreateCache(compareMethodInfo))
        {
        }

        public ProxyWrapperFactory(IProxyGenerator generator,
            IIndexedGetter<Type, IDictionary<MethodInfo, MethodInfo>> methodInfoCache)
        {
            _generator = generator;
            _methodInfosCache = methodInfoCache;
        }

        public object Wrap(Type interfaceType, object implicitImplementation)
        {
            if (implicitImplementation == null)
            {
                throw new ArgumentNullException(nameof(implicitImplementation));
            }

            var dictionary = _methodInfosCache[implicitImplementation.GetType()];
            var wrapper = new ImplicitInterceptor(
                implicitImplementation, dictionary, this);

            return _generator.CreateInterfaceProxyWithoutTarget(interfaceType, wrapper);
        }
    }
}
