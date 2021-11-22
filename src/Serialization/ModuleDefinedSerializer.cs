using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;

namespace L5Sharp.Serialization
{
    internal class ModuleDefinedSerializer : IXSerializer<IModuleDefined>
    {
        private readonly LogixContext _context;

        public ModuleDefinedSerializer(LogixContext context)
        {
            _context = context;
        }

        public XElement Serialize(IModuleDefined component)
        {
            throw new System.NotImplementedException();
        }

        public IModuleDefined Deserialize(XElement element)
        {
            if (element == null) return null;

            //The root of a ModuleDefined is a structure element for the type.
            //This is the starting point for creating the type.
            var structure = element.Element("Structure");

            var name = structure.GetName();
            var members = structure?.Elements().Select(e => 
                _context.Serializer.Deserialize<IMember<IDataType>>(e));

            return new ModuleDefined(name, members);
        }
    }
}