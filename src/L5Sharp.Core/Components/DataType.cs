﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;


namespace L5Sharp.Core;

/// <summary>
/// The Logix <c>DataType</c> component. Contains the properties and functionality that comprise the L5X DataType element.
/// </summary>
/// <remarks>
/// <para>
/// A DataType represents a user-defined type structure that one can define and reuse within a PLC project.
/// This type inherits <see cref="LogixComponent"/> which specifies <c>Name</c> and <c>Description</c> properties along with other
/// common component functionality. DataTypes also contain <see cref="Members"/> that define the structure of the
/// complex type. This class does not represent the type value, but rather the configuration of how a tag structure
/// would look if it referenced this type.
/// </para>
/// <para>DataType components can be defined out of order within an L5X. This means a type that is dependent on another
/// type can be defined first, and Logix will import just fine.</para>
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public class DataType : LogixComponent<DataType>
{
    /// <inheritdoc />
    protected override List<string> ElementOrder =>
    [
        L5XName.Description,
        L5XName.Members
    ];

    /// <summary>
    /// Creates a new <see cref="DataType"/> with default values.
    /// </summary>
    public DataType() : base(L5XName.DataType)
    {
        Family = DataTypeFamily.None;
        Class = DataTypeClass.User;
        Members = [];
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
        Element.SetAttributeValue(L5XName.Name, name);
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
        return Members.SelectMany(m => L5X.DependenciesForType(m.DataType)).Distinct(c => c.Name);
    }

    /// <summary>
    /// Adds a new member to the current <see cref="DataType"/> with the specified name, data type, and optional configuration.
    /// </summary>
    /// <param name="name">The name of the member to add. Must not be null or empty.</param>
    /// <param name="dataType">The data type of the member. Must not be null or empty.</param>
    /// <param name="config">An optional configuration action to modify the member after creation.</param>
    /// <returns>The newly added <see cref="DataTypeMember"/>.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown if the <paramref name="name"/> or <paramref name="dataType"/> is null or empty.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if a member with the given <paramref name="name"/> already exists in the <see cref="DataType"/>.
    /// </exception>
    public DataTypeMember AddMember(string name, string dataType, Action<DataTypeMember>? config = null)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Member property 'Name' can not be null or empty");

        if (string.IsNullOrEmpty(dataType))
            throw new ArgumentException("Member property 'DataType' can not be null or empty");

        if (Members.Any(m => m.Name == name))
            throw new InvalidOperationException($"A member with the name '{name}' already exists for type '{Name}'");

        var member = new DataTypeMember { Name = name, DataType = dataType };
        config?.Invoke(member);
        Members.Add(member);
        return member;
    }

    /// <summary>
    /// Removes a member with the specified name from the <see cref="Members"/> collection of the current DataType.
    /// </summary>
    /// <param name="name">The name of the member to be removed.</param>
    public void RemoveMember(string name)
    {
        Members.FirstOrDefault(m => m.Name == name)?.Remove();
    }

    /// <summary>
    /// Updates the specified member of the data type by applying the provided configuration.
    /// </summary>
    /// <param name="name">
    /// The name of the member to be updated. Must correspond to an existing member of the data type.
    /// </param>
    /// <param name="config">
    /// An action that defines the configuration to apply to the selected member.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// Thrown when no member with the specified name is found in the data type.
    /// </exception>
    public void UpdateMember(string name, Action<DataTypeMember> config)
    {
        if (config is null)
            throw new ArgumentNullException(nameof(config));

        var member = Members.FirstOrDefault(m => m.Name == name);

        if (member is null)
            throw new InvalidOperationException($"No member with name '{name}' is defined for type '{Name}'");

        config.Invoke(member);
    }

    /// <summary>
    /// Creates a new <see cref="Tag"/> instance by converting this <see cref="DataType"/> definition into a
    /// <see cref="LogixData"/> instance and using that along with the provided tag name to create the component.
    /// </summary>
    /// <param name="tagName">The name of the tag.</param>
    /// <returns>A <see cref="Tag"/> with the provided name and populated data structure using this data type definition.</returns>
    /// <remarks>
    /// If this data type has nested (user) data types, then internally this feature will attempt to retrieve
    /// and create them either statically or from the attached L5X. If this type is not attached or the nested types
    /// are not resolved, it will default to creating <see cref="ComplexData"/> objects with no members
    /// as there is no way to know the structure. 
    /// </remarks>
    public Tag ToTag(string tagName) => new(tagName, ToData());

    /// <summary>
    /// Converts the <see cref="DataType"/> component into a <see cref="LogixData"/> object using the configured
    /// name and members.
    /// </summary>
    /// <returns>A <see cref="LogixData"/> object populated with the members defined by this data type configuration.</returns>
    /// <remarks>
    /// If this data type has nested (user) data types, then internally this feature will attempt to retrieve
    /// and create them either statically or from the attached L5X. If this type is not attached or the nested types
    /// are not resolved, it will default to creating <see cref="ComplexData"/> objects with no members
    /// as there is no way to know the structure. 
    /// </remarks>
    public LogixData ToData()
    {
        if (string.IsNullOrEmpty(Name))
            throw new InvalidOperationException("Can not create data with null or empty data type name");

        //We need to handle strings types specifically to match a Logix custom format.
        if (Family is not null && Family == DataTypeFamily.String) return new StringData(Name, string.Empty);

        //This will be some predefined type or a complex data instance, depending on whether it is statically defined.
        var data = LogixData.Create(Name);

        //If it is not a complex data, then it was defined, and we can return it.
        if (data is not ComplexData complexData) return data;

        //Otherwise, we need to build the members using the configured Members collection.
        var members = Members.Where(m => m.Hidden is not true).Select(m => m.ToMember()).ToList();
        complexData.AddRange(members);
        return complexData;
    }
}