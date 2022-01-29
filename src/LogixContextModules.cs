using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Extensions;
using L5Sharp.Helpers;
using L5Sharp.Serialization;

namespace L5Sharp
{
    internal class LogixContextModules
    {
        private readonly LogixContext _context;
        private readonly Dictionary<string, IEnumerable<XElement>> _index;

        internal LogixContextModules(LogixContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _index = new Dictionary<string, IEnumerable<XElement>>();

            RegisterModules(_context.L5X.Root);
        }

        /// <summary>
        /// Gets all child modules of the current parent module name and port number
        /// </summary>
        /// <param name="moduleName">The name of the parent module to get children for.</param>
        /// <param name="portId">The port id of the module for which to get the children for.</param>
        /// <returns></returns>
        public IEnumerable<IModule> GetChildren(string moduleName, int portId)
        {
            return _index.TryGetValue(moduleName, out var modules)
                ? DeserializeModules(modules.Where(x =>
                {
                    var value = x.Attribute("ParentModPortId")?.Value;
                    return int.TryParse(value, out var id) && id == portId;
                }))
                : Enumerable.Empty<IModule>();
        }

        private IEnumerable<IModule> DeserializeModules(IEnumerable<XElement> elements)
        {
            var serializer = new ModuleSerializer(_context);
            return elements.Select(e => serializer.Deserialize(e));
        }

        private void RegisterModules(XContainer document)
        {
            var modules = document.Descendants(LogixNames.Module).ToList();

            foreach (var module in modules)
            {
                var moduleName = module.GetComponentName();
                var children = modules.Where(x => x.Attribute("ParentModule")?.Value == moduleName).ToList();
                _index.TryAdd(moduleName, children);
            }
        }
    }
}