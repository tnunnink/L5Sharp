using System;
using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.L5X;
using NUnit.Framework;

namespace L5Sharp.Internal.Tests.L5X
{
    [TestFixture]
    public class L5XContainersTests
    {
        private L5XDocument _l5X;

        [SetUp]
        public void Setup()
        {
            var document = new XDocument();
            _l5X = new L5XDocument(document);
        }
        
        [Test]
        public void GetComponentName_NonMappedComponent_ShouldThrowInvalidOperationException()
        {
            var components = new L5XComponents(_l5X);
            
            FluentActions.Invoking(() => components.Get<IMember<IDataType>>()).Should()
                .Throw<InvalidOperationException>();
        }

        [Test]
        public void GetComponentName_Controller_ShouldBeExpected()
        {
            var components = new L5XComponents(_l5X);
            
            var names = components.Get<IController>();

            names.Should().BeEmpty();
        }

        /*[Test]
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
        }*/
    }
}