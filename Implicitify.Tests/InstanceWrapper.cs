using Castle.DynamicProxy;
using System;
using Xunit;

namespace Implicitify.Tests
{
    public class InstanceWrapper
    {
        private const int SECRET_OF_THE_UNIVERSE = 42;

        public interface ISomeInterface
        {
            int SomeProperty { get; }

            string ToValue();

            int GetSum(int a, int b);
        }

        public class ImplicitImplementationOfSomeInterface
        {
            public int SomeProperty => SECRET_OF_THE_UNIVERSE;

            public int GetSum(int c, int d)
            {
                return c + d;
            }
        }

        [Fact]
        public void PropertiesCanBeFetched()
        {
            // Arrange
            var interceptor = new ProxyGenerator();
            var toWrap = new ImplicitImplementationOfSomeInterface();
            var wrapped = toWrap.As<ISomeInterface>(interceptor);

            // Act
            var result = wrapped.SomeProperty;

            // Assert
            Assert.Equal(SECRET_OF_THE_UNIVERSE, result);
        }

        [Fact]
        public void MethodsCanBeFetchedEvenWithDifferentlyNamedParameters()
        {
            // Arrange
            var interceptor = new ProxyGenerator();
            var toWrap = new ImplicitImplementationOfSomeInterface();
            var wrapped = toWrap.As<ISomeInterface>(interceptor);

            // Act
            var result = wrapped.GetSum(3, 5);

            // Assert
            Assert.Equal(8, wrapped.GetSum(3, 5));
        }

        [Fact]
        public void MethodDoesNotExistThrowsException()
        {
            // Arrange
            var interceptor = new ProxyGenerator();
            var toWrap = new ImplicitImplementationOfSomeInterface();
            var wrapped = toWrap.As<ISomeInterface>(interceptor);

            // Act
            var ex = Assert.Throws<NotImplementedException>(() => wrapped.ToValue());

            // Assert
            Assert.Equal(
                "System.String ToValue() is not implemented " +
                "by Implicitify.Tests.InstanceWrapper+ImplicitImplemen" +
                "tationOfSomeInterface!", ex.Message);
        }
    }
}
