using System.Linq;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Tests.Types.Custom;
using L5Sharp.Types.Atomics;
using NUnit.Framework;

namespace L5Sharp.Tests
{
    [TestFixture]
    public class LogixContentTests
    {
        [Test]
        public void New_ValidFile_ShouldNotBeNull()
        {
            var content = LogixContent.Load(Known.Test);

            content.Should().NotBeNull();
        }

        [Test]
        public void All_DataType_ShouldNotBeEmpty()
        {
            var content = LogixContent.Load(Known.Test);

            var dataTypes = content.GetAll<DataType>().ToList();

            dataTypes.Should().NotBeEmpty();
        }

        [Test]
        public void Find_KnownDataType_ShouldNotBeNull()
        {
            var content = LogixContent.Load(Known.Test);

            var type = content.Find<DataType>("BoolType");

            type.Should().NotBeNull();
        }

        [Test]
        public void Get_WhereFiltered_ShouldNotBeEmpty()
        {
            var content = LogixContent.Load(Known.Test);

            var results = content.GetAll<DataType>().Where(d => d.Members.Any(m => m.DataType == "BOOL")).ToList();

            results.Should().NotBeEmpty();
        }

        [Test]
        public void InScope_ShouldReturnExpected()
        {
            var content = LogixContent.Load(Known.Test);

            var tag = content.IsScope(Scope.Program, "MyProgramName").Find<Tag>("MyNestedType");

            var nested = tag.As<Tag<MyNestedType>>();

            nested.SetValue(t => t.Simple.M4, new DINT(5000));
        }

        [Test]
        public void InScope_CustomRead_ShouldNotBeEmpty()
        {
            var content = LogixContent.Load(Known.Test);

            var controllerTag = content.IsScope(Scope.Controller).GetAll<Tag>().Where(t => t.DataType == "TIMER");

            foreach (var tag in controllerTag)
            {
                content.IsScope(Scope.Program).Add(tag);
            }
        }

        [Test]
        public void InScope_Program_ShouldNotBeEmpty()
        {
            var content = LogixContent.Load(Known.Test);

            var controllerTag = content
                .IsScope(Scope.Program, "TestProgram")
                .GetAll<Tag>()
                .Where(t => t.DataType == "TIMER");

            foreach (var tag in controllerTag)
            {
                content.IsScope(Scope.Program).Add(tag);
            }
        }
    }
}