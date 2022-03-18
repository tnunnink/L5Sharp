using FluentAssertions;
using L5Sharp.Atomics;
using L5Sharp.Comparers;
using L5Sharp.Creators;
using NUnit.Framework;

namespace L5Sharp.Internal.Tests.L5X
{
    [TestFixture]
    public class ComponentNameComparerTests
    {
        [Test]
        public void Equals_EqualNameDifferentTypes()
        {
            var t1 = new Bool();
            var t2 = Member.Create<Bool>("BOOL");

            var comparer = ComponentNameComparer.Instance;

            comparer.Equals(t1, t2).Should().BeTrue();
        }
    }
}