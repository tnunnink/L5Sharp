using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Types.Tests
{
    [TestFixture]
    public class BitTests
    {

        [Test]
        public void Predefined_Bool_ShouldNotBeNull()
        {
            var type = Logix.DataType.Bit;

            type.Should().NotBeNull();
            type.Name.Should().Be("BOOL");
            type.Class.Should().Be(DataTypeClass.Atomic);
            type.Family.Should().Be(DataTypeFamily.None);
        } 
        
        [Test]
        public void ParseType_Bit_ShouldNotBeNull()
        {
            var type = Logix.DataType.Parse("BIT");

            type.Should().NotBeNull();
            type.Name.Should().Be("BOOL");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        } 
        
        [Test]
        public void ContainsType_Bit_ShouldBeTrue()
        {
            var result = Logix.DataType.Contains("BIT");

            result.Should().BeTrue();
        } 
    }
}