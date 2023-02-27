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
        public void DataTypes_WhenCalled_ShouldNotBeEmpty()
        {
            var content = LogixContent.Load(Known.Test);

            var dataTypes = content.DataTypes().ToList();

            dataTypes.Should().NotBeEmpty();
        }

        [Test]
        public void Find_KnownDataType_ShouldNotBeNull()
        {
            var content = LogixContent.Load(Known.Test);
 
            var type = content.DataTypes().Find("BoolType");

            type.Should().NotBeNull();
        }

        [Test]
        public void Get_WhereFiltered_ShouldNotBeEmpty()
        {
            var content = LogixContent.Load(Known.Test);

            var results = content.DataTypes().Where(d => d.Members.Any(m => m.DataType == "BOOL")).ToList();

            results.Should().NotBeEmpty();
        }

        [Test]
        public void InScope_ShouldReturnExpected()
        {
            var content = LogixContent.Load(Known.Test);

            var tag = content.Tags("MyProgramName").Find("MyNestedType");

            tag.Should().NotBeNull();
        }

        [Test]
        public void InScope_CustomRead_ShouldNotBeEmpty()
        {
            var content = LogixContent.Load(Known.Test);

            var timers = content.Tags().Where(t => t.DataType == "TIMER");

            timers.Should().NotBeEmpty();
        }

        [Test]
        public void InScope_Program_ShouldNotBeEmpty()
        {
            var content = LogixContent.Load(Known.Test);

            var timers = content.Tags("TestProject").Where(t => t.DataType == "TIMER");

            timers.Should().NotBeEmpty();
        }

        [Test]
        public void Routine_Testing_ShouldNotBeEmpty()
        {
            var content = LogixContent.Load(Known.Test);
            
            var routines = content.Routines<RllRoutine>("TestProgram");

            routines.Should().NotBeEmpty();
        }

        [Test]
        public void Query_Testing_ShouldLookNice()
        {
            var content = LogixContent.Load(Known.Test);

            var results = content.Query<Routine>().Where(r => r.Name == "Test");

            results.Should().BeEmpty();
        }
    }
}