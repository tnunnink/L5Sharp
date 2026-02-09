using System.Text;
using L5Sharp.Core;

namespace L5Sharp.Tests.Generators.Data;

[TestFixture]
public class Scratch
{
    [Test]
    public void GenerateSizeReport()
    {
        var content = L5X.Load(@"C:\Users\tnunnink\Documents\Rockwell\Butane_SE_Template.L5X");

        var groups = content.Query<Tag>().GroupBy(t => t.DataType);

        var builder = new StringBuilder();
        builder.AppendLine("DataType, Size, Instances, Total");

        foreach (var group in groups)
        {
            var dataType = group.Key;
            var instances = group.Count();
            if (!LogixType.IsRegistered(dataType)) continue;
            var size = LogixType.SizeOf(dataType);
            var total = size * instances;
            var result = $"{dataType}, {size}, {instances}, {total}";
            builder.AppendLine(result);
        }

        File.WriteAllText(@"C:\Users\tnunnink\Desktop\AllTypesMemory.csv", builder.ToString());
    }
}