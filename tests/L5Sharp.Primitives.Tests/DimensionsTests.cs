using System.Linq;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Primitives;
using NUnit.Framework;

namespace L5Sharp.Primitives.Tests
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

            dimensions.ToString().Should().BeEmpty();
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
            var fixture = new Fixture();
            var x = fixture.Create<ushort>();
            var y = fixture.Create<ushort>();
            var z = fixture.Create<ushort>();
            var dimensions = new Dimensions(x, y, z);

            var indices = dimensions.GenerateIndices().ToList();

            indices.Should().NotBeEmpty();
            indices.Should().HaveCount(x * y * z);
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
    }
}