using System;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class DimensionsTests
    {
        [Test]
        public void Empty_WhenCalled_ShouldNotBeNull()
        {
            var dimensions = Dimensions.Empty;

            dimensions.Should().NotBeNull();
        }
        
        [Test]
        public void Empty_WhenCalled_ShouldHaveAllZeros()
        {
            var dimensions = Dimensions.Empty;

            dimensions.ToString().Should().Be("0");
            dimensions.X.Should().Be(0);
            dimensions.Y.Should().Be(0);
            dimensions.Z.Should().Be(0);
        }
        
        [Test]
        public void New_OneDimension_ShouldHaveExpectedValues()
        {
            var fixture = new Fixture();
            var x = fixture.Create<ushort>();
            
            var dimensions = new Dimensions(x);

            dimensions.ToString().Should().Be(x.ToString());
            dimensions.X.Should().Be(x);
            dimensions.Y.Should().Be(0);
            dimensions.Z.Should().Be(0);
        }
        
        [Test]
        public void New_OneDimensionZero_ShouldHaveExpectedValues()
        {
            var dimensions = new Dimensions(0);

            dimensions.ToString().Should().Be(0.ToString());
            dimensions.X.Should().Be(0);
            dimensions.Y.Should().Be(0);
            dimensions.Z.Should().Be(0);
        }
        
        [Test]
        public void New_TwoDimensions_ShouldHaveExpectedValues()
        {
            var fixture = new Fixture();
            var x = fixture.Create<ushort>();
            var y = fixture.Create<ushort>();
            
            var dimensions = new Dimensions(x, y);

            dimensions.ToString().Should().Be($"{x} {y}");
            dimensions.X.Should().Be(x);
            dimensions.Y.Should().Be(y);
            dimensions.Z.Should().Be(0);
        }
        
        [Test]
        public void New_TwoDimensionsXIsZero_ShouldHaveExpectedValues()
        {
            var fixture = new Fixture();
            var y = fixture.Create<ushort>();

            FluentActions.Invoking(() => new Dimensions(0, y)).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void New_TwoDimensionsYIsZero_ShouldHaveExpectedValues()
        {
            var fixture = new Fixture();
            var x = fixture.Create<ushort>();

            FluentActions.Invoking(() => new Dimensions(x, 0)).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void New_ThreeDimensions_ShouldHaveExpectedValues()
        {
            var fixture = new Fixture();
            var x = fixture.Create<ushort>();
            var y = fixture.Create<ushort>();
            var z = fixture.Create<ushort>();
            
            var dimensions = new Dimensions(x, y, z);

            dimensions.ToString().Should().Be($"{x} {y} {z}");
            dimensions.X.Should().Be(x);
            dimensions.Y.Should().Be(y);
            dimensions.Z.Should().Be(z);
        }
        
        [Test]
        public void New_ThreeDimensionsZIsZero_ShouldHaveExpectedValues()
        {
            var fixture = new Fixture();
            var x = fixture.Create<ushort>();
            var y = fixture.Create<ushort>();

            FluentActions.Invoking(() => new Dimensions(x, y, 0)).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void New_ThreeDimensionsYIsZero_ShouldHaveExpectedValues()
        {
            var fixture = new Fixture();
            var x = fixture.Create<ushort>();
            var z = fixture.Create<ushort>();

            FluentActions.Invoking(() => new Dimensions(x, 0, z)).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void New_ThreeDimensionsXIsZero_ShouldHaveExpectedValues()
        {
            var fixture = new Fixture();
            var y = fixture.Create<ushort>();
            var z = fixture.Create<ushort>();

            FluentActions.Invoking(() => new Dimensions(0, y, z)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void GenerateIndices_OneDimension_ShouldNotBeNullAndHaveExpectedLength()
        {
            var fixture = new Fixture();
            var x = fixture.Create<ushort>();
            var dimensions = new Dimensions(x);

            var indices = dimensions.GenerateIndices().ToList();

            indices.Should().NotBeEmpty();
            indices.Should().HaveCount(x);
        }
        
        [Test]
        public void GenerateIndices_TwoDimension_ShouldNotBeNullAndHaveExpectedLength()
        {
            var fixture = new Fixture();
            var x = fixture.Create<ushort>();
            var y = fixture.Create<ushort>();
            var dimensions = new Dimensions(x, y);

            var indices = dimensions.GenerateIndices().ToList();

            indices.Should().NotBeEmpty();
            indices.Should().HaveCount(x * y);
        }
        
        [Test]
        public void GenerateIndices_ThreeDimension_ShouldNotBeNullAndHaveExpectedLength()
        {
            //Hard coding these because I don't want a crazy large array for unit testing
            var dimensions = new Dimensions(5, 6, 7);

            var indices = dimensions.GenerateIndices().ToList();

            indices.Should().NotBeEmpty();
            indices.Should().HaveCount(210);
        }
        
        [Test]
        public void Parse_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Dimensions.Parse(null)).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void Parse_Empty_ShouldBeEmpty()
        {
            var dimension = Dimensions.Parse(string.Empty);

            dimension.Should().Be(Dimensions.Empty);
        }
        
        [Test]
        public void Parse_SingleSpace_ShouldBeEmpty()
        {
            FluentActions.Invoking(() => Dimensions.Parse(" ")).Should()
                .Throw<ArgumentException>();
        }
        
        [Test]
        public void Parse_InvalidPattern_ShouldThrowInvalidOperationException()
        {
            var fixture = new Fixture();
            FluentActions.Invoking(() => Dimensions.Parse(fixture.Create<string>())).Should()
                .Throw<ArgumentException>();
        }
        
        [Test]
        public void Parse_MoreThanThreeDimensions_ShouldThrowInvalidOperationException()
        {
            FluentActions.Invoking(() => Dimensions.Parse("1 4 3 8")).Should()
                .Throw<ArgumentException>();
        }

        [Test]
        public void Parse_OneDimension_ShouldHaveExpectedValues()
        {
            var dimensions = Dimensions.Parse("3");

            dimensions.Should().NotBeNull();
            dimensions.X.Should().Be(3);
            dimensions.Y.Should().Be(0);
            dimensions.Z.Should().Be(0);
            dimensions.Length.Should().Be(3);
        }
        
        [Test]
        public void Parse_TwoDimension_ShouldHaveExpectedValues()
        {
            var dimensions = Dimensions.Parse("3 10");

            dimensions.Should().NotBeNull();
            dimensions.X.Should().Be(3);
            dimensions.Y.Should().Be(10);
            dimensions.Z.Should().Be(0);
            dimensions.Length.Should().Be(30);
        }
        
        [Test]
        public void Parse_ThreeDimension_ShouldHaveExpectedValues()
        {
            var dimensions = Dimensions.Parse("3 10 6");

            dimensions.Should().NotBeNull();
            dimensions.X.Should().Be(3);
            dimensions.Y.Should().Be(10);
            dimensions.Z.Should().Be(6);
            dimensions.Length.Should().Be(180);
        }

        [Test]
        public void Equals_TypeOverloadEquals_ShouldBeTrue()
        {
            var d1 = new Dimensions(5);
            var d2 = new Dimensions(5);

            var result = d1.Equals(d2);

            result.Should().BeTrue();
        }
        
        [Test]
        public void Equals_TypeOverloadSame_ShouldBeTrue()
        {
            var d1 = new Dimensions(5);

            var result = d1.Equals(d1);

            result.Should().BeTrue();
        }
        
        [Test]
        public void Equals_TypeOverloadNull_ShouldBeFalse()
        {
            var d1 = new Dimensions(5);

            var result = d1.Equals(null);

            result.Should().BeFalse();
        }
        
        [Test]
        public void Equals_ObjectOverloadEquals_ShouldBeTrue()
        {
            var d1 = new Dimensions(5);
            var d2 = (object) new Dimensions(5);

            var result = d1.Equals(d2);

            result.Should().BeTrue();
        }
        
        [Test]
        public void Equals_ObjectOverloadSame_ShouldBeTrue()
        {
            var d1 = new Dimensions(5);

            var result = d1.Equals((object) d1);

            result.Should().BeTrue();
        }
        
        [Test]
        public void Equals_ObjectOverloadNull_ShouldBeFalse()
        {
            var d1 = new Dimensions(5);

            var result = d1.Equals((object) null);

            result.Should().BeFalse();
        }
        
        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var d1 = new Dimensions(5);

            var result = d1.GetHashCode();

            result.Should().NotBe(0);
        }
    }
}