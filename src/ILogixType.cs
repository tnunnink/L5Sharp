using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// A base interface for all logix data type classes. <see cref="ILogixType"/> is a construct that contains the actual
    /// data value that make up a give logix data type and/or tag structure.
    /// </summary>
    public interface ILogixType
    {
        /// <summary>
        /// The name of the <see cref="ILogixType"/> object.
        /// </summary>
        /// <value>A <see cref="string"/> name identifying the logix type.</value>
        public string Name { get; }
        
        /// <summary>
        /// Gets the <see cref="DataTypeFamily"/> of the <see cref="ILogixType"/>
        /// </summary>
        /// <value>A <see cref="DataTypeClass"/> enum indicating the family of the logix type.</value>
        DataTypeFamily Family { get; }

        /// <summary>
        /// Gets the <see cref="DataTypeClass"/> of the <see cref="ILogixType"/>
        /// </summary>
        /// <value>A <see cref="DataTypeClass"/> enum indicating the class of the logix type.</value>
        DataTypeClass Class { get; }
    }
}