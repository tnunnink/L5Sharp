using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Context.Tests
{
    [TestFixture]
    public class LogixTypesTests
    {
        [Test]
        public void Types_WhenCalled_ShouldNotBeEmpty()
        {
            var context = new L5XContext(Known.Template);

            context.Should().NotBeNull();
        }
    }
}