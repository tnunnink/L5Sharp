using System;
using System.Globalization;
using System.Reflection;
using Ardalis.SmartEnum;
using L5Sharp.Abstractions;
using L5Sharp.Core;

namespace L5Sharp.Enums
{
    public abstract class TagType : SmartEnum<TagType, string>
    {
        private TagType(string name, string value) : base(name, value)
        {
        }

        public abstract ITag<IDataType> Create(string name, IDataType dataType);
        
        public abstract ITag<TDataType> Create<TDataType>(string name) where TDataType : IDataType, new();

        public static T Create<T>(string name, IDataType dataType) where T : ITag<IDataType>
        {
            return (T)Activator.CreateInstance(typeof(T),
                BindingFlags.CreateInstance |
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.OptionalParamBinding, null, new[] { name, dataType, Type.Missing },
                CultureInfo.CurrentCulture);
        }

        public static readonly TagType Base = new BaseType();
        public static readonly TagType Alias = new AliasType();
        public static readonly TagType Produced = new ProducedType();
        public static readonly TagType Consumed = new ConsumedType();

        private class BaseType : TagType
        {
            public BaseType() : base(nameof(Base), nameof(Base))
            {
            }

            public override ITag<IDataType> Create(string name, IDataType dataType)
            {
                return new Tag(name, dataType);
            }

            public override ITag<TDataType> Create<TDataType>(string name)
            {
                return new Tag<TDataType>(name);
            }
        }

        private class AliasType : TagType
        {
            public AliasType() : base(nameof(Alias), nameof(Alias))
            {
            }

            public override ITag<IDataType> Create(string name, IDataType dataType)
            {
                throw new NotImplementedException();
            }
            
            
            public override ITag<TDataType> Create<TDataType>(string name)
            {
                return new Tag<TDataType>(name);
            }
        }

        private class ProducedType : TagType
        {
            public ProducedType() : base(nameof(Produced), nameof(Produced))
            {
            }

            public override ITag<IDataType> Create(string name, IDataType dataType)
            {
                throw new NotImplementedException();
            }
            
            public override ITag<TDataType> Create<TDataType>(string name)
            {
                return new Tag<TDataType>(name);
            }
        }

        private class ConsumedType : TagType
        {
            public ConsumedType() : base(nameof(Consumed), nameof(Consumed))
            {
            }

            public override ITag<IDataType> Create(string name, IDataType dataType)
            {
                throw new NotImplementedException();
            }

            public override ITag<TDataType> Create<TDataType>(string name)
            {
                return new Tag<TDataType>(name);
            }
        }
    }
}