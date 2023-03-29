using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using L5Sharp.Enums;
using L5Sharp.Serialization;
using L5Sharp.Serialization.Data;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Types
{
    /// <summary>
    /// A fundamental <see cref="ILogixType"/> that represents value type object.
    /// </summary>
    /// <remarks>
    /// Logix atomic types are types that have value (i.e. BOOL, SINT, INT, DINT, REAL, etc.).
    /// These type are synonymous with value types in .NET.
    /// </remarks>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer>
    [LogixSerializer(typeof(DataValueSerializer))]
    public abstract class AtomicType : ILogixType
    {
        /// <summary>
        /// Creates a new <see cref="AtomicType"/> instance with the provided name.
        /// </summary>
        /// <param name="name">The name of the atomic type.</param>
        /// <param name="radix"></param>
        /// <exception cref="ArgumentNullException">name is null.</exception>
        protected internal AtomicType(string name, Radix? radix = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));

            if (radix is not null && !radix.SupportsType(this))
                throw new ArgumentException($"The provided radix {radix} is not supported by {GetType()}");

            Radix = radix ?? Radix.Default(this);
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public DataTypeFamily Family => DataTypeFamily.None;

        /// <inheritdoc />
        public DataTypeClass Class => DataTypeClass.Atomic;

        /// <inheritdoc />
        public virtual IEnumerable<Member> Members => GetBitMembers();

        /// <summary>
        /// The radix format for the <see cref="AtomicType"/>.
        /// </summary>
        /// <value>A <see cref="Enums.Radix"/> representing the format of the atomic type value.</value>
        public Radix Radix { get; }

        /// <summary>
        /// Gets the bit member of the current atomic value at the specified index.
        /// </summary>
        /// <param name="index">The <see cref="int"/> bit number representing the member to get.</param>
        public Member this[int index] => Members.SingleOrDefault(m => m.Name == index.ToString())
                                         ?? throw new IndexOutOfRangeException(
                                             $"The specified index {index} is out of ranges for the type {GetType()}");

        /// <summary>
        /// Return the atomic value formatted using the current <see cref="Radix"/> format.
        /// </summary>
        /// <returns>A <see cref="string"/> representing the formatted atomic value.</returns>
        public override string ToString() => Radix.Format(this);

        /// <summary>
        /// Returns the atomic value formatted using the provided <see cref="Enums.Radix"/> option.
        /// </summary>
        /// <param name="radix">The radix format.</param>
        /// <returns>A <see cref="string"/> representing the formatted atomic value.</returns>
        public string ToString(Radix radix) => radix.Format(this);

        /// <summary>
        /// Returns a collection of bit members based on the implementing type.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Member> GetBitMembers()
        {
            var chars = GetBinary().ToCharArray();
            
            var members = new List<Member>();

            for (var i = chars.Length - 1; i >= 0; i--)
            {
                var name = (chars.Length - 1 - i).ToString();
                var value = new BOOL(chars[i] == '1');
                members.Add(new Member(name, value));
            }

            return members;
        }

        /// <summary>
        /// Converts the current <see cref="AtomicType"/> to a binary string.
        /// </summary>
        /// <returns>A <see cref="string"/> value representing the value in the specified base.</returns>
        private string GetBinary()
        {
            const int baseNumber = 2;
            const int bitsPerByte = 8;

            var bytes = GetBytes(this);

            var builder = new StringBuilder();

            for (var ctr = bytes.GetUpperBound(0); ctr >= bytes.GetLowerBound(0); ctr--)
            {
                var byteString = Convert.ToString(bytes[ctr], baseNumber).PadLeft(bitsPerByte, '0');
                builder.Append(byteString);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Converts the current value type to an array of bytes.
        /// </summary>
        /// <param name="atomicType">The current value type to convert.</param>
        /// <returns>An array of bytes representing the value.</returns>
        private static byte[] GetBytes(AtomicType atomicType)
        {
            return atomicType switch
            {
                BOOL value => BitConverter.GetBytes((bool)value),
                SINT value => new[] { byte.Parse(((sbyte)value).ToString("X2"), NumberStyles.HexNumber) },
                USINT value => new[] { (byte)value },
                INT value => BitConverter.GetBytes(value),
                UINT value => BitConverter.GetBytes(value),
                DINT value => BitConverter.GetBytes(value),
                UDINT value => BitConverter.GetBytes(value),
                LINT value => BitConverter.GetBytes(value),
                ULINT value => BitConverter.GetBytes(value),
                _ => throw new NotSupportedException(
                    $"The provided atomic type {atomicType.GetType()} is not supported.")
            };
        }
    }
}