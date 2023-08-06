using FluentAssertions;
using L5Sharp.Samples;

namespace L5Sharp.Tests;

[TestFixture]
public class LogixContentMergeTests
{
    [Test]
    public void Merge_DataTypeFileToTestFile_ShouldContainUpdatedComponent()
    {
        var content = LogixContent.Load(Known.Test);
        var dataType = LogixContent.Load(Export.BoolTest);
        
        content.Import(dataType);

        var result = content.DataTypes.Find("BoolTest");
        result.Should().NotBeNull();
    }
}