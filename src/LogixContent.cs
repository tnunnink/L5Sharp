﻿using System;
using System.Xml.Linq;

namespace L5Sharp
{
    
    public class LogixContent
    {
        private readonly XDocument _l5X;

        /// <summary>
        /// Creates a new <see cref="LogixContent"/> instance with the provided <see cref="LogixInfo"/>.
        /// </summary>
        /// <param name="l5X">The <see cref="LogixInfo"/> instance that represents the loaded L5X.</param>
        private LogixContent(XDocument l5X)
        {
            _l5X = l5X ?? throw new ArgumentNullException(nameof(l5X));

            Info = new LogixInfo(_l5X.Root);
        }

        public LogixInfo Info { get; }

        /// <summary>
        /// Creates a new <see cref="LogixContent"/> by loading the contents of the provide file name.
        /// </summary>
        /// <param name="fileName">The full path, including file name, to the L5X file to load.</param>
        /// <returns>A new <see cref="LogixContent"/> containing the contents of the specified file.</returns>
        /// <exception cref="ArgumentException">The string is null or empty.</exception>
        /// <remarks>
        /// This factory method uses the <see cref="XDocument.Load(string)"/> to load the contents of the xml file into
        /// memory. This means that this method is subject to the same exceptions that could be generated by loading the
        /// XDocument. Once loaded, validation is performed to ensure the content adheres to the specified L5X Schema files.
        /// </remarks>
        public static LogixContent Load(string fileName) => new(XDocument.Load(fileName));

        /// <summary>
        /// Creates a new <see cref="LogixContent"/> with the provided L5X string content.
        /// </summary>
        /// <param name="text">The string that contains the L5X content to parse.</param>
        /// <returns>A new <see cref="LogixContent"/> containing the contents of the specified string.</returns>
        /// <exception cref="ArgumentException">The string is null or empty.</exception>
        /// <remarks>
        /// This factory method uses the <see cref="XDocument.Parse(string)"/> to load the contents of the xml file into
        /// memory. This means that this method is subject to the same exceptions that could be generated by parsing the
        /// XDocument. Once parsed, validation is performed to ensure the content adheres to the specified L5X Schema files.
        /// </remarks>
        public static LogixContent Parse(string text) => new(XDocument.Parse(text));


        public void Save(string fileName) => _l5X.Save(fileName);

        /// <inheritdoc />
        public override string ToString() => _l5X.ToString();
    }
}