using System;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Factories;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Factory.Tests
{
    [TestFixture]
    public class MemberComponentTests
    {
        [Test]
        public void Create_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Member.Create(null!, new Bool())).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void Create_NullType_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Member.Create("Test", null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Create_NameAndType_ShouldNotBeNull()
        {
            var member = Member.Create("Test", new Bool());

            member.Should().NotBeNull();
        }
        
        [Test]
        public void Create_Name_ShouldNotBeNull()
        {
            var member = Member.Create<Bool>("Test");

            member.Should().NotBeNull();
        }
        
        [Test]
        public void Create_NameAndGenericType_ShouldNotBeNull()
        {
            IDataType type = new Bool();
            
            var member = Member.Create("Test", type);

            member.Should().NotBeNull();
        }
        
        [Test]
        public void Create_NullNameWithDimensions_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Member.Create(null!, new Bool(), new Dimensions(1)))
                .Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void Create_NullTypeWithDimensions_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Member.Create("Test", null!, new Dimensions(1)))
                .Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void Create_NameAndTypeAndDimensions_ShouldNotBeNull()
        {
            var member = Member.Create("Test", new Bool(), new Dimensions(5));

            member.Should().NotBeNull();
        }
        
        [Test]
        public void Create_NameAndDimensions_ShouldNotBeNull()
        {
            var member = Member.Create<Bool>("Test", new Dimensions(5));

            member.Should().NotBeNull();
        }
        
        [Test]
        public void Create_NameAndGenericTypeAndDimensions_ShouldNotBeNull()
        {
            IDataType type = new Bool();
            
            var member = Member.Create("Test", type, new Dimensions(5));

            member.Should().NotBeNull();
        }
    }
}