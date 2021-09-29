using System.Linq;
using FluentAssertions;
using L5Sharp.Enumerations;
using L5Sharp.Primitives;
using NUnit.Framework;

namespace L5Sharp.Primitives.Tests
{
    [TestFixture]
    public class TagTests
    {
        [Test]
        public void New_ValidTagName_ShouldNotBeNull()
        {
            var tag = new Tag("Test", DataType.Bool);

            tag.Should().NotBeNull();
        }
        
        [Test]
        public void New_Counter_ShouldNotBeNull()
        {
            var tag = new Tag("Test", DataType.Counter);

            tag.Should().NotBeNull();
        }
        
        [Test]
        public void New_DintArray_ShouldNotBeNull()
        {
            var tag = new Tag("Test", DataType.Dint, new Dimensions(3, 4));

            tag.Should().NotBeNull();
        }

        [Test]
        public void SetRadix_ValidRadix_ShouldSetMembersRadix()
        {
            var tag = new Tag("Test", DataType.Dint, new Dimensions(3, 4));
            
            tag.Radix = Radix.Ascii;

            tag.Members.All(t => t.Radix == Radix.Ascii).Should().BeTrue();
        }
    }
}