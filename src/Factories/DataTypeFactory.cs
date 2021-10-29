using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Extensions;

[assembly: InternalsVisibleTo("L5Sharp.Factories.Tests")]

namespace L5Sharp.Factories
{
    internal class DataTypeFactory : IComponentFactory<IDataType>
    {
        private readonly LogixContext _context;
        private readonly IComponentCache<IDataType> _cache;

        public DataTypeFactory(LogixContext context)
        {
            _context = context;
            _cache = _context.GetCache<IDataType>();
        }

        public IDataType Create(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            var name = element.GetName();

            if (_cache.HasComponent(name))
                return _cache.Get(name);

            var factory = _context.GetFactory<DataTypeMember>();
            var members = element.GetAll<DataTypeMember>().Select(x => factory.Create(x));
            var description = element.GetDescription();

            var type = new DataType(name, members, description);
            
            _cache.Add(type);
            
            return type;
        }
    }
}