using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using L5Sharp.Attributes;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Serialization;
using L5Sharp.Serialization.Data;

namespace L5Sharp.Types
{
    /// <summary>
    /// 
    /// </summary>
    [LogixSerializer(typeof(StructureSerializer))]
    public class StructureType : ILogixType
    {
        private List<Member>? _members;

        /// <summary>
        /// Creates a new <see cref="StructureType"/> instance.
        /// </summary>
        /// <param name="name">The name of the type.</param>
        /// <exception cref="ArgumentNullException">name is null.</exception>
        protected StructureType(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        /// <summary>
        /// Creates a new <see cref="StructureType"/> instance.
        /// </summary>
        /// <param name="name">The name of the type.</param>
        /// <param name="members">The collection of <see cref="Member"/> that make up the type.</param>
        /// <exception cref="ArgumentNullException"><c>name</c> or <c>members</c> is null.</exception>
        public StructureType(string name, IEnumerable<Member> members)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            _members = members is not null ? members.ToList() : throw new ArgumentNullException(nameof(members));
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public virtual DataTypeFamily Family => DataTypeFamily.None;

        /// <inheritdoc />
        public virtual DataTypeClass Class => DataTypeClass.Unknown;

        /// <summary>
        /// The collection of <see cref="Member"/> objects that compose the structure of the logix type.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="Member"/> objects.</returns>
        public IEnumerable<Member> Members => _members ??= FindMembers().ToList();

        /// <inheritdoc />
        public override string ToString() => Name;

        private IEnumerable<Member> FindMembers()
        {
            var members = new List<Member>();

            members.AddRange(GetLogixTypeProperties());

            return members;
        }

        private IEnumerable<Member> GetLogixTypeProperties()
        {
            var properties = GetType().GetProperties()
                .Where(p => typeof(ILogixType).IsAssignableFrom(p.PropertyType) 
                            || (p.PropertyType.IsArray && 
                                typeof(ILogixType).IsAssignableFrom(p.PropertyType.GetElementType())))
                .ToList();
            
            return properties.Select(GenerateMember);
        }

        private Member GenerateMember(PropertyInfo info)
        {
            var name = info.Name;

            if (!info.PropertyType.IsArray)
                return new Member(name, (ILogixType)info.GetValue(this));
            
            var array = new ArrayType<ILogixType>((ILogixType[])info.GetValue(this));
            return new Member(name, array);
        }
    }
}