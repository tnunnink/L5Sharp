using System.Collections.Generic;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// Represents a member or component of a complex data type structure.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Members make up the structure of <see cref="IComplexType"/>. Since each member holds a strongly typed reference to
    /// it's DataType, the structure forms a hierarchical tree of nested members and types. Member with Dimensions have
    /// instantiated elements that can be accessed by index.
    /// Use the static factory <see cref="Member"/> class to create instances of IMember.
    /// </para>
    /// </remarks>
    /// <typeparam name="TDataType">Represents the <see cref="IDataType"/> of the Member component.</typeparam>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer>
    public interface IMember<out TDataType> :
        ILogixComponent,
        IPrototype<IMember<TDataType>>,
        IEnumerable<IMember<TDataType>>
        where TDataType : IDataType
    {
        /// <summary>
        /// Gets the DataType of the Member Component.
        /// </summary>
        TDataType DataType { get; }

        /// <summary>
        /// Gets the Dimension of the Member component.
        /// </summary>
        Dimensions Dimensions { get; }

        /// <summary>
        /// Gets the Radix of the Member component.
        /// </summary>
        Radix Radix { get; }

        /// <summary>
        /// Gets the ExternalAccess of the Member component.
        /// </summary>
        ExternalAccess ExternalAccess { get; }
        
        /// <summary>
        /// Gets an Member element at the specified index.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Only Members with Dimensions will have elements instantiated and accessible by the index getter.
        /// Each element of the Member's array are of the same type as the root/seed type of the member. The Member's
        /// Radix, ExternalAccess, and Description are propagated to each element on construction. 
        /// </para>
        /// </remarks>
        /// <param name="index">The index of the member's element to retrieve.</param>
        /// <returns>
        /// Null if this Member is dimensionless (i.e. Dimensions are equal to <see cref="Core.Dimensions.Empty"/>),
        /// or the provided index is out of bounds of the Member's array (specified by <c>Dimensions.Length</c>).
        /// Otherwise, an instance of <see cref="IMember{TDataType}"/> representing the element of the specified index.
        /// </returns>
        IMember<TDataType>? this[int index] { get; }
    }
}