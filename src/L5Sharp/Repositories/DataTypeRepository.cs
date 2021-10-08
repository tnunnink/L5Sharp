using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Base;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

[assembly: InternalsVisibleTo("L5Sharp.Repositories.Tests")]

namespace L5Sharp.Repositories
{
    internal class DataTypeRepository : Repository<DataType>, IDataTypeRepository
    {
        public DataTypeRepository(XElement context) : base(context)
        {
        }

        public override void Add(DataType component)
        {
            if (Context.Contains<DataType>(component.Name))
                Throw.ComponentNameCollisionException(component.Name, typeof(DataType));

            var element = component.Serialize();
            var dependents = component.GetDependentUserTypes().Select(t => t.Serialize());

            Context.Add(element);
            Context.Add(dependents);
        }

        public override void Remove(DataType component)
        {
            throw new System.NotImplementedException();
        }

        public override void Update(DataType component)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<DataType> WithMemberType(IDataType dataType)
        {
            return Context.Descendants(L5XNames.Components.Member)
                .Where(x => x.GetDataTypeName() == dataType.Name)
                .Select(x => x.Parent.Deserialize<DataType>());
        }
    }
}