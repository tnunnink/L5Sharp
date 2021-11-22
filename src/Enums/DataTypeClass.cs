using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    /// <summary>
    /// Represents a set of categories that a <c>DataType</c> can belong to.
    /// </summary>
    /// <remarks>
    /// L5X only exports User class types.
    /// <see cref="Atomic"/> types are value types (i.e. Bool, Int, Real, etc..).
    /// <see cref="Predefined"/> types represent other built in types (i.e. Timer, Counter, Message, etc.).
    /// 
    /// </remarks>
    public class DataTypeClass : SmartEnum<DataTypeClass>
    {
        private DataTypeClass(string name, int value) : base(name, value)
        {
        }

        /// <summary>
        /// Represents a data type that has value and radix. See <see cref="IAtomic"/>
        /// </summary>
        /// <example>
        /// Bool, Sint, Int, Dint, Lint, Real.
        /// </example>
        public static readonly DataTypeClass Atomic = new DataTypeClass("Atomic", 0);

        /// <summary>
        /// Represents a built in data type that must be preconfigured. See <see cref="IPredefined"/>.
        /// </summary>
        /// <example>
        /// String, Timer, Counter.
        /// </example>
        public static readonly DataTypeClass Predefined = new DataTypeClass("ProductDefined", 1);

        /// <summary>
        /// Represents a type that is defined for Logix Modules. See <see cref="IModuleDefined"/>.
        /// </summary>
        public static readonly DataTypeClass Io = new DataTypeClass("IO", 2);

        /// <summary>
        /// Represents a type that is defined by the user. See <see cref="IUserDefined"/>.
        /// </summary>
        public static readonly DataTypeClass User = new DataTypeClass("User", 3);
        
        /// <summary>
        /// Represents a type that is defined by an AOI. See <see cref="IAddOnDefined"/>
        /// </summary>
        public static readonly DataTypeClass AddOnDefined = new DataTypeClass("AddOn", 4);

        /// <summary>
        /// Gets the <see cref="DataTypeClass"/> of the current <c>TagMember</c>.
        /// </summary>
        /// <typeparam name="TDataType">The type of the current tag member data type.</typeparam>
        /// <returns></returns>
        public static DataTypeClass FromType<TDataType>() where TDataType : IDataType
        {
            if (typeof(IAtomic).IsAssignableFrom(typeof(TDataType)))
                return Atomic;
            
            if (typeof(IUserDefined).IsAssignableFrom(typeof(TDataType)))
                return User;
            
            if (typeof(IPredefined).IsAssignableFrom(typeof(TDataType)))
                return Predefined;
            
            if (typeof(IAddOnDefined).IsAssignableFrom(typeof(TDataType)))
                return AddOnDefined;
            
            return typeof(IModuleDefined).IsAssignableFrom(typeof(TDataType)) ? Io : null;
        }
    }
}