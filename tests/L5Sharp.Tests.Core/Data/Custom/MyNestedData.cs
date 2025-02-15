using System.Xml.Linq;

// ReSharper disable MemberCanBePrivate.Global

// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace L5Sharp.Tests.Core.Data.Custom
{
    /// <summary>
    /// A test type used to test nested complex data structure code
    /// </summary>
    //inheriting from complex type allows me to change structure after instantiated.
    public class MyNestedData : ComplexData
    {
        public MyNestedData() : base(nameof(MyNestedData))
        {
            Indy = new BOOL();
            Str = new STRING();
            Tmr = new TIMER();
            Simple = new MySimpleData();
            Flags = ArrayData.New<BOOL>(10);
            Counters = ArrayData.New<COUNTER>(3);
            Names = ArrayData.New<STRING>(5);
        }

        public MyNestedData(XElement element) : base(element)
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
        public MySimpleData Simple
        {
            get => GetMember<MySimpleData>();
            set => SetMember(value);
        }

        /// <summary>
        /// A nested array of atomic values.
        /// </summary>
        public ArrayData<BOOL> Flags
        {
            //All arrays are deserialized as generic ArrayType<LogixType> but can be casted like this to get ArrayType<TType>
            get => GetMember<ArrayData<LogixData>>().Cast<BOOL>();
            set => SetMember(value);
        }

        /// <summary>
        /// A nested array of structure types.
        /// </summary>
        public ArrayData<COUNTER> Counters
        {
            get => GetMember<ArrayData<LogixData>>().Cast<COUNTER>();
            set => SetMember(value);
        }

        /// <summary>
        /// A nested array of atomic values.
        /// </summary>
        public ArrayData<STRING> Names
        {
            get => GetMember<ArrayData<LogixData>>().Cast<STRING>();
            set => SetMember(value);
        }
    }
}