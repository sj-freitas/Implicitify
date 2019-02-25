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
        private static readonly IEqualityComparer<MethodInfo> _compareMethodSimple =
            new MethodInfoNameParametersEqualityComparer();
        private static readonly IIndexedGetter<Type, IDictionary<MethodInfo, MethodInfo>> _methodInfos =
            new Dictionary<Type, IDictionary<MethodInfo, MethodInfo>>()
                .ToSelfGrowing((type) => MethodInfoCacheHelper
                    .CreateMethodsCache(type, _compareMethodSimple));

        private readonly IProxyGenerator _generator;

        public ProxyWrapperFactory(IProxyGenerator generator)
        {
            _generator = generator;
        }

        public object Wrap(Type interfaceType, object implicitImplementation)
        {
            if (implicitImplementation == null)
            {
                throw new ArgumentNullException(nameof(implicitImplementation));
            }

            var dictionary = _methodInfos[implicitImplementation.GetType()];
            var wrapper = new DynamicImplicitInterceptor(
                implicitImplementation, dictionary, this);

            return _generator.CreateInterfaceProxyWithoutTarget(interfaceType, wrapper);
        }
    }

    public static class Helper
    {
        public static TInterface As<TInterface>(this IProxyWrapperFactory factory, object instance)
            where TInterface : class
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return factory.Wrap(typeof(TInterface), instance) as TInterface;
        }

        public static TInterface As<TInterface>(this object instance)
            where TInterface : class
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            var generator = new ProxyGenerator();
            var proxyFactory = new ProxyWrapperFactory(generator);

            return proxyFactory.As<TInterface>(instance);
        }
    }
}
