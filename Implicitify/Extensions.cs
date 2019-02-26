using Castle.DynamicProxy;
using System;

namespace Implicitify
{
    public static class Extensions
    {
        /// <summary>
        /// Wraps the current instance as the specific interface by implementing it
        /// implicitly. However, a NotImplementedException exception is thrown if the
        /// interface method is not implemented by the instance.
        /// </summary>
        /// <typeparam name="TInterface">The interface type to</typeparam>
        /// <param name="instance">The instance to wrap.</param>
        /// <returns>The wrapped instance as the specific interface.</returns>
        public static TInterface As<TInterface>(this object instance)
            where TInterface : class
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            return instance.As<TInterface>(new ProxyGenerator());
        }

        /// <summary>
        /// Wraps the current instance as the specific interface by implementing it
        /// implicitly. However, a NotImplementedException exception is thrown if the
        /// interface method is not implemented by the instance.
        /// </summary>
        /// <typeparam name="TInterface">The interface type to</typeparam>
        /// <param name="instance">The instance to wrap.</param>
        /// <param name="generator">The proxy generator that handles the interception.</param>
        /// <returns>The wrapped instance as the specific interface.</returns>
        public static TInterface As<TInterface>(this object instance, IProxyGenerator generator)
            where TInterface : class
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }
            if (generator == null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            return instance.As<TInterface>(new ProxyWrapperFactory(generator));
        }

        /// <summary>
        /// Wraps the current instance as the specific interface by implementing it
        /// implicitly. However, a NotImplementedException exception is thrown if the
        /// interface method is not implemented by the instance.
        /// </summary>
        /// <typeparam name="TInterface">The interface type to</typeparam>
        /// <param name="instance">The instance to wrap.</param>
        /// <param name="factory">The factory engine that generates the
        /// wrapped instance.</param>
        /// <returns>The wrapped instance as the specific interface.</returns>
        public static TInterface As<TInterface>(this object instance,
            IProxyWrapperFactory factory) where TInterface : class
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (!typeof(TInterface).IsInterface)
            {
                throw new InvalidOperationException(
                    $"{typeof(TInterface).Name} is not an interface!");
            }

            return factory.Wrap(typeof(TInterface), instance) as TInterface;
        }
    }
}
