using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Tests
{
    [TestFixture]
    public class Examples
    {
        [Test]
        public void GetAllTagsInFile()
        {
            var content = LogixContent.Load(Known.Test);

            var tags = content.Tags();

            tags.Should().NotBeEmpty();
        }
    }
}