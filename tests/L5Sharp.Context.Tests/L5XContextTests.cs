using System.Xml.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Context.Tests
{
    [TestFixture]
    public class L5XContextTests
    {
        [Test]
        public void Load_ValidL5X_ShouldNotBeNull()
        {
            var context = LogixContent.Load(Known.L5X);

            context.Should().NotBeNull();
        }

        [Test]
        public void Parse_ValidL5XString_ShouldNotBeNull()
        {
            var document = XDocument.Load(Known.L5X);

            var context = LogixContent.Parse(document.Root?.ToString()!);

            context.Should().NotBeNull();
        }
    }
}