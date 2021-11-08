using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Extensions;

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

        public TDataType GetValue()
        {
            return _dataType;
        }

        public void SetValue(IDataType value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (!(value is TDataType type))
                throw new InvalidTagValueException(value, typeof(TDataType));
                
            _dataType = type;
        }

        public IEnumerable<string> GetMembersNames()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITagMember<IDataType>> GetMembers()
        {
            throw new NotImplementedException();
        }

        public ITagMember<IDataType> GetMember(string name)
        {
            var member = _dataType.GetMember(name);
            return new TagMember<IDataType>(member, this);
        }

        public ITagMember<TType> GetMember<TType>(Func<TDataType, IMember<TType>> expression) 
            where TType : IDataType
        {
            var member = expression.Invoke(_dataType);

            if (!member.DataType.GetType().IsAssignableFrom(typeof(TType)))
                throw new InvalidOperationException();

            return new TagMember<TType>(member, this);
        }

        public void SetMember<TAtomic>(Func<TDataType, IMember<TAtomic>> expression, TAtomic value) where TAtomic : IAtomic
        {
            throw new NotImplementedException();
        }

        public void SetMember<TAtomic>(Func<TDataType, IMember<TAtomic>> expression, Radix radix) where TAtomic : IAtomic
        {
            throw new NotImplementedException();
        }

        public void SetMember<TAtomic>(Func<TDataType, IMember<TAtomic>> expression, string description) where TAtomic : IAtomic
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

        /*private void Instantiate()
        {
            var members = GenerateMembers(this);

            _members.Clear();

            foreach (var member in members)
                _members.Add(member.Name, member);
        }

        private static IEnumerable<TagMember<IDataType>> GenerateMembers(IMember<TDataType> member)
        {
            if (member.DataType is IAtomic && member.Dimensions.AreEmpty)
                return Array.Empty<TagMember<IDataType>>();

            if (!member.Dimensions.AreEmpty)
                return member.Dimensions.GenerateIndices().Select(i => new TagMember<IDataType>(
                    new Member<IDataType>(i, member.DataType, Dimensions.Empty,
                        member.Radix, member.ExternalAccess, member.Description),
                    member));

            return member.DataType.Members.Select(m => new TagMember<IDataType>(m, member));
        }*/
    }

    public static class Tag
    {
        public static ITag<IDataType> New(string name, IDataType dataType, Dimensions dimensions = null,
            Radix radix = null, ExternalAccess externalAccess = null, string description = null, TagUsage usage = null,
            bool constant = false, ILogixComponent parent = null)
        {
            return new Tag<IDataType>(name, dataType, dimensions, radix, externalAccess, description, usage, constant,
                parent);
        }

        public static ITag<TDataType> OfType<TDataType>(string name, Dimensions dimensions = null, Radix radix = null,
            ExternalAccess externalAccess = null, string description = null, TagUsage usage = null,
            bool constant = false, ILogixComponent parent = null)
            where TDataType : IDataType, new()
        {
            var dataType = new TDataType();
            return new Tag<TDataType>(name, dataType, dimensions, radix, externalAccess, description, usage, constant,
                parent);
        }
        
        public static ITag<IAtomic> OfAtomic<TAtomic>(string name, Dimensions dimensions = null, Radix radix = null,
            ExternalAccess externalAccess = null, string description = null, TagUsage usage = null,
            bool constant = false, ILogixComponent parent = null)
            where TAtomic : IAtomic, new()
        {
            var dataType = new TAtomic();
            return new Tag<IAtomic>(name, dataType, dimensions, radix, externalAccess, description, usage, constant,
                parent);
        }
        
        public static ITag<IPredefined> OfPredefined<TPredefined>(string name, Dimensions dimensions = null, Radix radix = null,
            ExternalAccess externalAccess = null, string description = null, TagUsage usage = null,
            bool constant = false, ILogixComponent parent = null)
            where TPredefined : IPredefined, new()
        {
            var dataType = new TPredefined();
            return new Tag<IPredefined>(name, dataType, dimensions, radix, externalAccess, description, usage, constant,
                parent);
        }
    }
}