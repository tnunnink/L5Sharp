using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Represents an abstract base class for alarm-related data within a Logix system.
/// </summary>
/// <remarks>
/// The <c>AlarmData</c> class serves as a foundation to define the structure for alarm semantics in the Logix architecture.
/// It extends from the <c>LogixData</c> class and provides a mechanism to handle virtual members for alarms.
/// The specific implementation details for alarms must be defined in derived classes.
/// </remarks>
public abstract class AlarmData : LogixData
{
    /// <inheritdoc />
    protected AlarmData(string name) : base(name)
    {
    }

    /// <inheritdoc />
    protected AlarmData(XElement element) : base(element)
    {
    }

    /// <inheritdoc />
    public override IEnumerable<LogixMember> Members => GenerateVirtualMembers();

    /// <inheritdoc />
    public override void UpdateData(LogixData data)
    {
        if (data is null)
            throw new ArgumentNullException(nameof(data));

        if (data is not AlarmData alarm)
            throw new ArgumentException($"Can not update alarm data with data of type '{data.GetType()}'");

        var pairs = Members.Join(alarm.Members, m => m.Name, m => m.Name, (a, b) => new { a, b });
        pairs.ToList().ForEach(p => p.a.Value.UpdateData(p.b.Value));
    }

    /// <summary>
    /// Alarm data is formatted as attribute name/value pairs instead of nested element data structure.
    /// To make this compatible with our implementation of LogixData, we need to return "virtual" data value member elements
    /// that match the typical decorated format. We can derive the virtual element using the attribute name, value,
    /// and the defined properties of this class. The one issue is setting the underlying attribute. To support that,
    /// the attribute will be stored as an annotation on the virtual member element. <see cref="AtomicData"/> will inspect
    /// the element and if the attribute exists, will apply the update accordingly.
    /// </summary>
    private IEnumerable<LogixMember> GenerateVirtualMembers()
    {
        var propertyTypes = GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => typeof(LogixData).IsAssignableFrom(p.PropertyType) && p.DeclaringType == GetType())
            .ToDictionary(p => p.Name, p => LogixType.NameFor(p.PropertyType));

        foreach (var attribute in Element.Attributes())
        {
            var member = new XElement(L5XName.DataValueMember);
            member.SetAttributeValue(L5XName.Name, attribute.Name.LocalName);
            member.SetAttributeValue(L5XName.DataType, propertyTypes[attribute.Name.LocalName]);
            member.SetAttributeValue(L5XName.Radix, Radix.Infer(attribute.Value));
            member.SetAttributeValue(L5XName.Value, attribute.Value);

            //This embeds the backing attribute on the in memory element so we can handle setting the value from atomic data.
            member.AddAnnotation(attribute);

            yield return new LogixMember(member);
        }
    }
}