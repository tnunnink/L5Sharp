using System;
using System.Collections.Generic;
using L5Sharp.Builders;
using L5Sharp.Enums;
using L5Sharp.Exceptions;

namespace L5Sharp.Core
{
    /// <inheritdoc />
    public sealed class Tag<TDataType> : ITag<TDataType> where TDataType : IDataType
    {
        private readonly TDataType _dataType;
        private TagMember<TDataType> _tagMember;

        internal Tag(ComponentName name, TDataType dataType, Dimensions dimensions = null, Radix radix = null,
            ExternalAccess externalAccess = null, string description = null, TagUsage usage = null,
            bool constant = false, ILogixComponent container = null)
        {
            _dataType = dataType ?? throw new ArgumentNullException(nameof(dataType));

            externalAccess ??= ExternalAccess.None; //Tags default to None not Read/Write
            Instantiate(name, dataType, dimensions, radix, externalAccess, description);

            Usage = usage != null ? usage : TagUsage.Null;
            Constant = constant;
            Container = container;
        }


        /// <inheritdoc cref="ILogixComponent.Name" />
        public ComponentName Name => _tagMember.Name;

        string ITagMember<TDataType>.Name => Name;

        /// <inheritdoc cref="ILogixComponent.Description" />
        public string Description => _tagMember.Description;

        /// <inheritdoc />
        public string TagName => _tagMember.TagName;

        /// <inheritdoc />
        public string DataType => _tagMember.DataType;

        /// <inheritdoc />
        public Dimensions Dimensions => _tagMember.Dimensions;

        /// <inheritdoc />
        public Radix Radix => _tagMember.Radix;

        /// <inheritdoc />
        public ExternalAccess ExternalAccess => _tagMember.ExternalAccess;

        /// <inheritdoc />
        public TagType TagType => TagType.Base;

        /// <inheritdoc />
        public Scope Scope => Container == null ? Scope.Null
            : Container is IController ? Scope.Controller
            : Container is IProgram ? Scope.Program
            : Scope.Null;

        /// <inheritdoc />
        public TagUsage Usage { get; private set; }

        /// <inheritdoc />
        public bool Constant { get; set; }

        //The Container of a tag should be the controller/program/routine that creates it.
        //Will assign this internally when is added to a collection
        /// <inheritdoc />
        public ILogixComponent Container { get; internal set; }

        /// <inheritdoc />
        public IAtomic GetData() => _tagMember.GetData();

        /// <inheritdoc />
        public void SetData(IAtomic value) => _tagMember.SetData(value);

        /// <inheritdoc />
        public void SetName(ComponentName name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            Instantiate(name, _dataType, Dimensions, Radix, ExternalAccess, Description);
        }

        /// <inheritdoc />
        public void SetDimensions(Dimensions dimensions)
        {
            if (dimensions == null)
                throw new ArgumentNullException(nameof(dimensions));

            Instantiate(Name, _dataType, dimensions, Radix, ExternalAccess, Description);
        }

        /// <inheritdoc />
        public void SetUsage(TagUsage usage)
        {
            if (usage == null)
                throw new ArgumentNullException(nameof(usage));

            if (Scope != Scope.Program)
                throw new ComponentNotConfigurableException(nameof(Usage), typeof(Tag<IDataType>),
                    "Tag is not a program scoped tag");

            Usage = usage;
        }

        /// <inheritdoc />
        public void SetExternalAccess(ExternalAccess externalAccess)
        {
            if (externalAccess == null)
                throw new ArgumentNullException(nameof(externalAccess));

            Instantiate(Name, _dataType, Dimensions, Radix, externalAccess, Description);
        }

        /// <inheritdoc />
        public void SetRadix(Radix radix) => _tagMember.SetRadix(radix);

        /// <inheritdoc />
        public void SetDescription(string description) => _tagMember.SetDescription(description);

        /// <inheritdoc />
        public IEnumerable<string> GetMemberNames() => _tagMember.GetMemberNames();

        /// <inheritdoc />
        public IEnumerable<string> GetDeepMembersNames() => _tagMember.GetDeepMembersNames();

        /// <inheritdoc />
        public ITagMember<IDataType> this[string name] => _tagMember[name];

        /// <inheritdoc />
        public ITagMember<IDataType> this[Func<TDataType, IMember<IDataType>> expression] => _tagMember[expression];

        /// <inheritdoc />
        public ITagMember<TDataType> this[int index] => _tagMember[index];

        /// <inheritdoc />
        public IEnumerable<ITagMember<IDataType>> GetMembers() => _tagMember.GetMembers();

        /// <inheritdoc />
        public IEnumerable<ITagMember<IDataType>> GetMembers(Func<ITagMember<IDataType>, bool> predicate)
            => _tagMember.GetMembers(predicate);

        /// <inheritdoc />
        public ITagMember<TType> GetMember<TType>(Func<TDataType, IMember<TType>> expression)
            where TType : IDataType => _tagMember.GetMember(expression);

        /// <inheritdoc />
        public void SetMember<TAtomic>(Func<TDataType, IMember<TAtomic>> expression, TAtomic value)
            where TAtomic : IAtomic => _tagMember.SetMember(expression, value);

        /// <inheritdoc />
        public void SetMember<TAtomic>(Func<TDataType, IMember<TAtomic>> expression, Radix radix)
            where TAtomic : IAtomic => _tagMember.SetMember(expression, radix);

        /// <inheritdoc />
        public void SetMember<TAtomic>(Func<TDataType, IMember<TAtomic>> expression, string description)
            where TAtomic : IAtomic => _tagMember.SetMember(expression, description);

        /// <inheritdoc />
        public ITag<TType> ChangeDataType<TType>(TType dataType) where TType : IDataType
        {
            if (dataType == null)
                throw new ArgumentNullException(nameof(dataType), "Dimensions can not be null");

            return new Tag<TType>(Name, dataType, Dimensions, Radix, ExternalAccess, Description,
                Usage, Constant);
        }

        private void Instantiate(ComponentName name, TDataType dataType, Dimensions dimensions, Radix radix,
            ExternalAccess externalAccess, string description)
        {
            var member = dimensions != null && !dimensions.AreEmpty
                ? Member.Create(name, dataType, dimensions, radix, externalAccess, description)
                : Member.Create(name, dataType, radix, externalAccess, description);

            _tagMember = new TagMember<TDataType>(member, null, this as Tag<IDataType>);
        }
    }
}