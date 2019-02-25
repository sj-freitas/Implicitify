﻿using System;
using System.Linq;
using System.Reflection;

namespace Implicitify.ProxyWrapper.MethodInfoHelpers
{
    public static class MethodInfoEqualityHelper
    {
        public static string ToStringKey(this MethodInfo methodInfo)
        {
            if (methodInfo == null)
            {
                throw new ArgumentNullException(nameof(methodInfo));
            }

            return $"{methodInfo.ToStringKeyNameAndParameters()}" +
                $".[{methodInfo.ReturnType}]";
        }

        public static string ToStringKeyNameAndParameters(this MethodInfo methodInfo)
        {
            methodInfo = methodInfo.IsGenericMethod ?
                methodInfo.GetGenericMethodDefinition() :
                methodInfo;

            return $"{methodInfo.Name}" +
                $".({string.Join(",", methodInfo.GetParameters().Select(t => t.ParameterType))})" +
                $".<{string.Join(",", methodInfo.GetGenericArguments().Select(t => t))}>";
        }
    }
}
