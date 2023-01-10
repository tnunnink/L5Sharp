using L5Sharp.Enums;

namespace L5Sharp
{
    public interface ILogixType
    {
        /// <summary>
        /// Gets the name of the <see cref="ILogixType"/>.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the <see cref="DataTypeFamily"/> of the <see cref="ILogixType"/>
        /// </summary>
        DataTypeFamily Family { get; }
        
        /// <summary>
        /// Gets the <see cref="DataTypeClass"/> of the <see cref="ILogixType"/>
        /// </summary>
        DataTypeClass Class { get; }
    }
}