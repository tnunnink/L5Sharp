using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Tests.Types.Custom;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;

// ReSharper disable MemberCanBePrivate.Global

// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace L5Sharp.Samples.Types
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

        public override DataTypeClass Class => DataTypeClass.User;

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
            get => GetMember<ArrayType<BOOL>>();
            set => SetMember(value);
        }

        /// <summary>
        /// A nested array of structure types.
        /// </summary>
        public ArrayType<COUNTER> Counters
        {
            get => GetMember<ArrayType<COUNTER>>();
            set => SetMember(value);
        }

        /// <summary>
        /// A nested array of atomic values.
        /// </summary>
        public ArrayType<STRING> Names
        {
            get => GetMember<ArrayType<STRING>>();
            set => SetMember(value);
        }
    }
}