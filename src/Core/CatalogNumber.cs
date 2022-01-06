using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    public class CatalogNumber
    {
        private const string CatalogPattern = @"^([\d]{4})-([A-Z]+)(\d+)([A-Z]*)$";

        private readonly string _catalogNumber;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="catalogNumber"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public CatalogNumber(string catalogNumber)
        {
            if (string.IsNullOrEmpty(catalogNumber))
                throw new ArgumentNullException(nameof(catalogNumber));

            _catalogNumber = catalogNumber;

            AnalyzeNumber(catalogNumber);
        }

        /// <summary>
        /// Attempt to determine values based on conventions...
        /// </summary>
        /// <param name="catalogNumber"></param>
        private void AnalyzeNumber(string catalogNumber)
        {
            var match = Regex.Match(catalogNumber, CatalogPattern, RegexOptions.Compiled);

            if (!match.Success)
            {
                Channels = 0;
                return;
            };

            Bulletin = new Bulletin(match.Groups[0].Value);
            //todo figure out module type
            Channels = short.TryParse(match.Groups[2].Value, out var channels) ? channels : default;
            FeatureCode = match.Groups[3].Value ?? string.Empty;
        }

        /// <summary>
        /// Gets the <see cref="Bulletin"/> value of the <see cref="CatalogNumber"/>.
        /// </summary>
        public Bulletin? Bulletin { get; set; }

        public IEnumerable<ModuleCategory> Categories { get; }

        public short Channels { get; set; }

        public string? FeatureCode { get; set; }


        public static implicit operator string(CatalogNumber value) => value._catalogNumber;
        
        public static implicit operator CatalogNumber(string value) => new(value);
    }
}