using System.Xml.Linq;
using FluentAssertions;

namespace L5Sharp.Tests.Core.Utilities;

[TestFixture]
public class L5XExtensionTests
{
    [Test]
    public void L5XType_AddOnInstruction_ShouldBeExpectedValue()
    {
        var type = typeof(AddOnInstruction).GetLogixTypeName();

        type.Should().Be("AddOnInstructionDefinition");
    }
    
    [Test]
    public void L5XType_DataType_ShouldBeExpectedValue()
    {
        var type = typeof(DataType).GetLogixTypeName();

        type.Should().Be("DataType");
    }
    
    [Test]
    public void L5XType_Module_ShouldBeExpectedValue()
    {
        var type = typeof(Module).GetLogixTypeName();

        type.Should().Be("Module");
    }
    
    [Test]
    public void L5XType_Program_ShouldBeExpectedValue()
    {
        var type = typeof(Program).GetLogixTypeName();

        type.Should().Be("Program");
    }
    
    [Test]
    public void L5XType_Routine_ShouldBeExpectedValue()
    {
        var type = typeof(Routine).GetLogixTypeName();

        type.Should().Be("Routine");
    }
    
    [Test]
    public void L5XType_Tag_ShouldBeExpectedValue()
    {
        var type = typeof(Tag).GetLogixTypeName();

        type.Should().Be("Tag");
    }
    
    [Test]
    public void L5XType_Task_ShouldBeExpectedValue()
    {
        var type = typeof(Task).GetLogixTypeName();

        type.Should().Be("Task");
    }

    [Test]
    public void L5XTypes_Tag_ShouldHaveExpectedCount()
    {
        var types = typeof(Tag).GetLogixTypeNames().ToList();

        types.Should().NotBeEmpty();
        types.Should().ContainSingle(s => s == "Tag");
        types.Should().ContainSingle(s => s == "ConfigTag");
        types.Should().ContainSingle(s => s == "InputTag");
        types.Should().ContainSingle(s => s == "OutputTag");
    }

    [Test]
    public void L5XContainerType_Tag_ShouldHaveExpectedValue()
    {
        var type = typeof(Tag).GetLogixContainerName();

        type.Should().Be("Tags");
    }

    [Test]
    public void LogixPath_GlobalElementFromKnownContent_ShouldBeExpected()
    {
        var content = XElement.Load(Known.Test);
        var element = content.Descendants(L5XName.Tag).FirstOrDefault();

        var path = element?.LogixPath();

        path.Should().Be("Controller/Tags/Tag[@Name='AliasTag']");
    }
    
    [Test]
    public void LogixPath_InMemoryElement_ShouldBeExpected()
    {
        var tag = new Tag("Test", 123);

        var path = tag.Serialize().LogixPath();

        path.Should().Be("Tag[@Name='Test']");
    }
    
    [Test]
    public void TagName_NoRootTag_ShouldBeEmpty()
    {
        var content = XElement.Parse(Sample.DataTypeElement.SimpleType());

        var tagName = content.TagName();

        tagName.Should().Be(TagName.Empty);
    }
    
    [Test]
    public void TagName_TestComplexTagRoot_ShouldBeExpectedValue()
    {
        var content = XElement.Parse(Sample.TagElement.TestComplexTag());

        var tagName = content.TagName();

        tagName.Should().Be("TestComplexTag");
    }

    [Test]
    public void TagName_TestComplexTagNestedMember_ShouldBeExpectedValue()
    {
        var content = XElement.Parse(Sample.TagElement.TestComplexTag());
        var element = content.Descendants("DataValueMember").FirstOrDefault(e => e.MemberName() == "DintMember");

        var tagName = element?.TagName();

        tagName.Should().Be("TestComplexTag.SimpleMember.DintMember");
    }
    
    [Test]
    public void TagName_TestComplexTagNestedArray_ShouldBeExpectedValue()
    {
        var content = XElement.Parse(Sample.TagElement.TestComplexTag());
        var element = content.Descendants("ArrayMember").FirstOrDefault()?
            .Descendants("DataValueMember").FirstOrDefault(e => e.MemberName() == "DintMember");

        var tagName = element?.TagName();

        tagName.Should().Be("TestComplexTag.SimplArray[0].DintMember");
    }
}