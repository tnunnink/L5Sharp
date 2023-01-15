using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp.Types
{
    public sealed class ArrayType : ILogixType, IEnumerable<ILogixType>, ILogixSerializable
    {
        private readonly Dictionary<string, ILogixType> _elements;

        /// <summary>
        /// Creates a new <see cref="ArrayType"/> collection using the provided array of <see cref="ILogixType"/> objects.
        /// </summary>
        /// <param name="elements">The array of element to initialize the array type with.</param>
        /// <exception cref="ArgumentNullException"><c>elements</c> is null.</exception>
        /// <exception cref="ArgumentException"><c>elements</c> is empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Any <c>elements</c> dimensional length is greater than <see cref="ushort.MaxValue"/>.</exception>
        public ArrayType(ILogixType[] elements)
        {
           ValidateArray(elements);

           var x = (ushort)elements.Length;

            Dimensions = new Dimensions(x);

            _elements = Dimensions.Indices()
                .Zip(elements, (index, type) => new { index, type })
                .ToDictionary(i => i.index, i => i.type);
        }

        /// <summary>
        /// Creates a new <see cref="ArrayType"/> collection using the provided array of <see cref="ILogixType"/> objects.
        /// </summary>
        /// <param name="elements">The array of element to initialize the array type with.</param>
        /// <exception cref="ArgumentNullException"><c>elements</c> is null.</exception>
        /// <exception cref="ArgumentException"><c>elements</c> is empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Any <c>elements</c> dimensional length is greater than <see cref="ushort.MaxValue"/>.</exception>
        public ArrayType(ILogixType[,] elements)
        {
            ValidateArray(elements);

            var x = (ushort)elements.GetLength(0);
            var y = (ushort)elements.GetLength(1);

            Dimensions = new Dimensions(x, y);

            _elements = Dimensions.Indices()
                .Zip(elements.Cast<ILogixType>(), (index, type) => new { index, type })
                .ToDictionary(i => i.index, i => i.type);
        }

        /// <summary>
        /// Creates a new <see cref="ArrayType"/> collection using the provided array of <see cref="ILogixType"/> objects.
        /// </summary>
        /// <param name="elements">The array of element to initialize the array type with.</param>
        /// <exception cref="ArgumentNullException"><c>elements</c> is null.</exception>
        /// <exception cref="ArgumentException"><c>elements</c> is empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Any <c>elements</c> dimensional length is greater than <see cref="ushort.MaxValue"/>.</exception>
        public ArrayType(ILogixType[,,] elements)
        {
            ValidateArray(elements);

            var x = (ushort)elements.GetLength(0);
            var y = (ushort)elements.GetLength(1);
            var z = (ushort)elements.GetLength(2);

            Dimensions = new Dimensions(x, y, z);

            _elements = Dimensions.Indices()
                .Zip(elements.Cast<ILogixType>(), (index, type) => new { index, type })
                .ToDictionary(i => i.index, i => i.type);
        }

        /// <inheritdoc />
        public string Name => _elements.First().Value.Name;

        /// <inheritdoc />
        public DataTypeFamily Family => _elements.First().Value.Family;

        /// <inheritdoc />
        public DataTypeClass Class => _elements.First().Value.Class;

        /// <summary>
        /// The dimensions value of the <see cref="ArrayType"/>, indicating the length of the array.
        /// </summary>
        public Dimensions Dimensions { get; }

        /// <summary>
        /// Gets the <see cref="ILogixType"/> instance at the specified index.
        /// </summary>
        /// <param name="x">The index of the array element</param>
        public ILogixType this[ushort x] => _elements[$"[{x}]"];


        /// <summary>
        /// Gets the <see cref="ILogixType"/> instance at the specified index.
        /// </summary>
        /// <param name="x">The x index of the array element</param>
        /// <param name="y">The y index of the array element</param>
        public ILogixType this[ushort x, ushort y] => _elements[$"[{x},{y}]"];

        /// <summary>
        /// Gets the <see cref="ILogixType"/> instance at the specified index.
        /// </summary>
        /// <param name="x">The x index of the array element</param>
        /// <param name="y">The y index of the array element</param>
        /// <param name="z">The z index of the array element</param>
        public ILogixType this[ushort x, ushort y, ushort z] => _elements[$"[{x},{y},{z}]"];

        /// <summary>
        /// Gets a collection of <see cref="Member"/> 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<Member> Members() => _elements.Select(e => new Member(e.Key, e.Value));

        /// <inheritdoc />
        public XElement Serialize()
        {
            var element = new XElement(L5XName.Array);
            element.Add(new XAttribute(L5XName.DataType, Name));
            element.Add(new XAttribute(L5XName.Dimensions, Dimensions));

            return element;
        }
        
        /// <inheritdoc />
        public override string ToString() => Name;

        /// <inheritdoc />
        public IEnumerator<ILogixType> GetEnumerator() => _elements.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private static void ValidateArray(Array elements)
        {
            if (elements is null)
                throw new ArgumentNullException(nameof(elements));

            if (elements.Length == 0)
                throw new ArgumentException("Array must have at least one element to be constructed.");

            for (var i = 0; i < elements.Rank; i++)
            {
                var length = elements.GetLength(i);
                if (length > ushort.MaxValue)
                    throw new ArgumentOutOfRangeException(
                        $"Array length of {elements.Length} is out of range. Length must not be greater than  {ushort.MaxValue}");
            }
        }
    }
}