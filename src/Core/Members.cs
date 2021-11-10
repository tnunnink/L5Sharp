using System;
using System.Collections.Generic;
using L5Sharp.Abstractions;
using L5Sharp.Configurations;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class Members : ComponentCollection<IMember<IDataType>>, IMembers
    {
        private readonly IDataType _parentType;
        
        public Members(IDataType parentType)
        {
            _parentType = parentType;
        }

        public Members(IDataType parentType, IEnumerable<IMember<IDataType>> members) : base(members)
        {
            _parentType = parentType;
        }
        
        public override void Add(IMember<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), "Member can not be null");
            
            Validate.Name(component.Name);

            if (component.DataType != null && component.DataType.Equals(_parentType))
                throw new CircularReferenceException(
                    $"Member can not be same type as parent type '{component.DataType.Name}'");

            base.Add(component);
        }

        public void Add(Action<IMemberNameConfiguration> config)
        {
            var configuration = new MemberConfiguration();
            config?.Invoke(configuration);
            Add(configuration);
        }

        public override void Update(string name, IMember<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), "Member can not be null");
            
            Validate.Name(component.Name);
            
            if (component.DataType.Equals(_parentType))
                throw new CircularReferenceException(
                    $"Member can not be same type as parent type '{component.DataType.Name}'");
            
            base.Update(name, component);
        }

        public void Update(string name, Action<IMemberNameConfiguration> config)
        {
            var configuration = new MemberConfiguration();
            config?.Invoke(configuration);
            Update(name, configuration);
        }

        public void SetDataType(string name, IDataType dataType)
        {
            if (dataType.Equals(_parentType))
                throw new CircularReferenceException(
                    $"Member can not be same type as parent type '{dataType.Name}'");
            
            var current = Get(name);
            if (current == null)
                throw new ArgumentNullException(nameof(current), $"Could not find Member with name '{name}'");

            var member = Member.New(current.Name, dataType, current.Dimensions, current.Radix, current.ExternalAccess,
                current.Description);
            
            Update(name, member);
        }

        public void SetDimensions(string name, Dimensions dimensions)
        {
            var current = Get(name);

            if (current == null)
                throw new ArgumentNullException(nameof(current));

            var member = Member.New(current.Name, current.DataType, dimensions, current.Radix, current.ExternalAccess,
                current.Description);
            
            Update(name, member);
        }

        public void SetAccess(string name, ExternalAccess externalAccess)
        {
            var current = Get(name);

            if (current == null)
                throw new ArgumentNullException(nameof(current));

            var member = Member.New(current.Name, current.DataType, current.Dimensions, current.Radix, externalAccess,
                current.Description);
            
            Update(name, member);
        }
    }
}