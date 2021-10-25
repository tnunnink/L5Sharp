using System;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp.Abstractions
{
    public abstract class TagBase<TDataType> : TagMemberBase<TDataType>, ITag<TDataType> where TDataType : IDataType
    {
        private string _name;
        private ExternalAccess _externalAccess;
        private TagUsage _usage;

        protected TagBase(string name, TDataType dataType, Dimensions dimensions, Radix radix,
            ExternalAccess externalAccess, string description, IComponent parent, TagUsage usage, bool constant)
            : base(name, dataType, dimensions, radix, externalAccess, description, parent)
        {
            Validate.Name(name);
            
            _name = name;
            _externalAccess = externalAccess ?? ExternalAccess.None;
            _usage = usage != null ? usage : TagUsage.Null;
            Constant = constant;
            
            InstantiateMembers();
        }

        public override string FullName => Name;

        public override string Name => _name;

        public override ExternalAccess ExternalAccess => _externalAccess;
        
        public abstract TagType TagType { get; }

        public Scope Scope => Parent == null ? Scope.Null
            : Parent is IController ? Scope.Controller
            : Parent is IProgram ? Scope.Program
            : throw new InvalidOperationException(
                $"Scope can not be determined by container type '{Parent.GetType()}'");
        
        public TagUsage Usage => _usage;
        
        public bool Constant { get; set; }

        public void SetName(string name)
        {
            SetProperty(ref _name, name, Validate.Name, nameof(Name));
        }

        public ITag<TDataType> SetDimensions(Dimensions dimensions)
        {
            if (dimensions == null)
                throw new ArgumentNullException(nameof(dimensions), "Dimensions can not be null");

            throw new NotImplementedException();
        }

        public void SetExternalAccess(ExternalAccess externalAccess)
        {
            if (externalAccess == null)
                throw new ArgumentNullException(nameof(externalAccess), "External Access can not be null");

            SetProperty(ref _externalAccess, externalAccess, nameof(ExternalAccess));
        }

        public void SetUsage(TagUsage usage)
        {
            if (usage == null)
                throw new ArgumentNullException(nameof(usage), "Usage can not be null");
            
            if (Scope != Scope.Program)
                throw new InvalidOperationException();
            
            SetProperty(ref _usage, usage, nameof(Usage));
        }

        public ITag<IDataType> ChangeTagType(TagType type)
        {
            return type.Create(Name, DataType);
        }
    }
}