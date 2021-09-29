using FluentAssertions;
using L5Sharp.Builders;
using L5Sharp.Enumerations;
using L5Sharp.Primitives;
using NUnit.Framework;

namespace L5Sharp.Builder.Tests
{
    [TestFixture]
    public class DataTypeBuilderTests
    {
        [Test]
        public void FluentInterface_BuildsCorrectly()
        {
            var dataType = new DataTypeBuilder("SomeType")
                .HasDescription("This is a test")
                .WithMember("TestMember", DataType.Dint, b => b
                    .HasDescription("This is a member test")
                    .HasRadix(Radix.Ascii))
                .WithMember("AnotherMember", DataType.Timer, b => b.HasDimension(new Dimensions(4)))
                .Build();

            dataType.Should().NotBeNull();
        }
    }
}