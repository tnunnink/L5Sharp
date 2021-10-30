using System;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp.Abstractions
{
    public abstract class TagBase : TagMemberBase, ITag
    {
        private string _name;
        private ExternalAccess _externalAccess;
        private TagUsage _usage;

        protected TagBase(string name, IDataType dataType, Dimensions dimensions, Radix radix,
            ExternalAccess externalAccess, string description, TagUsage usage, bool constant, ILogixComponent parent)
            : base(name, dataType, dimensions, radix, externalAccess, description, parent)
        {
            Validate.Name(name);
            
            _name = name;
            _externalAccess = externalAccess ?? ExternalAccess.None;
            _usage = usage != null ? usage : TagUsage.Null;
            Constant = constant;
            
            InstantiateMembers();
        }

        public override string FullName => _name;

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
            Validate.Name(name);
            SetProperty(ref _name, name, nameof(Name));
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
                Throw.NotConfigurableException(nameof(Usage), nameof(Tag),
                    "The tag must be a program tag to set usage");
            
            SetProperty(ref _usage, usage, nameof(Usage));
        }

        public ITag ChangeDataType(IDataType dataType)
        {
            if (dataType == null)
                throw new ArgumentNullException(nameof(dataType), "Dimensions can not be null");

            return new Tag(Name, dataType, Dimensions, Radix, ExternalAccess, Description, Usage, Constant, Parent);
        }

        public ITag ChangeDimensions(Dimensions dimensions)
        {
            if (dimensions == null)
                throw new ArgumentNullException(nameof(dimensions), "Dimensions can not be null");

            return new Tag(Name, DataType, dimensions, Radix, ExternalAccess, Description, Usage, Constant, Parent);
        }

        public ITag ChangeTagType(TagType type)
        {
            return type.Create(Name, DataType);
        }
    }
}