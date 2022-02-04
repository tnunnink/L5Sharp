using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Extensions;
using L5Sharp.Helpers;
using L5Sharp.Serialization;

namespace L5Sharp
{
    internal class LogixModuleIndex
    {
        private readonly LogixContext _context;
        private readonly Dictionary<string, XElement> _index;

        internal LogixModuleIndex(LogixContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _index = new Dictionary<string, XElement>();

            RegisterModules(_context.L5X.Root);
        }

        public IModule? GetModule(string name)
        {
            var serializer = new ModuleSerializer(_context);
            return _index.ContainsKey(name) ? serializer.Deserialize(_index[name]) : null;
        }

        private void RegisterModules(XContainer document)
        {
            var modules = document.Descendants(LogixNames.Module).ToList();

            foreach (var module in modules)
            {
                _index.TryAdd(module.GetComponentName(), module);
            }
        }
    }
}