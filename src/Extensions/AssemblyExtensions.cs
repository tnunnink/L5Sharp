using System;
using System.IO;
using System.Reflection;

namespace L5Sharp.Extensions
{
    public static class AssemblyExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="fileName"></param>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static Stream? GetStream(this Assembly assembly, string fileName, string? nameSpace = null)
        {
            if (assembly is null)
                throw new ArgumentNullException(nameof(assembly));

            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("Filename can not be null or empty");
            
            var assemblyName = assembly.FullName[..assembly.FullName.IndexOf(',')];

            var name = string.IsNullOrEmpty(nameSpace)
                ? $"{assemblyName}.{fileName}"
                : $"{assemblyName}.{nameSpace}.{fileName}";

            return assembly.GetManifestResourceStream(name);
        }
    }
}