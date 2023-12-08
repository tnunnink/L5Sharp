using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A logix <c>DataType</c> component. Contains the properties that comprise the L5X DataType element.
/// </summary>
/// <remarks>
/// Observe these guidelines when defining a DataType:<br/>
///     • DataTypes must be defined first within the controller body.<br/>
///     • DataTypes can be defined out of order. For example, if Type1 depends on Type2, Type2 can be defined first.<br/>
///     • DataTypes can be unverified. For example if Type1 depends on Type2 and Type2 is never defined, then Type1<br/>
///       will be accessible as an unverified type. Type2 will be typeless type. Tags of Type1 may be created but not of Type2.<br/>
///     • Datatype members can be arrays but only one dimension is allowed.<br/>
///     • These DataTypes cannot be used in a user-defined datatype:<br/>
///     • ALARM_ANALOG<br/>
///     • ALARM_DIGITAL<br/>
///     • AXIS types<br/>
///     • COORDINATE_SYSTEM<br/>
///     • MOTION_GROUP<br/>
///     • MESSAGE<br/>
///     • MODULE<br/>
///     • If one user-defined datatype references a second user-defined datatype defined in the file, the second<br/>
///       user-defined datatype appears before the first one in the import/export file.<br/>
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public class DataType : LogixComponent
{
    /// <summary>
    /// Creates a new <see cref="DataType"/> with default values.
    /// </summary>
    public DataType()
    {
        Family = DataTypeFamily.None;
        Class = DataTypeClass.User;
        Members = new LogixContainer<DataTypeMember>();
    }

    /// <summary>
    /// Creates a new <see cref="DataType"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public DataType(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Creates a new <see cref="DataType"/> initialized with the provided name.
    /// </summary>
    /// <param name="name">The name of the data type component.</param>
    /// <exception cref="ArgumentNullException"><c>name</c> is null.</exception>
    public DataType(string name) : this()
    {
        if (name is null) throw new ArgumentNullException(nameof(name));
        SetValue(L5XName.Name, name);
    }

    /// <summary>
    /// The family of the <c>DataType</c> component.
    /// </summary>
    /// <value>
    /// A <see cref="DataTypeFamily"/> option indicating the family for which the current data type belongs.
    /// This is just string for string types and none for all others.
    /// </value>
    public DataTypeFamily? Family
    {
        get => GetValue<DataTypeFamily>();
        set => SetValue(value);
    }

    /// <summary>
    /// The class of the <c>DataType</c> component.
    /// </summary>
    /// <value>
    /// A <see cref="DataTypeClass"/> option indicating the class for which the current data type belongs.
    /// L5X files will only ever contain <see cref="DataTypeClass.User"/> class types.
    /// </value>
    public DataTypeClass? Class
    {
        get => GetValue<DataTypeClass>();
        set => SetValue(value);
    }

    /// <summary>
    /// The collection of <see cref="DataTypeMember"/> that make up the structure of the <c>DataType</c> component.
    /// </summary>
    public LogixContainer<DataTypeMember> Members
    {
        get => GetContainer<DataTypeMember>();
        set => SetContainer(value);
    }
    
    /// <inheritdoc />
    public override IEnumerable<LogixComponent> Dependencies()
    {
        if (L5X is null) return Enumerable.Empty<LogixComponent>();

        var dependencies = new List<LogixComponent>();

        foreach (var member in Members)
        {
            var dataType = L5X.Find<DataType>(member.DataType);
            if (dataType is null) continue;
            dependencies.Add(dataType);
            dependencies.AddRange(dataType.Dependencies());
        }

        return dependencies.Distinct();
    }
}