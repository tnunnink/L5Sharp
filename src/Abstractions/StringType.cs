using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using L5Sharp.Atomics;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Abstractions
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class StringType : IStringType
    {
        private readonly List<IMember<IDataType>> _members;

        internal StringType(string name, ushort length)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));

            LEN = new Member<Dint>(nameof(LEN), new Dint(length));

            var array = new ArrayType<Sint>(new Dimensions(length), radix: Radix.Ascii);
            DATA = new Member<IArrayType<Sint>>(nameof(DATA), array, Radix.Ascii);

            _members = new List<IMember<IDataType>>
            {
                LEN,
                DATA
            };
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public virtual string Description => $"Logix representation of a {typeof(string)}";

        /// <inheritdoc />
        public DataTypeFamily Family => DataTypeFamily.String;

        /// <inheritdoc />
        public abstract DataTypeClass Class { get; }

        /// <inheritdoc />
        public IEnumerable<IMember<IDataType>> Members => _members.AsEnumerable();

        /// <inheritdoc />
        public string Value => Encoding.ASCII.GetString(DATA.DataType.Where(d => d.DataType.Value > 0)
                .Select(d => (byte)d.DataType.Value)
                .ToArray());

        /// <inheritdoc />
        public int Length => Value.Length;

        /// <inheritdoc />
        public IMember<Dint> LEN { get; }

        /// <inheritdoc />
        public IMember<IArrayType<Sint>> DATA { get; }

        /// <inheritdoc />
        public IDataType Instantiate() => New();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected abstract IDataType New();

        /// <inheritdoc />
        public void SetValue(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var bytes = Encoding.ASCII.GetBytes(value).Select(b => (sbyte)b).ToArray();

            if (bytes.Length > LEN.DataType.Value)
                throw new ArgumentOutOfRangeException(nameof(value),
                    $"Value length '{bytes.Length}' must be less than the predefined length '{LEN.DataType.Value}'");

            foreach (var element in DATA.DataType)
                element.DataType.SetValue(0);

            for (var i = 0; i < bytes.Length; i++)
                DATA.DataType[i].DataType.SetValue(bytes[i]);
        }
        
        /// <inheritdoc />
        public IEnumerator<char> GetEnumerator() => Value.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}