using System;
using System.Xml.Linq;
using System.Xml.Serialization;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Components;

/// <summary>
/// A component of the <see cref="DataType"/> that makes up the structure of the user defined type.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
[XmlType(L5XName.Member)]
public class DataTypeMember : LogixComponent<DataTypeMember>
{
    /// <summary>
    /// Creates a new <see cref="DataTypeMember"/> with default values.
    /// </summary>
    public DataTypeMember()
    {
        Name = string.Empty;
        DataType = string.Empty;
        Dimension = Dimensions.Empty;
        Radix = Radix.Null;
        ExternalAccess = ExternalAccess.ReadWrite;
    }

    /// <summary>
    /// Creates a new <see cref="DataTypeMember"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    public DataTypeMember(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The name of the data type of the member.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> containing the data type name of the member.
    /// Default is <see cref="string.Empty"/>.
    /// Valid value is required for valid import.
    /// </value>
    public string DataType
    {
        get => GetValue<string>() ??
               throw new InvalidOperationException($"Required property {nameof(DataType)} does not exist.");
        set => SetValue(value);
    }

    /// <summary>
    /// The array dimension of the member.
    /// </summary>
    /// <value>
    /// A <see cref="Core.Dimensions"/> representing the array dimensions of the member.
    /// Default is <see cref="Core.Dimensions.Empty"/>.
    /// Members should not have multidimensional arrays.
    /// </value>
    public Dimensions? Dimension
    {
        get => GetValue<Dimensions>();
        set => SetValue(value);
    }

    /// <summary>
    /// The radix value format of the member.
    /// </summary>
    /// <value>
    /// A <see cref="Enums.Radix"/> representing the value type format of the member.
    /// Default is <see cref="Enums.Radix.Null"/>.
    /// Should be <see cref="Enums.Radix.Null"/> for all non atomic types.
    /// </value>
    public Radix? Radix
    {
        get => GetValue<Radix>();
        set => SetValue(value?.Value);
    }

    /// <summary>
    /// The external access of the member. 
    /// </summary>
    /// <value>
    /// A <see cref="Enums.ExternalAccess"/> representing read/write access of the member.
    /// Default is <see cref="Enums.ExternalAccess.ReadWrite"/>.
    /// </value>
    public ExternalAccess? ExternalAccess
    {
        get => GetValue<ExternalAccess>();
        set => SetValue(value);
    }

    /// <inheritdoc />
    public override DataTypeMember Clone() => new(Serialize());
}