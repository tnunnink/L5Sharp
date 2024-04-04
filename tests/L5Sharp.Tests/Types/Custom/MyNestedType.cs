using System.Xml.Linq;

// ReSharper disable MemberCanBePrivate.Global

// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace L5Sharp.Tests.Types.Custom
{
    /// <summary>
    /// A test type used to test nested complex data structure code
    /// </summary>
    //inheriting from complex type allows me to change structure after instantiated.
    public class MyNestedType : ComplexType
    {
        public MyNestedType() : base(nameof(MyNestedType))
        {
            Indy = new BOOL();
            Str = new STRING();
            Tmr = new TIMER();
            Simple = new MySimpleType();
            Flags = ArrayType.New<BOOL>(10);
            Counters = ArrayType.New<COUNTER>(3);
            Names = ArrayType.New<STRING>(5);
        }

        public MyNestedType(XElement element) : base(element)
        {
        }

        /// <summary>
        /// A simple boolean member
        /// </summary>
        public BOOL Indy
        {
            get => GetMember<BOOL>();
            set => SetMember(value);
        }

        /// <summary>
        /// A string member
        /// </summary>
        public STRING Str
        {
            get => GetMember<STRING>();
            set => SetMember(value);
        }

        /// <summary>
        /// A nested timer member
        /// </summary>
        public TIMER Tmr
        {
            get => GetMember<TIMER>();
            set => SetMember(value);
        }

        /// <summary>
        /// A nested user defined type
        /// </summary>
        public MySimpleType Simple
        {
            get => GetMember<MySimpleType>();
            set => SetMember(value);
        }

        /// <summary>
        /// A nested array of atomic values.
        /// </summary>
        public ArrayType<BOOL> Flags
        {
            //All arrays are deserialized as generic ArrayType<LogixType> but can be casted like this to get ArrayType<TType>
            get => GetMember<ArrayType<LogixType>>().Cast<BOOL>();
            set => SetMember(value);
        }

        /// <summary>
        /// A nested array of structure types.
        /// </summary>
        public ArrayType<COUNTER> Counters
        {
            get => GetMember<ArrayType<LogixType>>().Cast<COUNTER>();
            set => SetMember(value);
        }

        /// <summary>
        /// A nested array of atomic values.
        /// </summary>
        public ArrayType<STRING> Names
        {
            get => GetMember<ArrayType<LogixType>>().Cast<STRING>();
            set => SetMember(value);
        }
    }
}