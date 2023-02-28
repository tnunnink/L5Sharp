using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Serialization.Data;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization;

/// <summary>
/// A logix serializer that performs serialization of <see cref="Parameter"/> objects.
/// </summary>
public class ParameterSerializer : ILogixSerializer<Parameter>
{
    /// <inheritdoc />
    public XElement Serialize(Parameter obj)
    {
        Check.NotNull(obj);

        var element = new XElement(L5XName.Parameter);
        element.AddValue(obj, p => p.Name);
        element.AddText(obj, p => p.Description);
        element.AddValue(obj, p => p.TagType);
        element.AddValue(obj, p => p.DataType, t => t != TagType.Alias);
        element.AddValue(obj.Dimension, L5XName.Dimensions, d => !d.IsEmpty);
        element.AddValue(obj, p => p.Usage);
        element.AddValue(obj, p => p.Radix, r => r != Radix.Null);

        if (obj.TagType == TagType.Alias)
            element.AddValue(obj, p => p.AliasFor);

        element.AddValue(obj, p => p.Required);
        element.AddValue(obj, p => p.Visible);

        if (obj.Usage == TagUsage.InOut)
            element.AddValue(obj, p => p.Constant);

        if (obj.Usage != TagUsage.InOut)
            element.AddValue(obj, p => p.ExternalAccess);

        return element;
    }

    /// <inheritdoc />
    public Parameter Deserialize(XElement element)
    {
        Check.NotNull(element);

        var defaultDataValue = element.Descendants(L5XName.DataValue).FirstOrDefault();

        return new Parameter
        {
            Name = element.LogixName(),
            Description = element.LogixDescription(),
            DataType = element.TryGetValue<string>(L5XName.DataType) ?? string.Empty,
            Dimension = element.TryGetValue<Dimensions>(L5XName.Dimensions) ?? Dimensions.Empty,
            Radix = element.TryGetValue<Radix>(L5XName.Radix) ?? Radix.Null,
            ExternalAccess = element.TryGetValue<ExternalAccess>(L5XName.ExternalAccess) ?? ExternalAccess.ReadWrite,
            Usage = element.TryGetValue<TagUsage>(L5XName.Usage) ?? TagUsage.Input,
            TagType = element.TryGetValue<TagType>(L5XName.TagType) ?? TagType.Base,
            AliasFor = element.TryGetValue<TagName>(L5XName.AliasFor) ?? TagName.Empty,
            Required = element.GetValue<bool>(L5XName.Required),
            Visible = element.GetValue<bool>(L5XName.Visible),
            Constant = element.TryGetValue<bool?>(L5XName.Constant) ?? false,
            Default = defaultDataValue is not null ? TagDataSerializer.DataValue.Deserialize(defaultDataValue) : null
        };
    }
}