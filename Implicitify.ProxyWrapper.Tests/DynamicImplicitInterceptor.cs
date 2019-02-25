using Xunit;

namespace Implicitify.ProxyWrapper.Tests
{
    public class DynamicImplicitInterceptor
    {
        public interface IParent
        {
            IChild GetChild();
        }

        public interface IChild
        {
            int SecretOfTheUniverse { get; }
        }

        public class DoesNotImplementParent
        {
            private readonly int _secretOfTheUniverse;

            public DoesNotImplementParent(int secretOfTheUniverse)
            {
                _secretOfTheUniverse = secretOfTheUniverse;
            }

            public DoesNotImplementChild GetChild()
            {
                return new DoesNotImplementChild(_secretOfTheUniverse);
            }
        }

        public class DoesNotImplementChild
        {
            public int SecretOfTheUniverse { get; }

            public DoesNotImplementChild(int secretOfTheUniverse)
            {
                SecretOfTheUniverse = secretOfTheUniverse;
            }
        }

        [Fact]
        public void ChildrenCanBeWrappedRecursivelyAsLongAsTheyAreInterfaces()
        {
            // Arrange
            const int secretOfTheUniverse = 42;
            var parent = new DoesNotImplementParent(secretOfTheUniverse)
                .As<IParent>();

            // Act
            var result = parent
                .GetChild()
                .SecretOfTheUniverse;

            // Assert
            Assert.Equal(secretOfTheUniverse, result);
        }
    }
}
