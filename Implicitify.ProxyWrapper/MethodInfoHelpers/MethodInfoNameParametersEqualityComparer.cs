using System;
using System.Collections.Generic;
using System.Reflection;

namespace Implicitify.ProxyWrapper.MethodInfoHelpers
{
    public class MethodInfoNameParametersEqualityComparer : IEqualityComparer<MethodInfo>
    {
        public bool Equals(MethodInfo x, MethodInfo y)
        {
            return string.Equals(
                x.ToStringKeyNameAndParameters(),
                y.ToStringKeyNameAndParameters(),
                StringComparison.InvariantCulture);
        }

        public int GetHashCode(MethodInfo methodInfo)
        {
            if (methodInfo == null)
            {
                throw new ArgumentNullException(nameof(methodInfo));
            }

            return methodInfo
                .ToStringKeyNameAndParameters()
                .GetHashCode();
        }
    }
}
