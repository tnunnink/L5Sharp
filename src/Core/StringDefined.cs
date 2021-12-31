using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using L5Sharp.Abstractions;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Types.Atomic;

namespace L5Sharp.Core
{
    /// <inheritdoc cref="L5Sharp.IStringType" />
    public class StringDefined : ComplexType, IStringType, IEquatable<StringDefined>
    {
        /// <summary>
        /// Creates a new instance of a <c>StringDefined</c> type with the provided arguments.
        /// </summary>
        /// <remarks>
        /// To create an instance of <c>IStringDefined</c> use <see cref="Create"/>.
        /// </remarks>
        /// <param name="name">The name of the type.</param>
        /// <param name="dimensions">The dimensional length of the type.</param>
        /// <param name="description">The description of the type.</param>
        private StringDefined(string name, Dimensions dimensions, string? description = null)
            : base(name, description, GenerateMembers(dimensions))
        {
            LEN = (IMember<Dint>)Members.Single(m => m.Name == nameof(LEN));
            DATA = (IMember<Sint>)Members.Single(m => m.Name == nameof(DATA));
        }


        /// <inheritdoc />
        public override DataTypeFamily Family => DataTypeFamily.String;

        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.User;

        /// <inheritdoc />
        public string Value =>
            Encoding.ASCII.GetString(DATA.Where(d => d.DataType.Value > 0).Select(d => d.DataType.Value).ToArray());

        /// <inheritdoc />
        public IMember<Dint> LEN { get; }

        /// <inheritdoc />
        public IMember<Sint> DATA { get; }

        /// <summary>
        /// Creates a new instance of a <c>IStringType</c> type with the provided arguments.
        /// </summary>
        /// <param name="name">The name of the type.</param>
        /// <param name="dimensions">The dimensional length of the type.</param>
        /// <param name="description">The description of the type.</param>
        /// <returns>A new instance of a <c>IStringDefined</c> type.</returns>
        /// <exception cref="ArgumentNullException">When name is null.</exception>
        /// <exception cref="ArgumentException">When dimensions are empty or multidimensional.</exception>
        public static IStringType Create(ComponentName name, Dimensions dimensions, string? description = null)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));
            
            if (dimensions.AreEmpty || dimensions.AreMultiDimensional)
                throw new ArgumentException("Dimension must single dimensional and have length greater that zero");

            return new StringDefined(name, dimensions, description);
        }

        /// <inheritdoc />
        protected override IDataType New() =>
            new StringDefined(string.Copy(Name), DATA.Dimension.Copy(), string.Copy(Description));

        /// <inheritdoc />
        public void SetValue(string value) => SetData(value);

        /// <inheritdoc />
        public override string ToString() => Name;

        /// <inheritdoc />
        public bool Equals(StringDefined? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Value == other.Value;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((StringDefined)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), Value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(StringDefined? left, StringDefined? right) => Equals(left, right);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(StringDefined? left, StringDefined? right) => !Equals(left, right);

        /// <summary>
        /// Helper to create the default member instance using the provided dimensions. 
        /// </summary>
        /// <param name="dimensions">The dimensional length to set the members with.</param>
        /// <returns>Collection of members to initialize the type with.</returns>
        private static IEnumerable<IMember<IDataType>> GenerateMembers(Dimensions dimensions)
        {
            return new List<IMember<IDataType>>
            {
                Member.Create(nameof(LEN), new Dint(dimensions.Length)),
                Member.Create<Sint>(nameof(DATA), dimensions, Radix.Ascii),
            };
        }

        /// <summary>
        /// Helper to set the underlying DATA member with the provided string value.
        /// </summary>
        /// <param name="value">The string value to parse and set DATA with.</param>
        /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the length of the provided value is longer than the predefined length.</exception>
        private void SetData(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var bytes = Encoding.ASCII.GetBytes(value);

            if (bytes.Length > LEN.DataType.Value)
                throw new ArgumentOutOfRangeException(nameof(value),
                    $"Value length '{bytes.Length}' must be less than the LEN value of '{LEN.DataType.Value}'");
            
            foreach (var dataElement in DATA)
                dataElement.DataType.SetValue(0);

            for (var i = 0; i < bytes.Length; i++)
                DATA[i].DataType.SetValue(bytes[i]);
        }
    }
}