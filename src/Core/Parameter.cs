using System;
using L5Sharp.Abstractions;
using L5Sharp.Builders;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace L5Sharp.Core
{
    public class Parameter<TDataType> : IParameter<TDataType> where TDataType : IDataType
    {
        internal Parameter(string name, TDataType dataType, TagUsage usage = null, bool required = false,
            bool visible = false, Dimensions dimensions = null, Radix radix = null,
            ExternalAccess externalAccess = null,
            string description = null, bool constant = false)
        {
            Name = name;
            Description = description;
            DataType = dataType;

            usage ??= DataType is IAtomic ? TagUsage.Input : TagUsage.InOut;
            SetUsage(usage);

            dimensions ??= Dimensions.Empty;
            SetDimensions(dimensions);

            if (DataType is IAtomic atomic && radix != null)
                atomic.SetRadix(radix);

            externalAccess = Usage == TagUsage.InOut ? ExternalAccess.Null
                : externalAccess != null ? externalAccess
                : Usage == TagUsage.Input ? ExternalAccess.ReadWrite : ExternalAccess.ReadOnly;
            SetExternalAccess(externalAccess);

            Required = Usage == TagUsage.InOut || required;
            Visible = Required || visible;
            Constant = constant;
        }


        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description { get; }

        /// <inheritdoc />
        public TDataType DataType { get; }

        /// <inheritdoc />
        public Dimensions Dimension { get; private set; }

        /// <inheritdoc />
        public Radix Radix => DataType.Radix;

        /// <inheritdoc />
        public ExternalAccess ExternalAccess { get; private set; }

        /// <inheritdoc />
        public TagType TagType => Alias == null ? TagType.Base : TagType.Alias;

        /// <inheritdoc />
        public TagUsage Usage { get; private set; }

        /// <inheritdoc />
        public bool Required { get; }

        /// <inheritdoc />
        public bool Visible { get; }

        /// <inheritdoc />
        public ITag<TDataType> Alias => null; //todo still need to decide how to handle aliases

        /// <inheritdoc />
        public IAtomic Default => DataType is IAtomic atomic ? atomic : null;

        /// <inheritdoc />
        public bool Constant { get; }

        /// <inheritdoc />
        public IMember<TDataType> Copy()
        {
            return null;
        }
        
        private void SetUsage(TagUsage usage)
        {
            if (usage == null) throw new ArgumentNullException(nameof(usage));

            if (usage != TagUsage.Input && usage != TagUsage.Output && usage != TagUsage.InOut)
                throw new ArgumentException(
                    $"Usage '{usage}' not valid for Parameter. Parameter usage must be Input, Output, or InOut");

            if (!DataType.IsValueType() && usage != TagUsage.InOut)
                throw new InvalidOperationException(
                    $"Usage '{usage}' is not valid for complex types. DataType must be Bool, Sint, Int, Dint, or Real");

            Usage = usage;
        }
        
        private void SetDimensions(Dimensions dimensions)
        {
            if (dimensions == null) throw new ArgumentNullException(nameof(dimensions));

            if (!dimensions.AreEmpty && Usage != TagUsage.InOut)
                throw new InvalidOperationException("Dimensions are only configurable for InOut parameters");

            Dimension = dimensions;
        }
        
        private void SetExternalAccess(ExternalAccess access)
        {
            if (Usage == TagUsage.InOut && access != ExternalAccess.Null)
                throw new InvalidOperationException("External Access is only configurable for Input/Output parameters");

            if (access == null) throw new ArgumentNullException(nameof(access));

            ExternalAccess = access;
        }
    }
}