using FluentAssertions;

namespace L5Sharp.Tests;

[TestFixture]
public class LogixContentImportTests
{
    [Test]
    public void Import_DataTypeFileToTestFile_ShouldContainUpdatedComponent()
    {
        var content = LogixContent.Load(Known.Test);
        var dataType = LogixContent.Load(Known.DataTypeContent);
        
        content.Import(dataType, true);

        var result = content.DataTypes().Find("BoolTest");

        result.Should().NotBeNull();
    }

    [Test]
    public void GetContainers_ShouldWork()
    {
        var content = LogixContent.Load(Known.Test);

        var containers = content.L5X.GetContainers().ToList();

        containers.Should().NotBeEmpty();
    }
    
    [Test]
    public void GetContainersBoolTest_ShouldWork()
    {
        var content = LogixContent.Load(Known.DataTypeContent);

        var containers = content.L5X.GetContainers().ToList();

        containers.Should().NotBeEmpty();
    }
}