using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using L5Sharp.Abstractions;
using L5Sharp.Exceptions;

[assembly: InternalsVisibleTo("L5Sharp.Core.Tests")]

namespace L5Sharp.Core
{
    /// <summary>
    /// A <c>ComponentCollection</c> of <see cref="IMember{TDataType}"/> that represents the members of a <see cref="IUserDefined"/>
    /// </summary>
    internal sealed class DataTypeMembers : ComponentCollection<IMember<IDataType>>
    {
        private readonly IUserDefined _dataType;

        /// <summary>
        /// Creates a new instance with the provided user defined data type parent.
        /// </summary>
        /// <param name="userDefined">
        /// The instance of the <see cref="IUserDefined"/> that represents the parent of the <c>DataTypeMembers</c>.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when the provided user defined instance is null.</exception>
        public DataTypeMembers(IUserDefined userDefined)
        {
            _dataType = userDefined ?? throw new ArgumentNullException(nameof(userDefined));
        }

        /// <summary>
        /// Creates a new instance with the provided user defined data type parent and initial collection of members.
        /// </summary>
        /// <param name="userDefined">
        /// The instance of <see cref="IUserDefined"/> that represents the parent of the <c>DataTypeMembers</c>.
        /// </param>
        /// <param name="members">
        /// The collection of members to initialize <c>DataTypeMembers</c> with.
        /// </param>
        public DataTypeMembers(IUserDefined userDefined, IEnumerable<IMember<IDataType>> members) : this(userDefined)
        {
            AddRange(members);
        }

        /// <inheritdoc />
        /// <remarks>
        /// Will check the provided member type to ensure it is not the same as the parent type.
        /// This is not allowed in Logix as it causes a circular reference.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when the component is null.</exception>
        /// <exception cref="CircularReferenceException">Thrown when the member data type is equal to the parent type.</exception>
        public override void Add(IMember<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), "Member can not be null");

            if (component.DataType != null && component.DataType.Equals(_dataType))
                throw new CircularReferenceException(
                    $"Member can not be same type as parent type '{component.DataType.Name}'");

            base.Add(component);
        }

        /// <inheritdoc />
        public override void Update(IMember<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), "Member can not be null");

            if (component.DataType != null && component.DataType.Equals(_dataType))
                throw new CircularReferenceException(
                    $"Member can not be same type as parent type '{component.DataType.Name}'");

            base.Update(component);
        }
    }
}