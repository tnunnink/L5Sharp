using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Types
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class StructureType : ILogixType
    {
        /// <summary>
        /// Creates a new <see cref="StructureType"/> with the provided name and member collection.
        /// </summary>
        /// <param name="name">The name of the type.</param>
        /// <param name="description">The description of the type</param>
        /// <exception cref="ArgumentNullException">name is null.</exception>
        protected StructureType(string name, string? description = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; }

        /// <inheritdoc />
        public virtual DataTypeFamily Family => DataTypeFamily.None;

        /// <inheritdoc />
        public abstract DataTypeClass Class { get; }

        /// <summary>
        /// The collection of <see cref="Member"/> objects that compose the structure of the logix type.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="Member"/> objects.</returns>
        public virtual IEnumerable<Member> Members() => FindMembers();

        /// <inheritdoc />
        public override string ToString() => Name;

        private IEnumerable<Member> FindMembers()
        {
            var members = new List<Member>();

            members.AddRange(FindMemberFields());
            members.AddRange(FindMemberProperties());

            return members;
        }
        
        private IEnumerable<Member> FindMemberFields()
        {
            var fields = GetType().GetFields().Where(f => f.FieldType.IsAssignableFrom(typeof(ILogixType)));

            return fields.Select(f => new Member(f.Name, (ILogixType)f.GetValue(this)));
        }
        
        private IEnumerable<Member> FindMemberProperties()
        {
            var properties = GetType().GetProperties().Where(p => p.PropertyType.IsAssignableFrom(typeof(ILogixType)));

            return properties.Select(p => new Member(p.Name, (ILogixType)p.GetValue(this)));
        }
    }
}