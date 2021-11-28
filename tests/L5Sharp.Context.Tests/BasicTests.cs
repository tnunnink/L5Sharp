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
            var context = LogixContext.Load(Known.L5X);

            context.Should().NotBeNull();
        }

        [Test]
        public void Content_ValidFile_ShouldHaveExpectedProperties()
        {
            var context = LogixContext.Load(Known.L5X);

            var info = context.Info;

            info.SchemaRevision.Should().Be(new Revision());
            info.SoftwareRevision.Should().Be(new Revision(32, 2));
            info.TargetName.Should().Be(new ComponentName("TestController"));
            info.TargetType.Should().Be("Controller");
            info.Owner.Should().Be("tnunnink, EN Engineering");
            info.ExportDate.Year.Should().BeGreaterOrEqualTo(2021);
        }
    }
}