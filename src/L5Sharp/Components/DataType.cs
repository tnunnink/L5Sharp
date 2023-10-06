using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Elements;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp.Components;

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
public class DataType : LogixComponent<DataType>
{
    /// <summary>
    /// Creates a new <see cref="DataType"/> with default values.
    /// </summary>
    public DataType()
    {
        Family = DataTypeFamily.None;
        Class = DataTypeClass.User;
        Members = new LogixContainer<DataTypeMember>(L5XName.Members);
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

    /// <summary>
    /// Returns a collection of all <see cref="LogixElement"/> objects that reference this component by name.
    /// </summary>
    /// <returns>
    /// A <see cref="IEnumerable{T}"/> containing <see cref="LogixElement"/> objects that have
    /// at least one property value referencing this component's name.
    /// </returns>
    public override IEnumerable<LogixElement> References()
    {
        var content = Element.Ancestors(L5XName.RSLogix5000Content).FirstOrDefault();
        if (content is null)
            return Enumerable.Empty<LogixElement>();

        var references = new List<LogixElement>();

        foreach (var descendant in content.Descendants())
        {
            if (!descendant.Value.Contains(ToString()) &&
                !descendant.Attributes().Any(a => a.Value.Contains(ToString()))) continue;
            
            var element = LogixSerializer.Deserialize(descendant);
            
            //For DataTypes we want to add the actual tag members that reference the data type and not just the root tag.
            //This is why we are overriding this method here.
            //DataTypes are one of the few component that are referenced by tags.
            if (element is Tag tag)
            {
                var tags = tag.Members(t => t.DataType == Name).ToList();
                foreach (var child in tags)
                {
                    if (references.Any(r => r is Tag t && t.TagName == child.TagName)) continue;
                    references.Add(child);
                }
                continue;
            }

            if (element is not null)
            {
                references.Add(element);
            }
        }
        
        return references;
    }
}