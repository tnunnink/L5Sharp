using System;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types.Predefined;
using NUnit.Framework;

namespace L5Sharp.Types.Tests
{
    [TestFixture]
    public class UndefinedTests
    {
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var type = new Undefined();
            
            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_Default_ShouldHaveExpectedDefaults()
        {
            var type = new Undefined();

            type.Name.Should().Be(nameof(Undefined));
            type.Description.Should().Be("Undefined DataType");
            type.Family.Should().Be(DataTypeFamily.None);
            type.Class.Should().Be(DataTypeClass.Unknown);
        }
        
        [Test]
        public void New_NameOverload_NameShouldBeProvided()
        {
            var type = new Undefined("Test");

            type.Name.Should().Be("Test");
        }
        
        [Test]
        public void New_NameOverloadNull_NameShouldBeProvided()
        {
            FluentActions.Invoking(() => new Undefined(null!)).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void New_NameOverloadEmpty_NameShouldBeProvided()
        {
            FluentActions.Invoking(() => new Undefined(string.Empty)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Instantiate_WhenCalled_ShouldBeEqualToDefault()
        {
            var type = new Undefined();

            var instance = type.Instantiate();

            instance.Should().NotBeNull();
            instance.Should().BeEquivalentTo(new Undefined());
        }
    }
}