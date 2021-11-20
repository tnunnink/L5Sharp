using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;

[assembly: InternalsVisibleTo("L5Sharp.Serialization.Tests")]

namespace L5Sharp.Serialization
{
    internal class UserDefinedMaterializer : IComponentMaterializer<IUserDefined>
    {
        private readonly LogixContext _context;

        public UserDefinedMaterializer(LogixContext context)
        {
            _context = context;
        }

        public IUserDefined Materialize(XElement element)
        {
            if (element == null) return null;

            var name = element.GetValue<IUserDefined>(x => x.Name);
            var description = element.GetDescription();

            var factory = _context.GetFactory<IMember<IDataType>>();
            var members = element.Descendants(LogixNames.Member).Select(x => factory.Materialize(x));

            return new DataType(name, description, members);
        }
    }
}