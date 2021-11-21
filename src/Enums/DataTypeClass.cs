using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    public class DataTypeClass : SmartEnum<DataTypeClass>
    {
        private DataTypeClass(string name, int value) : base(name, value)
        {
        }

        public static readonly DataTypeClass Atomic = new DataTypeClass("Atomic", 0);

        public static readonly DataTypeClass Predefined = new DataTypeClass("ProductDefined", 1);

        public static readonly DataTypeClass Io = new DataTypeClass("IO", 2);

        public static readonly DataTypeClass User = new DataTypeClass("User", 3);

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