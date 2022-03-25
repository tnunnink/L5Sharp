using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Abstractions
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class StringType : IStringType
    {
        private readonly List<IMember<IDataType>> _members;

        internal StringType(string name, ushort dimensions)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));

            LEN = new Member<DINT>(nameof(LEN), new DINT());

            var array = new ArrayType<SINT>(new Dimensions(dimensions), radix: Radix.Ascii);
            DATA = new Member<IArrayType<SINT>>(nameof(DATA), array, Radix.Ascii);

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
        public int Length => Value.Length;

        /// <inheritdoc />
        public string Value => Encoding.ASCII.GetString(DATA.DataType.Where(d => d.DataType.Value > 0)
                .Select(d => (byte)d.DataType.Value)
                .ToArray());

        /// <inheritdoc />
        public IMember<DINT> LEN { get; }

        /// <inheritdoc />
        public IMember<IArrayType<SINT>> DATA { get; }

        /// <inheritdoc />
        public IDataType Instantiate() => New();

        /// <summary>
        /// Creates a new instance of the <see cref="IComplexType"/> with default values.
        /// </summary>
        /// <returns>A new <see cref="IDataType"/> instance with default values.</returns>
        /// <remarks>
        /// This method is called by <see cref="Instantiate"/> in order to generate new instances
        /// of the <see cref="IDataType"/>.
        /// </remarks>
        protected abstract IDataType New();

        /// <summary>
        /// Clears the current value to an empty string.
        /// </summary>
        public void ClearValue()
        {
            for (var i = 0; i < DATA.Dimensions.Length; i++)
                DATA.DataType[i].DataType.SetValue(0);
        }

        /// <inheritdoc />
        public void SetValue(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var bytes = Encoding.ASCII.GetBytes(value).Select(b => (sbyte)b).ToArray();

            if (bytes.Length > DATA.Dimensions.Length)
                throw new ArgumentOutOfRangeException(nameof(value),
                    $"Value length '{bytes.Length}' must be less than the predefined length '{LEN.DataType.Value}'");
            
            ClearValue();
            
            for (var i = 0; i < bytes.Length; i++)
                DATA.DataType[i].DataType.SetValue(bytes[i]);
            
            LEN.DataType.SetValue(bytes.Length);
        }

        /// <inheritdoc />
        public override string ToString() => Name;

        /// <inheritdoc />
        public IEnumerator<char> GetEnumerator() => Value.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}