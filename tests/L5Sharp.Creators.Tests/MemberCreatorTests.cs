using System;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Creators;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using NUnit.Framework;

namespace L5Sharp.Creators.Tests
{
    [TestFixture]
    public class MemberCreatorTests
    {
        [Test]
        public void Create_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Member.Create(null!, new BOOL())).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void Create_NullType_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Member.Create("Test", null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Create_NameAndType_ShouldNotBeNull()
        {
            var member = Member.Create("Test", new BOOL());

            member.Should().NotBeNull();
        }
        
        [Test]
        public void Create_Name_ShouldNotBeNull()
        {
            var member = Member.Create<BOOL>("Test");

            member.Should().NotBeNull();
        }
        
        [Test]
        public void Create_NameAndGenericType_ShouldNotBeNull()
        {
            IDataType type = new BOOL();
            
            var member = Member.Create("Test", type);

            member.Should().NotBeNull();
        }
        
        [Test]
        public void Create_NullNameWithDimensions_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Member.Create(null!, new BOOL(), new Dimensions(1)))
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
            var member = Member.Create("Test", new BOOL(), new Dimensions(5));

            member.Should().NotBeNull();
        }
        
        [Test]
        public void Create_NameAndDimensions_ShouldNotBeNull()
        {
            var member = Member.Create<BOOL>("Test", new Dimensions(5));

            member.Should().NotBeNull();
        }
        
        [Test]
        public void Create_NameAndGenericTypeAndDimensions_ShouldNotBeNull()
        {
            IDataType type = new BOOL();
            
            var member = Member.Create("Test", type, new Dimensions(5));

            member.Should().NotBeNull();
        }
    }
}