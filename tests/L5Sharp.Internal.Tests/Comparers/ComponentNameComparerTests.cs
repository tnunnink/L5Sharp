using FluentAssertions;
using L5Sharp.Comparers;
using L5Sharp.Creators;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Internal.Tests.Comparers
{
    [TestFixture]
    public class ComponentNameComparerTests
    {
        [Test]
        public void Equals_EqualNameDifferentTypes()
        {
            var t1 = new BOOL();
            var t2 = Member.Create<BOOL>("BOOL");

            var comparer = ComponentNameComparer.Instance;

            comparer.Equals(t1, t2).Should().BeTrue();
        }
    }
}