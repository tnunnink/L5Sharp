using System;
using FluentAssertions;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Types.Tests
{
    [TestFixture]
    public class UndefinedTests
    {
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var type = new UNDEFINED();
            
            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_Default_ShouldHaveExpectedDefaults()
        {
            var type = new UNDEFINED();

            type.Name.Should().Be(nameof(UNDEFINED));
            type.Description.Should().BeEmpty();
            type.Family.Should().Be(DataTypeFamily.None);
            type.Class.Should().Be(DataTypeClass.Unknown);
        }
        
        [Test]
        public void New_NameOverload_NameShouldBeProvided()
        {
            var type = new UNDEFINED("Test");

            type.Name.Should().Be("Test");
        }
        
        [Test]
        public void New_NameOverloadNull_NameShouldBeProvided()
        {
            FluentActions.Invoking(() => new UNDEFINED(null!)).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void New_NameOverloadEmpty_NameShouldBeProvided()
        {
            FluentActions.Invoking(() => new UNDEFINED(string.Empty)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Instantiate_WhenCalled_ShouldBeEqualToDefault()
        {
            var type = new UNDEFINED();

            var instance = type.Instantiate();

            instance.Should().NotBeNull();
            instance.Should().BeEquivalentTo(new UNDEFINED());
        }
    }
}