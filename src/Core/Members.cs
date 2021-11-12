using System;
using System.Collections.Generic;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;

namespace L5Sharp.Core
{
    public class Members : ComponentCollection<IMember<IDataType>>, IMembers
    {
        private readonly IUserDefined _dataType;

        public Members(IUserDefined userDefined)
        {
            _dataType = userDefined ?? throw new ArgumentNullException(nameof(userDefined));
        }

        public Members(IUserDefined userDefined, IEnumerable<IMember<IDataType>> members) : this(userDefined)
        {
            AddRange(members);
        }

        public override void Add(IMember<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), "Member can not be null");

            if (component.DataType != null && component.DataType.Equals(_dataType))
                throw new CircularReferenceException(
                    $"Member can not be same type as parent type '{component.DataType.Name}'");

            base.Add(component);
        }

        public override void Update(IMember<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), "Member can not be null");

            if (component.DataType.Equals(_dataType))
                throw new CircularReferenceException(
                    $"Member can not be same type as parent type '{component.DataType.Name}'");

            base.Update(component);
        }

        public void UpdateDataType(string name, IDataType dataType)
        {
            if (dataType.Equals(_dataType))
                throw new CircularReferenceException(
                    $"Member can not be same type as parent type '{dataType.Name}'");

            var current = Get(name);
            if (current == null)
                throw new InvalidOperationException($"Could not find member with name '{name}'");

            var member = Member.Create(current.Name, dataType, current.Dimensions, current.Radix,
                current.ExternalAccess,
                current.Description);

            Update(member);
        }

        public void UpdateDimensions(string name, Dimensions dimensions)
        {
            var current = Get(name);
            if (current == null)
                throw new InvalidOperationException($"Could not find member with name '{name}'");

            var member = Member.Create(current.Name, current.DataType, dimensions, current.Radix,
                current.ExternalAccess,
                current.Description);

            Update(member);
        }

        public void UpdateAccess(string name, ExternalAccess externalAccess)
        {
            var current = Get(name);
            if (current == null)
                throw new InvalidOperationException($"Could not find member with name '{name}'");

            var member = Member.Create(current.Name, current.DataType, current.Dimensions, current.Radix,
                externalAccess,
                current.Description);

            Update(member);
        }
    }
}