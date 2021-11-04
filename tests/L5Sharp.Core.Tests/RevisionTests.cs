using System;
using AutoFixture;
using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class RevisionTests
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _fixture.Customizations.Add(new RandomNumericSequenceGenerator(1, 127));
        }
        
        [Test]
        public void New_MajorZero_ShouldThrowArgumentOutOfRangeException()
        {
            FluentActions.Invoking(() => new Revision(0)).Should().Throw<ArgumentOutOfRangeException>();
        }
        
        [Test]
        public void New_Major128_ShouldThrowArgumentOutOfRangeException()
        {
            FluentActions.Invoking(() => new Revision(128)).Should().Throw<ArgumentOutOfRangeException>();
        }
        
        [Test]
        public void New_MinorZero_ShouldThrowArgumentOutOfRangeException()
        {
            FluentActions.Invoking(() => new Revision(1, 0)).Should().Throw<ArgumentOutOfRangeException>();
        }
        
        [Test]
        public void New_Major127_ShouldNotThrowException()
        {
            FluentActions.Invoking(() => new Revision(127)).Should().NotThrow();
        }
        
        [Test]
        public void New_Minor255_ShouldNotThrowException()
        {
            FluentActions.Invoking(() => new Revision(1, 255)).Should().NotThrow();
        }
        
        [Test]
        public void New_Valid_ShouldNotBeNull()
        {
            var revision = new Revision();

            revision.Should().NotBeNull();
        }
        
        [Test]
        public void New_Valid_ShouldHaveExpectedDefaults()
        {
            var revision = new Revision();

            revision.Major.Should().Be(1);
            revision.Minor.Should().Be(1);
        }
        
        [Test]
        public void ChangeMajor_InvalidMajor_ResultShouldThrowArgumentOutOfRangeException()
        {
            var revision = new Revision();
            
            FluentActions.Invoking(() => revision.ChangeMajor(0)).Should().Throw<ArgumentOutOfRangeException>();
        }
        
        [Test]
        public void ChangeMajor_ValidMajor_ResultShouldHaveExpectedMajor()
        {
            var major = _fixture.Create<byte>();
            var minor = _fixture.Create<byte>();
            var revision = new Revision(major, minor);
            var expected = _fixture.Create<byte>();
            
            var result = revision.ChangeMajor(expected);

            result.Major.Should().Be(expected);
        }
        
        [Test]
        public void ChangeMajor_ValidMajor_ResultShouldHaveSameMinor()
        {
            var major = _fixture.Create<byte>();
            var minor = _fixture.Create<byte>();
            var revision = new Revision(major, minor);
            var expected = _fixture.Create<byte>();
            
            var result = revision.ChangeMajor(expected);

            result.Minor.Should().Be(minor);
        }
        
        [Test]
        public void ChangeMinor_InvalidMinor_ResultShouldThrowArgumentOutOfRangeException()
        {
            var revision = new Revision();
            
            FluentActions.Invoking(() => revision.ChangeMinor(0)).Should().Throw<ArgumentOutOfRangeException>();
        }
        
        [Test]
        public void ChangeMinor_ValidMinor_ResultShouldHaveExpectedMinor()
        {
            var major = _fixture.Create<byte>();
            var minor = _fixture.Create<byte>();
            var revision = new Revision(major, minor);
            var expected = _fixture.Create<byte>();
            
            var result = revision.ChangeMinor(expected);

            result.Minor.Should().Be(expected);
        }
        
        [Test]
        public void ChangeMinor_ValidMinor_ResultShouldHaveSameMajor()
        {
            var major = _fixture.Create<byte>();
            var minor = _fixture.Create<byte>();
            var revision = new Revision(major, minor);
            var expected = _fixture.Create<byte>();
            
            var result = revision.ChangeMinor(expected);

            result.Major.Should().Be(major);
        }

        [Test]
        public void EqualsTyped_Equal_ShouldBeTrue()
        {
            var first = new Revision();
            var second = new Revision();

            var result = first.Equals(second);
            
            result.Should().BeTrue();
        }
        
        [Test]
        public void EqualsTyped_Same_ShouldBeTrue()
        {
            var first = new Revision();

            var result = first.Equals(first);
            
            result.Should().BeTrue();
        }
        
        [Test]
        public void EqualsTyped_Null_ShouldBeFalse()
        {
            var first = new Revision();

            var result = first.Equals(null);
            
            result.Should().BeFalse();
        }
        
        [Test]
        public void EqualsObject_Equal_ShouldBeTrue()
        {
            var first = new Revision();
            var second = new Revision();

            var result = first.Equals((object)second);
            
            result.Should().BeTrue();
        }
        
        [Test]
        public void EqualsObject_Same_ShouldBeTrue()
        {
            var first = new Revision();

            var result = first.Equals((object)first);
            
            result.Should().BeTrue();
        }
        
        [Test]
        public void EqualsObject_Null_ShouldBeFalse()
        {
            var first = new Revision();

            var result = first.Equals((object)null);
            
            result.Should().BeFalse();
        }
        
        [Test]
        public void EqualsOperator_Equal_ShouldBeTrue()
        {
            var first = new Revision();
            var second = new Revision();

            var result = first == second;
            
            result.Should().BeTrue();
        }
        
        [Test]
        public void NotEqualsOperator_Equal_ShouldBeFalse()
        {
            var first = new Revision();
            var second = new Revision();

            var result = first != second;
            
            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var revision = new Revision();

            var hash = revision.GetHashCode();

            hash.Should().NotBe(0);
        }
    }
}