using System;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Represents a class-level attribute used to map a class to a specific L5X XML element type.
/// This attribute is applied to classes to associate them with a particular element name within the
/// L5X document structure to enable serialization and deserialization.
/// </summary>
/// <remarks>
/// When applied to a class, the <see cref="LogixElementAttribute"/> will automatically register the type with the
/// specified XML element name in the <see cref="LogixSerializer"/> using source generators. If you need to perform
/// custom registration, use the <see cref="LogixSerializer"/> to specify which element(s) the type corresponds to
/// and how the type should be created.
/// </remarks>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public class LogixElementAttribute : Attribute
{
    /// <summary>
    /// An attribute used to define metadata for Logix element classes. This attribute can be applied to classes
    /// to specify an associated element name or type.
    /// </summary>
    /// <remarks>
    /// The attribute is applied only to class types and is not inherited by derived classes. This is useful for
    /// marking classes with type-specific information for Logix elements.
    /// </remarks>
    public LogixElementAttribute(string elementName)
    {
        ElementName = elementName ?? throw new ArgumentNullException(nameof(elementName));
    }

    /// <summary>
    /// Gets the name of the XML element associated with the Logix element class.
    /// </summary>
    /// <remarks>
    /// This property specifies the L5X XML element name that the associated class
    /// maps to for serialization or deserialization purposes. It provides a clear
    /// linkage between a class and its corresponding XML representation within the
    /// L5X document structure.
    /// </remarks>
    public string ElementName { get; }
}