using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;
using L5Sharp.Exceptions;

namespace L5Sharp.Core
{
    /// <inheritdoc cref="L5Sharp.IPredefined" />
    public abstract class Predefined : IPredefined, IEquatable<Predefined>
    {
        private readonly Dictionary<string, IMember<IDataType>> _members =
            new Dictionary<string, IMember<IDataType>>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Creates a new instance of a <c>Predefined</c> type with the provided component name./>
        /// </summary>
        /// <param name="name">The value of the <see cref="ComponentName"/>.</param>
        /// <exception cref="ArgumentNullException">Throw when name is null.</exception> 
        protected Predefined(ComponentName name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        /// <inheritdoc />
        public ComponentName Name { get; }

        /// <inheritdoc />
        public virtual string Description => null;

        /// <inheritdoc />
        public Radix Radix => Radix.Null;

        /// <inheritdoc />
        public virtual DataTypeFamily Family => DataTypeFamily.None;

        /// <inheritdoc />
        public DataTypeClass Class => DataTypeClass.Predefined;

        /// <inheritdoc />
        public virtual TagDataFormat DataFormat => TagDataFormat.Decorated;

        /// <inheritdoc />
        public IEnumerable<IMember<IDataType>> Members => _members.Values.AsEnumerable();

        /// <inheritdoc />
        public bool Equals(Predefined other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Members.SequenceEqual(other.Members);
        }

        /// <inheritdoc />
        public IDataType Instantiate()
        {
            return New();
        }

        /// <summary>
        /// Creates new instance of the current type with default values.
        /// </summary>
        /// <remarks>
        ///  The <c>Predefined</c> calls this when <see cref="Instantiate"/> is called. This abstraction is here to let the
        /// base class define the code for instantiating a new version of itself. Simply return <c>new  MyPredefined()</c>.
        /// </remarks>
        /// <returns>A new instance of the current type with default values.</returns>
        protected abstract IDataType New();
        
        /// <inheritdoc />
        public override string ToString()
        {
            return Name;
        }
        
        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Predefined)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return HashCode.Combine(_members, Name, Family);
        }

        /// <summary>
        /// Indicates whether one object is equal to another object of the same type.
        /// </summary>
        /// <param name="left">The left instance of the object.</param>
        /// <param name="right">The right instance of the object.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
        public static bool operator ==(Predefined left, Predefined right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Indicates whether one object is not equal to another object of the same type.
        /// </summary>
        /// <param name="left">The left instance of the object.</param>
        /// <param name="right">The right instance of the object.</param>
        /// <returns>True if the two objects are not equal, otherwise false.</returns>
        public static bool operator !=(Predefined left, Predefined right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Adds all instance fields of type <see cref="IMember{TDataType}"/> to the <see cref="Members"/> collection
        /// using reflection so that the developer does not need to register each member individually.
        /// </summary>
        protected void RegisterMemberFields()
        {
            var fields = GetType().GetFields().Where(f =>
                f.FieldType.IsGenericType &&
                f.FieldType.GetGenericTypeDefinition().IsAssignableFrom(typeof(IMember<>))).ToList();

            foreach (var member in fields.Select(p => (IMember<IDataType>) p.GetValue(this)))
                RegisterTypeMember(member);
        }

        /// <summary>
        /// Registers the <c>Member</c> to the <see cref="Members"/> collection.
        /// </summary>
        /// <param name="member"></param>
        protected void RegisterMember(IMember<IDataType> member) => RegisterTypeMember(member);

        private void RegisterTypeMember(IMember<IDataType> member)
        {
            if (_members.ContainsKey(member.Name))
                throw new ComponentNameCollisionException(member.Name, typeof(IMember<>));

            if (member.DataType.Equals(this))
                throw new CircularReferenceException(
                    $"Member can not be same type as parent type '{member.DataType.Name}'");
            
            _members.Add(member.Name, member);
        }
    }
}