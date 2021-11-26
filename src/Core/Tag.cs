using System;
using System.Collections.Generic;
using L5Sharp.Enums;
using L5Sharp.Exceptions;

namespace L5Sharp.Core
{
    /// <inheritdoc />
    public sealed class Tag<TDataType> : ITag<TDataType> where TDataType : IDataType
    {
        private readonly TDataType _dataType;
        private TagMember<TDataType> _tagMember;
        private string _description;

        internal Tag(ComponentName name, TDataType dataType, Dimensions dimensions = null, Radix radix = null,
            ExternalAccess externalAccess = null, string description = null, TagUsage usage = null,
            bool constant = false)
        {
            //Initialize tag level state.
            _dataType = dataType ?? throw new ArgumentNullException(nameof(dataType));
            _description = description;
            Usage = usage != null ? usage : TagUsage.Null;
            Scope = Scope.Null;
            Constant = constant;
            Comments = new Comments(this as ITag<IDataType>);
            
            //Initialize tag member root.
            externalAccess ??= ExternalAccess.None; //Tags default to 'None' External Access unlike members.
            Instantiate(name, dataType, dimensions, radix, externalAccess, description);
        }
        
        /// <inheritdoc cref="ITag{TDataType}.Name" />
        public ComponentName Name => _tagMember.Name;

        string ITagMember<TDataType>.Name => Name;

        /// <inheritdoc cref="ITag{TDataType}.Name" />
        public string Description => DetermineDescription();

        /// <inheritdoc />
        public string TagName => _tagMember.TagName;

        /// <inheritdoc />
        public string Operand => _tagMember.Name;

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
        public Scope Scope { get; }

        /// <inheritdoc />
        public TagUsage Usage { get; private set; }

        /// <inheritdoc />
        public bool Constant { get; set; }

        /// <inheritdoc />
        public IAtomic GetData() => _tagMember.GetData();

        /// <inheritdoc />
        public void SetData(IAtomic value) => _tagMember.SetData(value);

        /// <inheritdoc />
        public Comments Comments { get; }

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
        public void SetDescription(string description)
        {
            _description = description;
        }

        /// <inheritdoc />
        public ITagMember<IDataType> Parent => null;

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
        public void SetMember<TType>(Func<TDataType, IMember<TType>> expression, string description)
            where TType : IDataType => _tagMember.SetMember(expression, description);

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
            var member = Member.Create(name, dataType, dimensions, radix, externalAccess, description);

            _tagMember = new TagMember<TDataType>(member, Parent, (ITag<IDataType>)(ITag<TDataType>)this);
        }

        private string DetermineDescription()
        {
            if (!string.IsNullOrEmpty(_description)) return _description;

            if (_dataType is IUserDefined userDefined)
                return userDefined.Description;

            return null;
        }
    }
}