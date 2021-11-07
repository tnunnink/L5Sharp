using System;
using L5Sharp.Abstractions;
using L5Sharp.Configurations;
using L5Sharp.Exceptions;

namespace L5Sharp.Core
{
    public class DataTypeMembers : ComponentCollection<IDataTypeMember<IDataType>>, IDataTypeMembers
    {
        private readonly IDataType _parentType;

        public DataTypeMembers(IDataType parentType)
        {
            _parentType = parentType;
        }
        
        public override void Add(IDataTypeMember<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), "Member can not be null");
            
            if (component.DataType.Equals(_parentType))
                throw new CircularReferenceException(
                    $"Member can not be same type as parent type '{component.DataType.Name}'");

            base.Add(component);
        }

        public void Add(string name, Action<IDataTypeMemberConfiguration> config = null)
        {
            var configuration = new DataTypeMemberConfiguration(name);
            config?.Invoke(configuration);
            Add(configuration);
        }
    }
}