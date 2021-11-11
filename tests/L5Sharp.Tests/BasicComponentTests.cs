using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Tests
{
    [TestFixture]
    public class BasicComponentTests
    {
        [Test]
        public void Create_DataTypeMember_ShouldNotBeNull()
        {
            var member = Member.Create("Test", new Dint());

            member.Should().NotBeNull();
        }
    }
}