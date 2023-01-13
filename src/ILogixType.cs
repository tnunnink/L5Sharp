using L5Sharp.Enums;

namespace L5Sharp
{
    public interface ILogixType
    {
        /// <summary>
        /// 
        /// </summary>
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