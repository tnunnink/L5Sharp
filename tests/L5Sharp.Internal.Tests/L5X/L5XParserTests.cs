using FluentAssertions;
using L5Sharp.L5X;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Internal.Tests.L5X
{
    [TestFixture]
    public class L5XParserTests
    {
        [Test]
        public void TryParse_Atomic_ShouldNotBeNull()
        {
            var atomic = "0".TryParse<IAtomicType>();

            atomic.Should().NotBeNull();
            atomic.Should().BeOfType<SINT>();
        }
    }
}