using System;

namespace Implicitify
{
    public interface IProxyWrapperFactory<TInterface> where TInterface : class
    {
        TInterface Wrap<T>(T implicitImplementation);
    }

    public interface IProxyWrapperFactory
    {
        object Wrap(Type interfaceType, object implicitImplementation);
    }
}
