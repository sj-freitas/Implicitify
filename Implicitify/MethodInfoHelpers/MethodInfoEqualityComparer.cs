using System;
using System.Collections.Generic;
using System.Reflection;

namespace Implicitify.MethodInfoHelpers
{
    public class MethodInfoEqualityComparer : IEqualityComparer<MethodInfo>
    {
        public bool Equals(MethodInfo x, MethodInfo y)
        {
            return string.Equals(
                x.ToStringKey(),
                y.ToStringKey(),
                StringComparison.InvariantCulture);
        }

        public int GetHashCode(MethodInfo methodInfo)
        {
            if (methodInfo == null)
            {
                throw new ArgumentNullException(nameof(methodInfo));
            }

            return methodInfo
                .ToStringKey()
                .GetHashCode();
        }
    }
}
