using System;
using System.Collections.Generic;
using L5Sharp.Components;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <inheritdoc />
    public sealed class Tag<TDataType> : ITag<TDataType> where TDataType : IDataType
    {
        private readonly TDataType _dataType;
        private readonly ITagMember<TDataType> _tagMember;
        private string _description;

        internal Tag(ComponentName name, TDataType dataType, Dimensions? dimensions = null, Radix? radix = null,
            ExternalAccess? externalAccess = null, string? description = null, TagUsage? usage = null,
            bool constant = false)
        {
            //_dataType = dataType ?? throw new ArgumentNullException(nameof(dataType));
            _description = description ?? string.Empty;
            Usage = usage != null ? usage : TagUsage.Null;
            Scope = Scope.Null;
            Constant = constant;
            Comments = new Comments(Root);
            
           
            externalAccess ??= ExternalAccess.None; //Tags default to 'None' External Access unlike members.
            
            //Instantiate(name, dataType, dimensions, radix, externalAccess, description);
            
            var member = Member.Create(name, dataType, radix, externalAccess, description);

            _tagMember = new TagMember<TDataType>(member, Root, Parent);
        }


        /// <inheritdoc />
        public string Name => _tagMember.Name;

        /// <inheritdoc />
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
        public object? Value => _tagMember.Value;

        /// <inheritdoc />
        public ITagMember<IDataType>? Parent => null;

        /// <inheritdoc />
        public ITag<IDataType> Root => (ITag<IDataType>)(ITag<TDataType>)this;

        /// <inheritdoc />
        public TagType TagType => TagType.Base;

        /// <inheritdoc />
        public Scope Scope { get; }

        /// <inheritdoc />
        public TagUsage Usage { get; }

        /// <inheritdoc />
        public bool Constant { get; set; }
        
        /// <inheritdoc />
        public Comments Comments { get; }

        /// <inheritdoc />
        public void Comment(string comment)
        {
            _description = comment;
        }

        /// <inheritdoc />
        public void SetValue(IAtomicType value) => _tagMember.SetValue(value);

        /// <inheritdoc />
        public bool TrySetValue(IAtomicType value) => _tagMember.TrySetValue(value);

        /// <inheritdoc />
        public ITagMember<TDataType>? this[int index] => _tagMember[index];

        /// <inheritdoc />
        public ITagMember<IDataType>? this[string name] => _tagMember[name];

        /// <inheritdoc />
        public ITagMember<TType> GetMember<TType>(Func<TDataType, IMember<TType>> expression)
            where TType : IDataType => _tagMember.GetMember(expression);

        /// <inheritdoc />
        public IEnumerable<ITagMember<IDataType>> GetMembers() => _tagMember.GetMembers();

        /// <inheritdoc />
        public IEnumerable<string> GetMemberNames() => _tagMember.GetMemberNames();

        /// <inheritdoc />
        public IEnumerable<string> GetDeepMembersNames() => _tagMember.GetDeepMembersNames();

        /*private void Instantiate(ComponentName name, TDataType dataType, Dimensions dimensions, Radix radix,
            ExternalAccess externalAccess, string description)
        {
            var member = Member.Create(name, dataType, dimensions, radix, externalAccess, description);

            _tagMember = new TagMember<TDataType>(member, Root, Parent);
        }*/

        private string DetermineDescription()
        {
            if (!string.IsNullOrEmpty(_description)) return _description;

            if (_dataType is IUserDefined userDefined)
                return userDefined.Description;

            return string.Empty;
        }
    }
}