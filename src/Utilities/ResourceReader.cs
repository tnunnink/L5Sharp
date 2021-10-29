using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace L5Sharp.Utilities
{
    public class ResourceReader
    {
        private readonly Assembly _assembly;

        public ResourceReader(Type type)
        {
            _assembly = type.Assembly;

            if (_assembly == null)
                throw new InvalidOperationException("Assembly Type Not Initialized");
        }

        public List<string> Names => _assembly.GetManifestResourceNames().ToList();
        private string AssemblyName => _assembly.FullName.Substring(0, _assembly.FullName.IndexOf(','));

        public Stream GetStream(string fileName, string nameSpace)
        {
            return _assembly.GetManifestResourceStream($"{AssemblyName}.{nameSpace}.{fileName}");
        }
    }
}