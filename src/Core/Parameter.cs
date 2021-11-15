using System;
using L5Sharp.Abstractions;
using L5Sharp.Builders;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace L5Sharp.Core
{
    public class Parameter<TDataType> : LogixComponent, IParameter<TDataType> where TDataType : IDataType
    {
        private bool _visible;
        private bool _required;

        internal Parameter(string name, TDataType dataType, TagUsage usage = null, bool required = false,
            bool visible = false, Dimensions dimensions = null, Radix radix = null,
            ExternalAccess externalAccess = null,
            string description = null, bool constant = false)
            : base(name, description)
        {
            DataType = dataType ?? throw new ArgumentNullException(nameof(dataType));

            usage ??= DataType is IAtomic ? TagUsage.Input : TagUsage.InOut;
            SetUsage(usage);

            dimensions ??= Dimensions.Empty;
            SetDimensions(dimensions);

            if (DataType is IAtomic atomic)
                if (radix != null)
                    atomic.SetRadix(radix);

            externalAccess = Usage == TagUsage.InOut ? ExternalAccess.Null
                : externalAccess != null ? externalAccess
                : Usage == TagUsage.Input ? ExternalAccess.ReadWrite : ExternalAccess.ReadOnly;
            SetExternalAccess(externalAccess);

            _required = required;
            _visible = visible;
            Constant = constant;
        }

        public TDataType DataType { get; }
        public Dimensions Dimensions { get; private set; }
        public Radix Radix => DataType.Radix;
        public ExternalAccess ExternalAccess { get; private set; }
        public IElement<TDataType>[] Elements { get; private set; }
        public TagType TagType => Alias == null ? TagType.Base : TagType.Alias;
        public TagUsage Usage { get; private set; }

        public bool Required
        {
            get => Usage == TagUsage.InOut || _required;
            set => _required = value;
        }

        public bool Visible
        {
            get => Required || _visible;
            set => _visible = value;
        }

        public ITag<TDataType> Alias => null; //todo still need to decide how to handle aliases
        public IAtomic Default => DataType is IAtomic atomic ? atomic : null;
        public bool Constant { get; set; }

        public void SetName(string name)
        {
            throw new NotImplementedException();
        }

        public void SetUsage(TagUsage usage)
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

        public void SetDimensions(Dimensions dimensions)
        {
            if (dimensions == null) throw new ArgumentNullException(nameof(dimensions));

            if (!dimensions.AreEmpty && Usage != TagUsage.InOut)
                throw new InvalidOperationException("Dimensions are only configurable for InOut parameters");

            Dimensions = dimensions;
        }

        public void SetRadix(Radix radix)
        {
            if (radix == null)
                throw new ArgumentNullException(nameof(radix));

            if (!(DataType is IAtomic atomic))
                throw new InvalidOperationException("Radix can only be set on atomic members");

            atomic.SetRadix(radix);

            if (Elements.Length == 0) return;

            foreach (var element in Elements)
                if (element.DataType is IAtomic atomicType)
                    atomicType.SetRadix(radix);
        }

        public void SetExternalAccess(ExternalAccess access)
        {
            if (Usage == TagUsage.InOut && access != ExternalAccess.Null)
                throw new InvalidOperationException("External Access is only configurable for Input/Output parameters");

            if (access == null) throw new ArgumentNullException(nameof(access));

            ExternalAccess = access;
        }

        public void SetDefault(IAtomic value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (!(DataType is IAtomic atomic)) return;

            atomic.SetValue(value);
        }
    }

    public static class Parameter
    {
        public static IParameter<IDataType> Create(string name, IDataType dataType)
        {
            return new Parameter<IDataType>(name, dataType);
        }

        public static IParameter<TDataType> Create<TDataType>(string name)
            where TDataType : IDataType, new()
        {
            return new Parameter<TDataType>(name, new TDataType());
        }

        public static IParameter<TDataType> Create<TDataType>(string name, TDataType dataType)
            where TDataType : IDataType, new()
        {
            return new Parameter<TDataType>(name, dataType);
        }

        public static IParameterBuilder<IDataType> Build(string name, IDataType dataType)
        {
            return new ParameterBuilder<IDataType>(name, dataType);
        }

        public static IParameterBuilder<TDataType> Build<TDataType>(string name)
            where TDataType : IDataType, new()
        {
            var dataType = new TDataType();
            return new ParameterBuilder<TDataType>(name, dataType);
        }

        public static IParameterBuilder<TDataType> Build<TDataType>(string name, TDataType dataType)
            where TDataType : IDataType, new()
        {
            return new ParameterBuilder<TDataType>(name, dataType);
        }
    }
}