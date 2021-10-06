using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enumerations;
using L5Sharp.Utilities;

namespace L5Sharp.Extensions
{
    public static class L5XAttributeExtensions
    {
        /// <summary>
        /// Gets the attribute "Name" from the current element
        /// </summary>
        /// <param name="element">The current element instance</param>
        /// <returns>The string value of the Name attribute if it exists. Null if it is not found</returns>
        public static string GetName(this XElement element)
        {
            return element.Attribute(L5XNames.Attributes.Name)?.Value;
        }

        /// <summary>
        /// Simple helper extension that gets the attribute "DataType" from the current element
        /// </summary>
        /// <param name="element">The current element instance</param>
        /// <returns>The string value of the DataType attribute if it exists. Null if it is not found</returns>
        public static string GetDataTypeName(this XElement element)
        {
            return element.Attribute(L5XNames.Attributes.DataType)?.Value;
        }

        /// <summary>
        /// Simple helper extension that gets the attribute "Dimensions" from the current element
        /// </summary>
        /// <param name="element">The current element instance</param>
        /// <returns>The string value of the Dimensions attribute if it exists. Null if it is not found</returns>
        public static Dimensions GetDimensions(this XElement element)
        {
            var dimensions = element.Attribute(L5XNames.Attributes.Dimensions)?.Value;
            return dimensions != null ? Dimensions.Parse(dimensions) : null;
        }

        /// <summary>
        /// Simple helper extension that gets the attribute "Dimension" from the current element
        /// </summary>
        /// <param name="element">The current element instance</param>
        /// <returns>The string value of the Dimension attribute if it exists. Null if it is not found</returns>
        public static ushort GetDimension(this XElement element)
        {
            var dimension = element.Attribute(L5XNames.Attributes.Dimension)?.Value;
            return dimension != null ? Convert.ToUInt16(dimension) : (ushort)0;
        }

        /// <summary>
        /// Simple helper extension that gets the attribute "Radix" from the current element
        /// </summary>
        /// <param name="element">The current element instance</param>
        /// <returns>The string value of the Radix attribute if it exists. Null if it is not found</returns>
        public static Radix GetRadix(this XElement element)
        {
            var radix = element.Attribute(L5XNames.Attributes.Radix)?.Value;
            return radix != null ? Radix.FromName(radix) : null;
        }

        /// <summary>
        /// Simple helper extension that gets the attribute "ExternalAccess" from the current element
        /// </summary>
        /// <param name="element">The current element instance</param>
        /// <returns>The string value of the ExternalAccess attribute if it exists. Null if it is not found</returns>
        public static ExternalAccess GetExternalAccess(this XElement element)
        {
            var externalAccess = element.Attribute(L5XNames.Attributes.ExternalAccess)?.Value;
            return externalAccess != null ? ExternalAccess.FromName(externalAccess) : null;
        }

        /// <summary>
        /// Simple helper extension that gets the attribute "ExternalAccess" from the current element
        /// </summary>
        /// <param name="element">The current element instance</param>
        /// <returns>The string value of the ExternalAccess attribute if it exists. Null if it is not found</returns>
        public static string GetValue(this XElement element)
        {
            return element.Attribute(L5XNames.Attributes.Value)?.Value;
        }

        /// <summary>
        /// Simple helper extension that gets the element "Description" from the current element.
        /// </summary>
        /// <param name="element">The current element instance</param>
        /// <returns>The string value of the Description element if it exists. Null if it is not found</returns>
        public static string GetDescription(this XElement element)
        {
            return element.Element(L5XNames.Attributes.Description)?.Value;
        }

        /// <summary>
        /// Simple helper extension that gets the attribute "Family" from the current element
        /// </summary>
        /// <param name="element">The current element instance</param>
        /// <returns>The string value of the Family attribute if it exists. Null if it is not found</returns>
        public static DataTypeFamily GetFamily(this XElement element)
        {
            var family = element.Attribute(L5XNames.Attributes.Family)?.Value;
            return family != null ? DataTypeFamily.FromName(family) : null;
        }

        /// <summary>
        /// Simple helper extension that gets the element "Hidden" from the current element.
        /// </summary>
        /// <param name="element">The current element instance</param>
        /// <returns>The string value of the Hidden element if it exists. Null if it is not found</returns>
        public static bool GetHidden(this XElement element)
        {
            var hidden = element.Attribute(L5XNames.Attributes.Hidden)?.Value;
            return hidden != null && Convert.ToBoolean(hidden);
        }

        /// <summary>
        /// Simple helper extension that gets the element "Target" from the current element.
        /// </summary>
        /// <param name="element">The current element instance</param>
        /// <returns>The string value of the Target element if it exists. Null if it is not found</returns>
        public static string GetTarget(this XElement element)
        {
            return element.Attribute(L5XNames.Attributes.Target)?.Value;
        }

        /// <summary>
        /// Simple helper extension that gets the element "BitNumber" from the current element.
        /// </summary>
        /// <param name="element">The current element instance</param>
        /// <returns>The string value of the BitNumber element if it exists. Null if it is not found</returns>
        public static ushort GetBitNumber(this XElement element)
        {
            var bitNumber = element.Attribute(L5XNames.Attributes.BitNumber)?.Value;
            return Convert.ToUInt16(bitNumber);
        }

        /// <summary>
        /// Simple helper extension that gets the attribute "TagType" from the current element
        /// </summary>
        /// <param name="element">The current element instance</param>
        /// <returns>The string value of the TagType attribute if it exists. Null if it is not found</returns>
        public static TagType GetTagType(this XElement element)
        {
            var tagType = element.Attribute(L5XNames.Attributes.TagType)?.Value;
            return tagType != null ? TagType.FromName(tagType) : null;
        }

        /// <summary>
        /// Simple helper extension that gets the attribute "Usage" from the current element
        /// </summary>
        /// <param name="element">The current element instance</param>
        /// <returns>The string value of the Usage attribute if it exists. Null if it is not found</returns>
        public static TagUsage GetUsage(this XElement element)
        {
            var usage = element.Attribute(L5XNames.Attributes.Usage)?.Value;
            return usage != null ? TagUsage.FromName(usage) : null;
        }

        /// <summary>
        /// Simple helper extension that gets the attribute "Constant" from the current element
        /// </summary>
        /// <param name="element">The current element instance</param>
        /// <returns>The string value of the Constant attribute if it exists. Null if it is not found</returns>
        public static bool GetConstant(this XElement element)
        {
            var constant = element.Attribute(L5XNames.Attributes.Constant)?.Value;
            return constant != null && Convert.ToBoolean(constant);
        }

        /// <summary>
        /// Simple helper extension that gets the attribute "AliasFor" from the current element
        /// </summary>
        /// <param name="element">The current element instance</param>
        /// <returns>The string value of the AliasFor attribute if it exists. Null if it is not found</returns>
        public static string GetAliasFor(this XElement element)
        {
            return element.Attribute(L5XNames.Attributes.AliasFor)?.Value;
        }

        /// <summary>
        /// Simple helper extension that gets the attribute "Scope" from the current element
        /// </summary>
        /// <param name="element">The current element instance</param>
        /// <returns>The string value of the Scope attribute if it exists. Null if it is not found</returns>
        public static Scope GetScope(this XElement element)
        {
            var program = element.Ancestors(L5XNames.Components.Program).FirstOrDefault();
            return program == null ? Scope.Controller : Scope.Program;
        }

        /// <summary>
        /// Simple helper extension that gets the attribute "Scope" from the current element
        /// </summary>
        /// <param name="element">The current element instance</param>
        /// <returns>The string value of the Scope attribute if it exists. Null if it is not found</returns>
        public static TaskType GetTaskType(this XElement element)
        {
            var type = element.Attribute(L5XNames.Attributes.Type)?.Value;
            return type != null ? TaskType.FromName(type) : null;
        }
    }
}