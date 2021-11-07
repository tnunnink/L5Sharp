using System;
using System.Collections.Generic;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;

namespace L5Sharp.Core
{
    public sealed class Tag<TDataType> : LogixComponent, ITag<TDataType> where TDataType : IDataType
    {
        private TDataType _dataType;

        public Tag(string name, TDataType dataType, Dimensions dimensions = null, Radix radix = null,
            ExternalAccess externalAccess = null, string description = null, TagUsage usage = null,
            bool constant = false, ILogixComponent parent = null) : base(name, description)
        {
            _dataType = dataType ?? throw new ArgumentNullException(nameof(dataType), "DataType can not be null");
            Dimensions = dimensions ?? Dimensions.Empty;
            Radix = radix != null
                ? radix.IsValidForType(dataType)
                    ? radix
                    : throw new RadixNotSupportedException(radix, dataType)
                : dataType is IAtomic atomic
                    ? Radix.Default(atomic)
                    : Radix.Null;
            ExternalAccess = externalAccess ?? ExternalAccess.None;
            Usage = usage != null ? usage : TagUsage.Null;
            Constant = constant;
            Parent = parent;
        }

        public string FullName => Name;
        public string DataType => _dataType.Name;
        TDataType IMember<TDataType>.DataType => _dataType;
        public Dimensions Dimensions { get; }
        public Radix Radix { get; }
        public ExternalAccess ExternalAccess { get; private set; }

        public TagType TagType => TagType.Base;

        public Scope Scope => Parent == null ? Scope.Null
            : Parent is IController ? Scope.Controller
            : Parent is IProgram ? Scope.Program
            : Scope.Null;

        public TagUsage Usage { get; private set; }
        public bool Constant { get; set; }
        public ILogixComponent Parent { get; }

        public object GetValue()
        {
            return _dataType is IAtomic atomic ? atomic.GetValue() : null;
        }

        public void SetValue(object value)
        {
            if (!(_dataType is IAtomic atomic))
                return;

            atomic.SetValue(value);
        }

        public IEnumerable<ITagMember<IDataType>> GetMembers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetMembersNames()
        {
            throw new NotImplementedException();
        }

        public ITagMember<IDataType> GetMember(Func<TDataType, IDataType> expression)
        {
            throw new NotImplementedException();
        }

        public ITagMember<IDataType> GetMember(Func<TDataType, IMember<IDataType>> expression)
        {
            var member = expression.Invoke(_dataType);
            return new Tag<IDataType>(member.Name, member.DataType, member.Dimensions, member.Radix,
                member.ExternalAccess, member.Description);
        }

        public ITagMember<TType> GetMember<TType>(Func<TDataType, TType> expression) where TType : IDataType
        {
            throw new NotImplementedException();
        }

        public void SetRadix(Radix radix)
        {
            throw new NotImplementedException();
        }

        public void SetExternalAccess(ExternalAccess externalAccess)
        {
            if (externalAccess == null)
                throw new ArgumentNullException(nameof(externalAccess), "External Access can not be null");

            ExternalAccess = externalAccess;
        }

        public void SetUsage(TagUsage usage)
        {
            if (usage == null)
                throw new ArgumentNullException(nameof(usage), "Usage can not be null");

            if (Scope != Scope.Program)
                throw new ComponentNotConfigurableException(nameof(Usage), typeof(Tag<IDataType>),
                    "Tag is not a program scoped tag");

            Usage = usage;
        }

        public ITag<T> ChangeDataType<T>(T dataType) where T : IDataType
        {
            if (dataType == null)
                throw new ArgumentNullException(nameof(dataType), "Dimensions can not be null");

            return new Tag<T>(Name, dataType, Dimensions, Radix, ExternalAccess, Description,
                Usage, Constant, Parent);
        }

        public ITag<TDataType> ChangeDimensions(Dimensions dimensions)
        {
            if (dimensions == null)
                throw new ArgumentNullException(nameof(dimensions), "Dimensions can not be null");

            return new Tag<TDataType>(Name, _dataType, dimensions, Radix, ExternalAccess, Description, Usage, Constant,
                Parent);
        }

        public ITag<TDataType> ChangeTagType(TagType type)
        {
//            return type.Create<TDataType>(Name, _dataType);
            throw new NotImplementedException();
        }
    }

    public static class Tag
    {
        public static Tag<TType> New<TType>(string name, Dimensions dimensions = null, Radix radix = null,
            ExternalAccess externalAccess = null, string description = null, TagUsage usage = null,
            bool constant = false, ILogixComponent parent = null)
            where TType : IDataType, new()
        {
            var dataType = new TType();
            return new Tag<TType>(name, dataType, dimensions, radix, externalAccess, description, usage, constant,
                parent);
        }
    }
}