using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;

[assembly: InternalsVisibleTo("L5Sharp.Factories.Tests")]

namespace L5Sharp.Factories
{
    internal class UserDefinedFactory : IComponentFactory<IUserDefined>
    {
        private readonly LogixContext _context;

        public UserDefinedFactory(LogixContext context)
        {
            _context = context;
        }

        public IUserDefined Create(XElement element)
        {
            if (element == null) return null;

            var name = element.GetName();
            var description = element.GetDescription();

            var factory = _context.GetFactory<IMember<IDataType>>();
            var members = element.Descendants(LogixNames.Member).Select(x => factory.Create(x));

            return new DataType(name, description, members);
        }
    }
}