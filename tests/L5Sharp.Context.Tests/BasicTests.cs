using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Context.Tests
{
    [TestFixture]
    public class BasicTests
    {
        
        [Test]
        public void KnownTestFileShouldExists()
        {
            FileAssert.Exists(Known.L5X);
        }

        [Test]
        public void Load_ValidFile_ShouldNotBeNull()
        {
            var context = new LogixContext(Known.L5X);

            context.Should().NotBeNull();
        }

        [Test]
        public void Content_ValidFile_ShouldHaveExpectedProperties()
        {
            var context = new LogixContext(Known.L5X);

            context.SchemaRevision.Should().Be(new Revision());
            context.SoftwareRevision.Should().Be(new Revision(32, 2));
            context.TargetName.Should().Be(new ComponentName("TestController"));
            context.TargetType.Should().Be("Controller");
            context.Owner.Should().Be("tnunnink, EN Engineering");
            context.ExportDate.Year.Should().BeGreaterOrEqualTo(2021);
        }
    }
}