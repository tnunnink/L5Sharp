using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Extensions;

[assembly: InternalsVisibleTo("L5Sharp.Serialization.Tests")]

namespace L5Sharp.Serialization
{
    internal class DataTypeMaterializer : IComponentMaterializer<IDataType>
    {
        private readonly LogixContext _context;

        public DataTypeMaterializer(LogixContext context)
        {
            _context = context;
        }
        
        public IDataType Materialize(XElement element)
        {
            if (element == null) throw new ArgumentNullException(nameof(element));

            var members = element.GetAll<IMember>().Select(x => x.Deserialize<IMember>(_context));

            return new DataType(element.GetName(), members, element.GetDescription());
        }
    }
}