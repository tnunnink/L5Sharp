using System;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Helpers;
using NUnit.Framework;

namespace L5Sharp.Internal.Tests.Helpers
{
    [TestFixture]
    public class LogixNamesTests
    {
        [Test]
        public void GetComponentName_Invalid_ShouldThrowInvalidOperationException()
        {
            FluentActions.Invoking(LogixNames.GetComponentName<string>).Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void GetComponentName_Controller_ShouldBeExpected()
        {
            var name = LogixNames.GetComponentName<Controller>();

            name.Should().Be(L5XElement.Controller.ToString());
        }
        
        [Test]
        public void GetComponentName_UserDefined_ShouldBeExpected()
        {
            var name = LogixNames.GetComponentName<UserDefined>();

            name.Should().Be(L5XElement.DataType.ToString());
        }
        
        [Test]
        public void GetComponentName_Member_ShouldBeExpected()
        {
            var name = LogixNames.GetComponentName<Member<IDataType>>();

            name.Should().Be(L5XElement.Member.ToString());
        }
        
        [Test]
        public void GetContainerName_Invalid_ShouldThrowInvalidOperationException()
        {
            FluentActions.Invoking(LogixNames.GetContainerName<string>).Should().Throw<InvalidOperationException>();
        }
        
        [Test]
        public void GetContainerName_UserDefined_ShouldBeExpected()
        {
            var name = LogixNames.GetContainerName<UserDefined>();

            name.Should().Be(L5XElement.DataTypes.ToString());
        }
        
        [Test]
        public void GetContainerName_Member_ShouldBeExpected()
        {
            var name = LogixNames.GetContainerName<Member<IDataType>>();

            name.Should().Be(L5XElement.Members.ToString());
        }
    }
}