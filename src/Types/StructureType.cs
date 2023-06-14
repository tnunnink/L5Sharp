using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Extensions;

namespace L5Sharp.Types;

/// <summary>
/// A <see cref="L5Sharp.LogixType"/> that represents a complex structure containing members of different types.
/// </summary>
/// <remarks>
/// <para>
/// This type is a building block for all <c>Predefined</c> data types. Inherit from this class to create custom
/// user defined data types that can be used to create in memory representation of the tags for those types.
/// </para>
/// </remarks>
public class StructureType : LogixType
{
    /// <inheritdoc />
    public StructureType(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Creates a new <see cref="StructureType"/> with the provided name and collection of <see cref="Member"/> objects.
    /// </summary>
    /// <param name="name">The name of the structure type.</param>
    /// <param name="members">The members of the structure type.</param>
    /// <exception cref="ArgumentException"><c>name</c> is null or empty.</exception>
    /// <exception cref="ArgumentNullException"><c>members</c> is null.</exception>
    public StructureType(string name, IEnumerable<Member> members) : base(GenerateElement(name, members))
    {
    }

    /// <summary>
    /// Creates a new <see cref="StructureType"/> instance.
    /// </summary>
    /// <param name="name">The name of the type.</param>
    /// <exception cref="ArgumentException"><c>name</c> is null or empty.</exception>
    protected StructureType(string name) : base(GenerateElement(name, Enumerable.Empty<Member>()))
    {
    }

    /// <summary>
    /// Gets the member logix type for the specified name. 
    /// </summary>
    /// <param name="name">The member name to find.</param>
    /// <typeparam name="TLogixType">The logix type to return.</typeparam>
    /// <returns>A new <see cref="L5Sharp.LogixType"/> representing the deserialized data of the underlying type element member.</returns>
    /// <exception cref="L5XException">A member with the specified name was not found in the underlying element.</exception>
    /// <remarks>
    /// This method is for users implementing custom user defined or predefined types.
    /// Use this as the getter for all members of the type.
    /// </remarks>
    protected TLogixType GetMember<TLogixType>([CallerMemberName] string? name = null) where TLogixType : LogixType =>
        Members.SingleOrDefault(m => m.Name == name)?.DataType.To<TLogixType>() ??
        throw new L5XException(Element, name);

    /// <summary>
    /// Sets the underlying member element of the structure type with the provided logix type. 
    /// </summary>
    /// <param name="value">The logix type value to set.</param>
    /// <param name="name">The name of the member to set.</param>
    /// <typeparam name="TLogixType">The logix type parameter of the member.</typeparam>
    /// <exception cref="ArgumentNullException"><c>name</c> or <c>value</c> is null.</exception>
    /// <remarks>
    /// This method is for users implementing custom user defined or predefined types.
    /// Use this as the setter for all members of the type. Internally this will update the L5X data structure
    /// by replacing the member with a new serialized member created from the provided name and logix type.
    /// </remarks>
    protected void SetMember<TLogixType>(TLogixType value, [CallerMemberName] string? name = null)
        where TLogixType : LogixType
    {
        if (name is null)
            throw new ArgumentNullException(nameof(name));

        var member = new Member(name, value).Serialize();

        var target = Element.Elements().SingleOrDefault(e => e.MemberName() == name);

        if (target is null)
        {
            Element.Add(member);
            return;
        }

        target.ReplaceWith(member);
    }

    /// <summary>
    /// Generates the default backing element for the <see cref="StructureType"/>.
    /// </summary>
    /// <param name="name">The name of the structure type.</param>
    /// <param name="members">The structure type's member.</param>
    /// <returns>A new <see cref="XElement"/> containing the default L5X data.</returns>
    private static XElement GenerateElement(string name, IEnumerable<Member> members)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty for a structure type object.");

        if (members is null)
            throw new ArgumentNullException(nameof(members), "Members can not be null for a structure type object.");

        var element = new XElement(L5XName.Structure);
        element.Add(new XAttribute(L5XName.DataType, name));
        element.Add(members.Select(m => m.Serialize()));
        return element;
    }
}