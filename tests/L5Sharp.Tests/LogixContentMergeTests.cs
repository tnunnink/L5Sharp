using FluentAssertions;
using L5Sharp.Extensions;

namespace L5Sharp.Tests;

[TestFixture]
public class LogixContentMergeTests
{
    [Test]
    public void Merge_DataTypeFileToTestFile_ShouldContainUpdatedComponent()
    {
        var content = LogixContent.Load(Known.Test);
        var dataType = LogixContent.Load(Known.DataTypeContent);
        
        content.Merge(dataType, true);

        var result = content.DataTypes.Find("BoolTest");
        result.Should().NotBeNull();
    }
}