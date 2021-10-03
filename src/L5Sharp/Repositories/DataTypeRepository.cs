using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Utilities;

[assembly: InternalsVisibleTo("L5Sharp.Repositories.Tests")]

namespace L5Sharp.Repositories
{
    internal class DataTypeRepository : IDataTypeRepository
    {
        private readonly XElement _container;

        public DataTypeRepository(Logix logix)
        {
            _container = logix.Content.Container<DataType>();
        }

        public IEnumerable<DataType> All()
        {
            return _container.All<DataType>();
        }

        public DataType Get(string name)
        {
            return _container.Get<DataType>(name);
        }

        public void Add(DataType component)
        {
            if (_container.Contains<DataType>(component.Name))
                Throw.NameCollisionException(component.Name, typeof(DataType));

            var element = component.Serialize();
            var dependents = component.GetDependentUserTypes().Select(t => t.Serialize());

            _container.Add(element);
            _container.Add(dependents);
        }

        public void Remove(DataType component)
        {
            throw new System.NotImplementedException();
        }

        public void Update(DataType component)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<DataType> WithMemberType(IDataType dataType)
        {
            return _container.Descendants(L5XNames.Components.Member)
                .Where(x => x.GetDataTypeName() == dataType.Name)
                .Select(x => x.Parent.Deserialize<DataType>());
        }
    }
}