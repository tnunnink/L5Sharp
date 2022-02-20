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
        public void NamesShouldNotBeNull()
        {
            LogixNames.RsLogix5000Content.Should().NotBeNull();
            LogixNames.Controller.Should().NotBeNull();
            LogixNames.DataTypes.Should().NotBeNull();
            LogixNames.DataType.Should().NotBeNull();
            LogixNames.Members.Should().NotBeNull();
            LogixNames.Member.Should().NotBeNull();
            LogixNames.Modules.Should().NotBeNull();
            LogixNames.Module.Should().NotBeNull();
            LogixNames.AddOnInstructionDefinitions.Should().NotBeNull();
            LogixNames.AddOnInstructionDefinition.Should().NotBeNull();
            LogixNames.Parameters.Should().NotBeNull();
            LogixNames.Parameter.Should().NotBeNull();
            LogixNames.Tags.Should().NotBeNull();
            LogixNames.Tag.Should().NotBeNull();
            LogixNames.Programs.Should().NotBeNull();
            LogixNames.Program.Should().NotBeNull();
            LogixNames.Routines.Should().NotBeNull();
            LogixNames.Routine.Should().NotBeNull();
            LogixNames.RllContent.Should().NotBeNull();
            LogixNames.StContent.Should().NotBeNull();
            LogixNames.Rungs.Should().NotBeNull();
            LogixNames.Rung.Should().NotBeNull();
            LogixNames.Tasks.Should().NotBeNull();
            LogixNames.Data.Should().NotBeNull();
            LogixNames.Value.Should().NotBeNull();
            LogixNames.DataValue.Should().NotBeNull();
            LogixNames.Array.Should().NotBeNull();
            LogixNames.Index.Should().NotBeNull();
            LogixNames.Element.Should().NotBeNull();
            LogixNames.Structure.Should().NotBeNull();
            LogixNames.ArrayMember.Should().NotBeNull();
            LogixNames.DataValueMember.Should().NotBeNull();
            LogixNames.StructureMember.Should().NotBeNull();
        }

        [Test]
        public void GetComponentName_Invalid_ShouldThrowInvalidOperationException()
        {
            FluentActions.Invoking(LogixNames.GetComponentName<string>).Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void GetComponentName_Controller_ShouldBeExpected()
        {
            var name = LogixNames.GetComponentName<Controller>();

            name.Should().Be(LogixNames.Controller);
        }
        
        [Test]
        public void GetComponentName_UserDefined_ShouldBeExpected()
        {
            var name = LogixNames.GetComponentName<UserDefined>();

            name.Should().Be(LogixNames.DataType);
        }
        
        [Test]
        public void GetComponentName_Member_ShouldBeExpected()
        {
            var name = LogixNames.GetComponentName<Member<IDataType>>();

            name.Should().Be(LogixNames.Member);
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

            name.Should().Be(LogixNames.DataTypes);
        }
        
        [Test]
        public void GetContainerName_Member_ShouldBeExpected()
        {
            var name = LogixNames.GetContainerName<Member<IDataType>>();

            name.Should().Be(LogixNames.Members);
        }
    }
}