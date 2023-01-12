using FluentAssertions;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Utilities;
using NUnit.Framework;

namespace L5Sharp.Internal.Tests.L5X
{
    [TestFixture]
    public class L5XParserTests
    {
        [Test]
        public void TryParse_Atomic_ShouldNotBeNull()
        {
            var atomic = "0".TryParse<AtomicType>();

            atomic.Should().NotBeNull();
            atomic.Should().BeOfType<SINT>();
        }
    }
}