using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Implicitify.ProxyWrapper
{
    public class DynamicImplicitInterceptor : IInterceptor
    {
        private readonly object _instance;
        private readonly IDictionary<MethodInfo, MethodInfo> _methodsCache;
        private readonly IProxyWrapperFactory _proxyWrapperFactory;

        public DynamicImplicitInterceptor(object instance,
            IDictionary<MethodInfo, MethodInfo> methodsCache,
            IProxyWrapperFactory proxyWrapperFactory)
        {
            _instance = instance;
            _methodsCache = methodsCache;
            _proxyWrapperFactory = proxyWrapperFactory;
        }

        public void Intercept(IInvocation invocation)
        {
            if (invocation == null)
            {
                throw new ArgumentNullException(nameof(invocation));
            }

            var interfaceMethod = invocation.Method;
            if (!_methodsCache.TryGetValue(interfaceMethod, out MethodInfo implementedMethod))
            {
                throw new NotImplementedException(
                    $"{invocation.Method} is not implemented by {_instance.GetType()}!");
            }

            if (interfaceMethod.IsGenericMethod)
            {
                var genericParameters = interfaceMethod.GetGenericArguments();

                implementedMethod = implementedMethod
                    .MakeGenericMethod(genericParameters);
            }

            var returnedInstance = implementedMethod
                .Invoke(_instance, invocation.Arguments);

            // Return type is not of the expected type, however, if it's
            // an interface, we can try to implicitly convert it.
            if (returnedInstance != null &&
                interfaceMethod.ReturnType.IsInterface &&                 
                !interfaceMethod
                    .ReturnType
                    .IsAssignableFrom(returnedInstance.GetType()))
            {
                invocation.ReturnValue = _proxyWrapperFactory
                    .Wrap(interfaceMethod.ReturnType, returnedInstance);
                return;
            }

            invocation.ReturnValue = returnedInstance;
        }
    }
}
