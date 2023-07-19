using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Types;

/// <summary>
/// A <see cref="L5Sharp.LogixType"/> that represents value type object.
/// </summary>
/// <remarks>
/// <para>
/// Logix atomic types are types that have value (e.g., BOOL, SINT, INT, DINT, REAL, etc.).
/// These type are synonymous with value types in .NET and in fact wrap the .NET value types internally while adding
/// the common <see cref="LogixType"/> API. Atomic types also add <see cref="Radix"/> to indicate the format of the current
/// type value.
/// </para>
/// <para>
/// All derived atomic types will implement value equality and comparison semantics to allow common operations to be performed.
/// They will also implement the <see cref="IConvertible"/> interface explicitly so to allow conversion between types. 
/// </para>
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public abstract class AtomicType : LogixType
{
    /// <inheritdoc />
    public sealed override DataTypeFamily Family => DataTypeFamily.None;

    /// <inheritdoc />
    public sealed override DataTypeClass Class => DataTypeClass.Atomic;

    /// <inheritdoc />
    public override IEnumerable<LogixMember> Members
    {
        get
        {
            var bits = new BitArray(GetBytes());
            for (var i = 0; i < bits.Count; i++)
            {
                var member = new LogixMember(i.ToString(), new BOOL(bits[i]));
                member.DataChanged += OnMemberDataChanged;
                yield return member;
            }
        }
    }

    /// <summary>
    /// The radix format for the <see cref="AtomicType"/>.
    /// </summary>
    /// <value>A <see cref="Enums.Radix"/> representing the format of the atomic type value.</value>
    public abstract Radix Radix { get; }

    /// <summary>
    /// Returns the <see cref="AtomicType"/> value as an array of <see cref="byte"/> values.
    /// </summary>
    /// <returns>An array of <see cref="byte"/> representing the value of the type.</returns>
    public abstract byte[] GetBytes();

    /// <summary>
    /// Return the atomic value formatted using the current <see cref="Radix"/> format.
    /// </summary>
    /// <returns>A <see cref="string"/> representing the formatted atomic value.</returns>
    public override string ToString() => Radix.Format(this);

    /// <summary>
    /// Returns the atomic value formatted in the specified <see cref="Enums.Radix"/> format.
    /// </summary>
    /// <param name="radix">The radix format.</param>
    /// <returns>A <see cref="string"/> representing the formatted atomic value.</returns>
    public string ToString(Radix radix) => radix.Format(this);

    /// <summary>
    /// Serialized the atomic type as the DataValue <see cref="XElement"/>.
    /// </summary>
    /// <returns>A <see cref="XElement"/> containing the data for the atomic type.</returns>
    public override XElement Serialize()
    {
        var element = new XElement(L5XName.DataValue);
        element.Add(new XAttribute(L5XName.DataType, Name));
        element.Add(new XAttribute(L5XName.Radix, Radix));
        element.Add(new XAttribute(L5XName.Value, ToString()));
        return element;
    }

    /// <summary>
    /// Gets a bit member for the atomic type at the specified bit index.
    /// </summary>
    /// <param name="index">The zero based bit index of the value to get.</param>
    /// <returns>A <see cref="LogixMember"/> representing the bit member of the atomic type.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range for the atomic type value.</exception>
    protected LogixMember BitMember(int index)
    {
        return Member(index.ToString()) ??
               throw new ArgumentOutOfRangeException(nameof(index),
                   $"The bit index {index} is out of range for a {Name} atomic value.");
    }

    /// <summary>
    /// Trigger the <see cref="LogixType.DataChanged"/> event when a atomic member data changed event is fired to forward
    /// the call up the type/member hierarchy.
    /// </summary>
    /// <param name="sender">The member sending the data changed event.</param>
    /// <param name="e">The event args.</param>
    protected virtual void OnMemberDataChanged(object sender, EventArgs e) => RaiseDataChanged(sender);
}