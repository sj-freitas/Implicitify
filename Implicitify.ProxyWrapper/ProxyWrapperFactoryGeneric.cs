using Castle.DynamicProxy;
using Implicitify.ProxyWrapper.MethodInfoHelpers;

namespace Implicitify.ProxyWrapper
{
    public class ProxyWrapperFactoryGeneric<TInterface> : IProxyWrapperFactory<TInterface>
        where TInterface : class
    {
        private readonly IProxyGenerator _generator;

        public ProxyWrapperFactoryGeneric(IProxyGenerator generator)
        {
            _generator = generator;
        }

        public TInterface Wrap<T>(T implicitImplementation)
        {
            var wrapper = new ImplicitInterceptor<T>(
                implicitImplementation,
                MethodInfoCache<T>.Methods);

            return _generator.CreateInterfaceProxyWithoutTarget<TInterface>(wrapper);
        }
    }

    public static class HelperGeneric
    {
        public static IProxyWrapperFactory<TInterface> For<TInterface>(this IProxyGenerator generator)
            where TInterface : class
        {
            return new ProxyWrapperFactoryGeneric<TInterface>(generator);
        }
    }
}
