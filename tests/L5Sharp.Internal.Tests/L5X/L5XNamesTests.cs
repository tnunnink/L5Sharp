using System;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.L5X;
using NUnit.Framework;

namespace L5Sharp.Internal.Tests.L5X
{
    [TestFixture]
    public class L5XNamesTests
    {
        [Test]
        public void GetComponentName_NonMappedComponent_ShouldThrowInvalidOperationException()
        {
            FluentActions.Invoking(L5XNames.GetComponentName<Controller>).Should()
                .Throw<InvalidOperationException>();
        }

        [Test]
        public void GetComponentName_Controller_ShouldBeExpected()
        {
            var name = L5XNames.GetComponentName<IController>();

            name.Should().Be(L5XElement.Controller.ToString());
        }

        [Test]
        public void GetComponentName_UserDefined_ShouldBeExpected()
        {
            var name = L5XNames.GetComponentName<IUserDefined>();

            name.Should().Be(L5XElement.DataType.ToString());
        }

        [Test]
        public void GetComponentName_Member_ShouldBeExpected()
        {
            var name = L5XNames.GetComponentName<IMember<IDataType>>();

            name.Should().Be(L5XElement.Member.ToString());
        }

        [Test]
        public void GetComponentName_Module_ShouldBeExpected()
        {
            var name = L5XNames.GetComponentName<IModule>();

            name.Should().Be(L5XElement.Module.ToString());
        }

        [Test]
        public void GetComponentName_Tag_ShouldBeExpected()
        {
            var name = L5XNames.GetComponentName<ITag<IDataType>>();

            name.Should().Be(L5XElement.Tag.ToString());
        }

        [Test]
        public void GetComponentName_Program_ShouldBeExpected()
        {
            var name = L5XNames.GetComponentName<IProgram>();

            name.Should().Be(L5XElement.Program.ToString());
        }

        [Test]
        public void GetComponentName_Routine_ShouldBeExpected()
        {
            var name = L5XNames.GetComponentName<IRoutine<ILogixContent>>();

            name.Should().Be(L5XElement.Routine.ToString());
        }

        [Test]
        public void GetComponentName_Task_ShouldBeExpected()
        {
            var name = L5XNames.GetComponentName<ITask>();

            name.Should().Be(L5XElement.Task.ToString());
        }

        [Test]
        public void GetContainerName_NonMappedComponent_ShouldThrowInvalidOperationException()
        {
            FluentActions.Invoking(L5XNames.GetContainerName<IController>).Should()
                .Throw<InvalidOperationException>();
        }

        [Test]
        public void GetContainerName_UserDefined_ShouldBeExpected()
        {
            var name = L5XNames.GetContainerName<IUserDefined>();

            name.Should().Be(L5XElement.DataTypes.ToString());
        }

        [Test]
        public void GetContainerName_Member_ShouldBeExpected()
        {
            var name = L5XNames.GetContainerName<IMember<IDataType>>();

            name.Should().Be(L5XElement.Members.ToString());
        }

        [Test]
        public void GetContainerName_Module_ShouldBeExpected()
        {
            var name = L5XNames.GetContainerName<IModule>();

            name.Should().Be(L5XElement.Modules.ToString());
        }

        [Test]
        public void GetContainerName_Tag_ShouldBeExpected()
        {
            var name = L5XNames.GetContainerName<ITag<IDataType>>();

            name.Should().Be(L5XElement.Tags.ToString());
        }

        [Test]
        public void GetContainerName_Program_ShouldBeExpected()
        {
            var name = L5XNames.GetContainerName<IProgram>();

            name.Should().Be(L5XElement.Programs.ToString());
        }

        [Test]
        public void GetContainerName_Routine_ShouldBeExpected()
        {
            var name = L5XNames.GetContainerName<IRoutine<ILogixContent>>();

            name.Should().Be(L5XElement.Routines.ToString());
        }

        [Test]
        public void GetContainerName_Task_ShouldBeExpected()
        {
            var name = L5XNames.GetContainerName<ITask>();

            name.Should().Be(L5XElement.Tasks.ToString());
        }
    }
}