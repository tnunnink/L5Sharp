using System;
using System.Text.RegularExpressions;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    public class CatalogNumber
    {
        private const string CatalogPattern = @"^([\d]{4})-([A-Z]+)(\d+)([A-Z]*)$";

        public CatalogNumber(string catalogNumber)
        {
            if (string.IsNullOrEmpty(catalogNumber))
                throw new ArgumentNullException(nameof(catalogNumber));

            var match = Regex.Match(catalogNumber, CatalogPattern, RegexOptions.Compiled);

            if (!match.Success || match.Groups.Count != 4)
                throw new FormatException(
                    $"The provided catalog number '{catalogNumber}' does not have a valid format.");

            Bulletin = new Bulletin(match.Groups[0].Value);
            //todo figure out module type
            Channels = short.TryParse(match.Groups[2].Value, out var channels) ? channels : default;
            FeatureCode = match.Groups[3].Value ?? string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bulletin"></param>
        /// <param name="moduleType"></param>
        /// <param name="channels"></param>
        /// <param name="featureCode"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public CatalogNumber(Bulletin bulletin, ModuleType moduleType, short channels, string? featureCode = null)
        {
            Bulletin = bulletin ?? throw new ArgumentNullException(nameof(bulletin));
            ModuleType = moduleType ?? throw new ArgumentNullException(nameof(moduleType));
            Channels = channels;
            FeatureCode = featureCode ?? string.Empty;
        }

        /// <summary>
        /// Gets the value of the <see cref="CatalogNumber"/>.
        /// </summary>
        public string Number => $"{Bulletin}-{ModuleType}{Channels}{FeatureCode}";

        /// <summary>
        /// Gets the <see cref="Bulletin"/> value of the <see cref="CatalogNumber"/>.
        /// </summary>
        public Bulletin Bulletin { get; }

        public ModuleType ModuleType { get; }

        public short Channels { get; }

        public string FeatureCode { get; }

        
        public static implicit operator string(CatalogNumber value) => value.Number;
        
        public static implicit operator CatalogNumber(string value) => new(value);
    }
}