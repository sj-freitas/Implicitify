using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Implicitify.ProxyWrapper
{
    public class ImplicitInterceptor<TInstance> : IInterceptor
    {
        private readonly TInstance _instance;
        private readonly IDictionary<MethodInfo, MethodInfo> _methodsCache;

        public ImplicitInterceptor(TInstance instance,
            IDictionary<MethodInfo, MethodInfo> methodsCache)
        {
            _instance = instance;
            _methodsCache = methodsCache;
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
                    $"{invocation.Method} is not implemented by {typeof(TInstance)}!");
            }

            if (interfaceMethod.IsGenericMethod)
            {
                var genericParameters = interfaceMethod.GetGenericArguments();

                implementedMethod = implementedMethod
                    .MakeGenericMethod(genericParameters);
            }

            invocation.ReturnValue = implementedMethod
                .Invoke(_instance, invocation.Arguments);
        }
    }
}
