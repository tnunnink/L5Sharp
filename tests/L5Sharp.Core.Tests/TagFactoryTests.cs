using FluentAssertions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class TagFactoryTests
    {
        [Test]
        public void Create_DefaultOverload_ShouldBeOfTypeIDataType()
        {
            var dataType = new DataType("MyType");
            var tag = Tag.Create("Test", dataType);

            tag.Should().NotBeNull();
            tag.Should().BeOfType<Tag<IDataType>>();
        }
        
        [Test]
        public void Create_GenericOverload_ShouldBeOfTypeBool()
        {
            var tag = Tag.Create<Bool>("Test");

            tag.Should().NotBeNull();
            tag.Should().BeOfType<Tag<Bool>>();
        }
        
        [Test]
        public void Create_GenericOverloadWithTypeArgument_ShouldBeOfTypeBool()
        {
            var tag = Tag.Create("Test", new Bool(true));

            tag.Should().NotBeNull();
            tag.Should().BeOfType<Tag<Bool>>();
        }

        [Test]
        public void Build_SimpleNameOverload_ShouldNotBeNull()
        {
            var tag = Tag.Build<Timer>("Test").Create();

            tag.Should().NotBeNull();
            tag.Should().BeOfType<Tag<Timer>>();
        }
    }
}