using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Rockwell;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;

namespace L5Sharp
{
    /// <summary>
    /// A global static class containing methods for creating types, components, and other usefull helpers.
    /// </summary>
    public static class Logix
    {
        private static readonly Dictionary<string, Func<string, Radix?, AtomicType>> Factories = new()
        {
            { nameof(BOOL), (s, r) => new BOOL(s, r) },
            { "BIT", (s, r) => new BOOL(s, r) },
            { nameof(SINT), (s, r) => new SINT(s, r) },
            { nameof(INT), (s, r) => new INT(s, r) },
            { nameof(DINT), (s, r) => new DINT(s, r) },
            { nameof(LINT), (s, r) => new LINT(s, r) },
            { nameof(USINT), (s, r) => new USINT(s, r) },
            { nameof(UINT), (s, r) => new UINT(s, r) },
            { nameof(UDINT), (s, r) => new UDINT(s, r) },
            { nameof(ULINT), (s, r) => new ULINT(s, r) },
            { nameof(REAL), (s, r) => new REAL(s, r) }
        };

        /// <summary>
        /// Returns the singleton null <see cref="ILogixType"/> object.
        /// </summary>
        public static ILogixType Null => NullType.Instance;

        /// <summary>
        /// Creates a <see cref="AtomicType"/> by parsing the input text value. 
        /// </summary>
        /// <param name="input">The string input value to parse.</param>
        /// <param name="radix">The optional radix format of the input string. If not provided, this method will
        /// call <see cref="Enums.Radix.Infer"/> to attempt to determine the format.</param>
        /// <returns>A converted <see cref="AtomicType"/> representing the value of the input string.</returns>
        /// <exception cref="ArgumentException"><c>name</c> is not a known/existing atomic type name.</exception>
        public static AtomicType Atomic(string input, Radix? radix = null)
        {
            radix ??= Radix.Infer(input);
            return radix.Parse(input);
        }

        /// <summary>
        /// Creates a <see cref="AtomicType"/> value specified by the provided type name and input text value. 
        /// </summary>
        /// <param name="name">The name of the atomic type to parse.</param>
        /// <param name="input">The string input value to parse as set for the atomic type.</param>
        /// <param name="radix">The optional radix format of the input string. If not provided, will
        /// call <see cref="Enums.Radix.Infer"/> to attempt to determine the format.</param>
        /// <returns>A converted <see cref="AtomicType"/> representing the value of the input string.</returns>
        /// <exception cref="ArgumentException"><c>name</c> is not a known/existing atomic type name.</exception>
        public static AtomicType Atomic(string name, string input, Radix? radix = null)
        {
            if (!Factories.TryGetValue(name, out var factory))
                throw new ArgumentException();

            return factory.Invoke(input, radix);
        }

        /// <summary>
        /// Creates a <see cref="AtomicType"/> value specified by the provided type argument and input text value. 
        /// </summary>
        /// <param name="input">The string input value to parse.</param>
        /// <param name="radix">The optional radix format of the input string. If not provided, this method will
        /// call <see cref="Enums.Radix.Infer"/> to attempt to determine the format.</param>
        /// <typeparam name="TAtomic">The <see cref="AtomicType"/> to convert the parsed value to.</typeparam>
        /// <returns>A <see cref="AtomicType"/> value converted to the specified generic type parameter.</returns>
        public static TAtomic Atomic<TAtomic>(string input, Radix? radix = null) where TAtomic : AtomicType
        {
            if (!Factories.TryGetValue(typeof(TAtomic).Name, out var factory))
                throw new ArgumentException();

            return (TAtomic)factory.Invoke(input, radix);
        }

        /// <summary>
        /// Determines whether the specified type name is an <see cref="AtomicType"/> logix type.
        /// </summary>
        /// <param name="typeName">The name of the type.</param>
        /// <returns><c>true</c> if <c>typeName</c> represents the name of an atomic type; otherwise, <c>false</c>.</returns>
        public static bool IsAtomic(string typeName) => Factories.ContainsKey(typeName);

        /// <summary>
        /// Created a new <see cref="ArrayType{TDataType}"/> of the specified type with the length of the provided dimensions.
        /// </summary>
        /// <param name="dimensions">The dimensions of the array to create.</param>
        /// <typeparam name="TDataType">The logix type for which to create.
        /// Must have a default parameterless constructor in order to generate instances.</typeparam>
        /// <returns>A <see cref="ArrayType{TDataType}"/> of the specified dimensions containing new objects of the specified type.</returns>
        public static ArrayType<TDataType> Array<TDataType>(Dimensions dimensions) where TDataType : ILogixType, new()
        {
            if (dimensions is null)
                throw new ArgumentNullException(nameof(dimensions));
            
            var elements = Enumerable.Range(0, dimensions.Length).Select(_ => new TDataType());

            return new ArrayType<TDataType>(dimensions, elements.ToArray());
        }

        /// <summary>
        /// Creates a new <see cref="Member"/> with the provided name and specified type parameter.
        /// </summary>
        /// <param name="name">The name of the tag.</param>
        /// <typeparam name="TLogixType">The logix data type of the tag. Type must have parameterless constructor to create.</typeparam>
        /// <returns>A new <see cref="Tag"/> object with specified parameters.</returns>
        public static Member Member<TLogixType>(string name) where TLogixType : ILogixType, new() =>
            new(name, new TLogixType());

        /// <summary>
        /// Creates a new <see cref="Components.Tag"/> with the provided name and specified type parameter.
        /// </summary>
        /// <param name="name">The name of the tag.</param>
        /// <typeparam name="TLogixType">The logix data type of the tag. Type must have parameterless constructor to create.</typeparam>
        /// <returns>A new <see cref="Tag"/> object with specified parameters.</returns>
        public static Tag Tag<TLogixType>(string name) where TLogixType : ILogixType, new() =>
            new() { Name = name, Data = new TLogixType() };

        /// <summary>
        /// Creates a new <see cref="Components.Tag"/> with the provided name and specified type parameter as an array of
        /// the provided dimensions.
        /// </summary>
        /// <param name="name">The name of the tag.</param>
        /// <param name="dimensions">The dimensions of the tag data array.</param>
        /// <typeparam name="TLogixType">The logix data type of the tag. Type must have parameterless constructor to create.</typeparam>
        /// <returns>A new <see cref="Tag"/> object with specified parameters.</returns>
        public static Tag TagArray<TLogixType>(string name, Dimensions dimensions)
            where TLogixType : ILogixType, new() =>
            new() { Name = name, Data = Array<TLogixType>(dimensions) };

        /// <summary>
        /// Creates a new <see cref="Task"/> component with the provide properties.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <param name="type">The <see cref="Enums.TaskType"/> specifying the type of task.</param>
        /// <param name="description">The description of the task.</param>
        /// <returns>A <see cref="LogixTask"/> component with the provided property values.</returns>
        public static LogixTask Task(string? name = null, TaskType? type = null, string? description = null) => new()
        {
            Name = name ?? string.Empty, 
            Type = type ?? TaskType.Periodic,
            Description = description ?? string.Empty
        };

        /// <summary>
        /// Creates a new <see cref="Components.Module"/> with the provided name and catalog number.
        /// </summary>
        /// <param name="name">The name of the module</param>
        /// <param name="catalogNumber">The catalog number to lookup a catalog entry for.</param>
        /// <param name="description">The option description of the module.</param>
        /// <returns>A new <see cref="Components.Module"/> object initialized with data return by the catalog service.</returns>
        /// <exception cref="InvalidOperationException">The module catalog service could not load the installed catalog
        /// database file -or- catalog number does not exist in the catalog database.</exception>
        /// <exception cref="ArgumentException"><c>catalogNumber</c> is null or empty.</exception>
        /// <remarks>This factory method uses the <see cref="ModuleCatalog"/> service to lookup info for the specified
        /// catalog number. If RSLogix is not installed on the current environment, this will throw an exception.</remarks>
        public static Module Module(string name, string catalogNumber, string? description = null)
        {
            var catalog = new ModuleCatalog();
            var entry = catalog.Lookup(catalogNumber);
            
            return new Module
            {
                Name = name,
                CatalogNumber = entry.CatalogNumber,
                Revision = entry.Revisions.Max(),
                Vendor = entry.Vendor,
                ProductType = entry.ProductType,
                ProductCode = entry.ProductCode,
                Ports = entry.Ports.Select(p => new Port
                {
                    Id = p.Number,
                    Type = p.Type,
                    Address = p.Type =="Ethernet" ? Address.DefaultIP() : Address.DefaultSlot(),
                    Upstream = !p.DownstreamOnly
                }).ToList(),
                Description = description ?? string.Empty
            };
        }
    }
}