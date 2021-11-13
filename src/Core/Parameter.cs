using System;
using System.Collections.Generic;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Extensions;

namespace L5Sharp.Core
{
    public class Parameter<TDataType> : LogixComponent, IParameter<TDataType> where TDataType : IDataType
    {
        internal Parameter(string name, TDataType dataType, TagUsage usage = null, bool required = false,
            bool visible = false, Dimensions dimensions = null, Radix radix = null, ExternalAccess externalAccess = null,
            string description = null, IAtomic @default = null, bool constant = false)
            : base(name, description)
        {
            DataType = dataType;

            Dimensions = dimensions ?? Dimensions.Empty;
            if (Dimensions.AreMultiDimensional)
                throw new ArgumentException("Member can only have single dimensional arrays");

            if (DataType is IAtomic atomic)
            {
                if (radix != null)
                    atomic.SetRadix(radix);

                if (@default != null)
                    atomic.SetValue(@default);
            }

            ExternalAccess = externalAccess ?? ExternalAccess.ReadWrite;
            Usage = usage ?? TagUsage.Input;
            Required = required;
            Visible = visible;
            Constant = constant;

            Elements = InstantiateElements();
        }

        public TDataType DataType { get; }
        public Dimensions Dimensions { get; private set; }
        public Radix Radix => DataType.Radix;
        public ExternalAccess ExternalAccess { get; private set; }
        public IMember<TDataType>[] Elements { get; }
        public TagType TagType => Alias == null ? TagType.Base : TagType.Alias;
        public TagUsage Usage { get; private set; }
        public bool Required { get; set; }
        public bool Visible { get; set; }
        public ITag<TDataType> Alias => null; //todo still need to decide how to handle aliases
        public IAtomic Default => DataType is IAtomic atomic ? atomic : null;
        public bool Constant { get; set; }

        public void SetUsage(TagUsage usage)
        {
            if (usage == null)
                throw new ArgumentNullException(nameof(usage), "Usage can not be null");

            if (usage != TagUsage.Input && usage != TagUsage.Output && usage != TagUsage.InOut)
                throw new ArgumentException(
                    $"Usage '{usage}' not valid for {typeof(IParameter<IDataType>)}. Usage must be Input, Output, or InOut");

            if (DataType.IsValueType() && usage == TagUsage.InOut)
                throw new InvalidOperationException($"Usage '{usage}' is not valid for value types");

            Usage = usage;
        }

        public void SetDimensions(Dimensions dimensions)
        {
            if (dimensions == null)
                throw new ArgumentNullException(nameof(dimensions), "Dimensions can not be null");
            
            
        }

        public void SetRadix(Radix radix)
        {
            if (radix == null)
                throw new ArgumentNullException(nameof(radix));

            if (!(DataType is IAtomic atomic))
                throw new ComponentNotConfigurableException(nameof(Radix), GetType(),
                    "Radix can only be set on atomic members");

            atomic.SetRadix(radix);

            if (Elements.Length == 0) return;

            foreach (var element in Elements)
                if (element.DataType is IAtomic atomicType)
                    atomicType.SetRadix(radix);
        }

        public void SetExternalAccess(ExternalAccess access)
        {
            if (access == null)
                throw new ArgumentNullException(nameof(access), "Access can not be null");

            if (Usage == TagUsage.InOut)
                throw new ComponentNotConfigurableException(nameof(ExternalAccess), typeof(IParameter<IDataType>),
                    "External Access is only configurable for Input/Output parameters");

            ExternalAccess = access;
        }

        public void SetDefault(IAtomic value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "Value can not be null");

            if (!(DataType is IAtomic atomic)) return;

            atomic.SetValue(value);
        }

        private IMember<TDataType>[] InstantiateElements()
        {
            var elements = new List<IMember<TDataType>>(Dimensions);

            for (var i = 0; i < Dimensions; i++)
                elements.Add(Member.Create($"[{i}]", (TDataType)DataType.Instantiate(),
                    Dimensions.Empty, Radix, ExternalAccess, Description));

            return elements.ToArray();
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
    }
}