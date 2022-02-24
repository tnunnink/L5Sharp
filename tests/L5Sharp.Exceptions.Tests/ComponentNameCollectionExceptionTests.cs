using FluentAssertions;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using NUnit.Framework;

namespace L5Sharp.Exceptions.Tests
{
    [TestFixture]
    public class ComponentNameCollectionExceptionTests
    {
        [Test]
        public void New_NameAndType_ShouldBeExpected()
        {
            var exception = new ComponentNameCollisionException("Name", typeof(Bool));

            exception.Should().NotBeNull();
        }
    }
}