using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Enums;
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

            var tags = content.GetAll<Tag>();

            tags.Should().NotBeEmpty();
        }

        [Test]
        public void GetControllerTagByName()
        {
            var content = LogixContent.Load(Known.Test);
            
            var tag = content.IsScope(Scope.Controller).Get<Tag>("MyTag");

            tag.Should().NotBeNull();
        }

        [Test]
        public void GetProgramTagNyName()
        {
            var content = LogixContent.Load(Known.Test);

            var tag = content.InProgram("MyProgram").Get<Tag>("TestTag");

            tag.Should().NotBeNull();
        }
        
        [Test]
        public void GetAllProgramTags()
        {
            var content = LogixContent.Load(Known.Test);

            var tags = content.InProgram("MyProgram").GetAll<Tag>();

            tags.Should().NotBeEmpty();
        }
    }
}