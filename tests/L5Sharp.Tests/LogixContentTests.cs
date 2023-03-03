using System.Linq;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Types;
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
        public void New_ValidFile_ShouldHaveExpectedContent()
        {
            var content = LogixContent.Load(Known.Test);

            content.L5X.Should().NotBeNull();
            content.L5X.SchemaRevision.Should().Be(new Revision());
            content.L5X.SoftwareRevision.Should().Be(new Revision(32, 02));
            content.L5X.TargetName.Should().Be("TestController");
            content.L5X.TargetType.Should().Be("Controller");
            content.L5X.ContainsContext.Should().Be(false);
            content.L5X.Owner.Should().Be("tnunnink, EN Engineering");
            content.L5X.ExportDate.Should().NotBeNull();
        }

        [Test]
        public void Controller_WhenCalled_ReturnsControllerInstance()
        {
            var content = LogixContent.Load(Known.Test);

            var controller = content.Controller();

            controller?.Name.Should().Be("TestController");
            controller?.ProcessorType.Should().Be("1756-L83E");
            controller?.Description.Should().Be("This is a test project");
            controller?.Revision.Should().Be(new Revision(32, 11));
        }

        [Test]
        public void DataTypes_WhenCalled_ShouldNotBeEmpty()
        {
            var content = LogixContent.Load(Known.Test);

            var dataTypes = content.DataTypes().ToList();

            dataTypes.Should().NotBeEmpty();
        }
        
        [Test]
        public void Tags_WhenCalled_ShouldNotBeEmpty()
        {
            var content = LogixContent.Load(Known.Test);

            var tags = content.Tags().ToList();

            tags.Should().NotBeEmpty();
        }

        [Test]
        public void Tags_InMainProgram_ShouldNotBeEmpty()
        {
            var content = LogixContent.Load(Known.Test);

            var tags = content.Tags("MainProgram").ToList();

            tags.Should().NotBeEmpty();
        }
        
        
        [Test]
        public void Modules_WhenCalled_ShouldNotBeEmpty()
        {
            var content = LogixContent.Load(Known.Test);

            var modules = content.Modules().ToList();

            modules.Should().NotBeEmpty();
        }
        
        [Test]
        public void Tasks_WhenCalled_ShouldNotBeEmpty()
        {
            var content = LogixContent.Load(Known.Test);

            var tags = content.Tasks().ToList();

            tags.Should().NotBeEmpty();
        }
        
        [Test]
        public void Routines_WhenCalled_ShouldNotBeEmpty()
        {
            var content = LogixContent.Load(Known.Test);

            var tags = content.Routines("MainProgram").ToList();

            tags.Should().NotBeEmpty();
        }
        
        
        [Test]
        public void Tag_WhenCalled_ShouldNotBeEmpty()
        {
            var content = LogixContent.Load(Known.Test);

            var tags = content.Tags().Find("MultiDimensionalArray");

            tags.Should().NotBeNull();

            var array = tags.Data.As<ArrayType<ILogixType>>();

            var elements = array.Elements.ToList();
            elements.Should().NotBeEmpty();

            array.Should().NotBeNull();
        }

        [Test]
        public void Find_KnownDataType_ShouldNotBeNull()
        {
            var content = LogixContent.Load(Known.Test);
 
            var type = content.DataTypes().Find("AlarmType");

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

            var tag = content.Tags("MainProgram").Find("MyNestedType");

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

            var timers = content.Tags("MainProgram").Where(t => t.DataType == "TIMER");

            timers.Should().NotBeEmpty();
        }

        [Test]
        public void Routine_Testing_ShouldNotBeEmpty()
        {
            var content = LogixContent.Load(Known.Test);
            
            var routines = content.Routines<RllRoutine>("MainProgram");

            routines.Should().NotBeEmpty();
        }

        [Test]
        public void Query_Testing_ShouldLookNice()
        {
            var content = LogixContent.Load(Known.Test);

            var results = content.Query<Routine>().Where(r => r.Name == "Test");

            results.Should().BeEmpty();
        }

        [Test]
        public void Query_Rungs_ShouldReturnsRungs()
        {
            var content = LogixContent.Load(Known.Test);

            var results = content.Query<Rung>().SelectMany(t => t.Text.Tags()).Distinct().ToList();

            results.Should().NotBeEmpty();
        }
        
        [Test]
        public void Query_DistinctReferencedTagNames_ShouldReturnsLotsOfTagNames()
        {
            var content = LogixContent.Load(Known.Template);

            var results = content.Query<Rung>().SelectMany(t => t.Text.Tags()).Distinct().ToList();

            results.Should().NotBeEmpty();
        }
        
        [Test]
        public void Query_TagsInMovInstructions_ShouldReturnsLotsOfTagNames()
        {
            var content = LogixContent.Load(Known.Template);
        }
    }
}