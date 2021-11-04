using FluentAssertions;
using L5Sharp.Extensions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class TagExtensionTests
    {
        [Test]
        public void GetMember_WhenCalled_ShouldNotBeNull()
        {
            var tag = new Tag("Test", Predefined.String);

            var result = tag.GetMember<String>(m => m.Data);

            result.Should().NotBeNull();
        }
    }
}