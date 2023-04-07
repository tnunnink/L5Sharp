using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization;

/// <summary>
/// A logix serializer that performs serialization of <see cref="DataTypeMember"/> objects.
/// </summary>
public class DataTypeMemberSerializer : ILogixSerializer<DataTypeMember>
{
    /// <inheritdoc />
    public XElement Serialize(DataTypeMember obj)
    {
        Check.NotNull(obj);

        var element = new XElement(L5XName.Member);
        element.AddValue(obj, m => m.Name);
        element.AddText(obj, m => m.Description);
        element.AddValue(obj, m => m.DataType);
        element.AddValue(obj, m => m.Dimension);
        element.AddValue(obj.Radix.Value, L5XName.Radix);
        element.AddValue(obj, m => m.ExternalAccess);
            
        return element;
    }

    /// <inheritdoc />
    public DataTypeMember Deserialize(XElement element)
    {
        Check.NotNull(element);

        return new DataTypeMember
        {
            Name = element.LogixName(),
            Description = element.LogixDescription(),
            DataType = element.GetValue<string>(L5XName.DataType),
            Dimension = element.GetValue<Dimensions>(L5XName.Dimension),
            Radix = element.GetValue<Radix>(L5XName.Radix),
            ExternalAccess = element.GetValue<ExternalAccess>(L5XName.ExternalAccess)
        };
    }
}