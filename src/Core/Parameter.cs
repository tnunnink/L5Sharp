using L5Sharp.Abstractions;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    public class Parameter<TDataType> : LogixComponent, IParameter<TDataType> where TDataType : IDataType
    {
        public Parameter(string name, TDataType dataType, Dimensions dimensions = null, TagUsage usage = null,
            bool required = false, bool visible = false, Radix radix = null, ExternalAccess externalAccess = null,
            string description = null, IAtomic min = null, IAtomic max = null, IAtomic @default = null,
            TagType tagType = null, bool constant = false) : base(name, description)
        {
            DataType = dataType;
            Dimensions = dimensions;
            Radix = radix;
            ExternalAccess = externalAccess;
            TagType = tagType;
            Usage = usage;
            Required = required;
            Visible = visible;
            Min = min;
            Max = max;
            Default = @default;
            Constant = constant;
        }

        public TDataType DataType { get; }
        public Dimensions Dimensions { get; }
        public Radix Radix { get; }
        public ExternalAccess ExternalAccess { get; }
        public IMember<TDataType>[] Elements { get; }
        public TagType TagType { get; }
        public TagUsage Usage { get; }
        public bool Required { get; }
        public bool Visible { get; }
        public IAtomic Min { get; }
        public IAtomic Max { get; }
        public IAtomic Default { get; }
        public bool Constant { get; }

        public void SetUsage(TagUsage usage)
        {
            throw new System.NotImplementedException();
        }

        public void IsRequired()
        {
            throw new System.NotImplementedException();
        }

        public void IsVisible()
        {
            throw new System.NotImplementedException();
        }

        public void SetDimensions(Dimensions dimensions)
        {
            throw new System.NotImplementedException();
        }

        public void SetMin(IAtomic value)
        {
            throw new System.NotImplementedException();
        }

        public void SetMax(IAtomic value)
        {
            throw new System.NotImplementedException();
        }

        public void SetDefault(IAtomic value)
        {
            throw new System.NotImplementedException();
        }

        public void SetRadix(Radix radix)
        {
            throw new System.NotImplementedException();
        }

        public void SetExternalAccess(ExternalAccess access)
        {
            throw new System.NotImplementedException();
        }

        public void IsConstant()
        {
            throw new System.NotImplementedException();
        }
    }
}