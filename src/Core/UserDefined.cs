using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <inheritdoc cref="L5Sharp.IUserDefined" />
    public sealed class UserDefined : IUserDefined
    {
        /// <summary>
        /// Creates a new <see cref="UserDefined"/> instance with the provided name and optional description and member collection.
        /// </summary>
        /// <param name="name">The <see cref="Core.ComponentName"/> of the type.</param>
        /// <param name="description">The string description of the type. Will default to <see cref="string.Empty"/>.</param>
        /// <param name="members">A enumeration of <see cref="IMember{TDataType}"/> to initialize the type with.</param>
        /// <exception cref="ArgumentNullException">name is null.</exception>
        public UserDefined(ComponentName name, string? description = null,
            IEnumerable<IMember<IDataType>>? members = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;
            Members = new MemberCollection<IMember<IDataType>>(this, members);
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description { get; }

        /// <inheritdoc />
        public DataTypeFamily Family => DataTypeFamily.None;

        /// <inheritdoc />
        public DataTypeClass Class => DataTypeClass.User;

        /// <inheritdoc />
        public IMemberCollection<IMember<IDataType>> Members { get; }

        IEnumerable<IMember<IDataType>> IComplexType.Members => Members;

        /// <inheritdoc />
        public IDataType Instantiate() =>
            new UserDefined(string.Copy(Name), string.Copy(Description), 
                Members.Select(m => new Member<IDataType>(string.Copy(m.Name), m.DataType.Instantiate(),
                    m.Radix, m.ExternalAccess, string.Copy(m.Description))));

        /// <inheritdoc />
        public override string ToString() => Name;
    }
}