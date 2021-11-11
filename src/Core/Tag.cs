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
        private string _description;

        public Tag(string name, TDataType dataType, Dimensions dimensions, Radix radix,
            ExternalAccess externalAccess, string description, TagUsage usage,
            bool constant, ILogixComponent parent = null) : base(name, description)
        {
            _dataType = dataType ?? throw new ArgumentNullException(nameof(dataType), "DataType can not be null");
            if (_dataType is IAtomic atomic && radix != null)
                atomic.SetRadix(radix);
            
            _description = description;
            
            Dimensions = dimensions ?? Dimensions.Empty;
            ExternalAccess = externalAccess ?? ExternalAccess.None;
            Usage = usage != null ? usage : TagUsage.Null;
            Constant = constant;
            Parent = parent;
            Elements = InstantiateElements();
        }
        
        public override string Description => _description;
        public string FullName => Name;
        public string DataType => _dataType.Name;
        TDataType IMember<TDataType>.DataType => _dataType;
        public Dimensions Dimensions { get; }
        public Radix Radix => _dataType.Radix;
        public ExternalAccess ExternalAccess { get; private set; }
        public IMember<TDataType>[] Elements { get; }
        public TagType TagType => TagType.Base;

        public Scope Scope => Parent == null ? Scope.Null
            : Parent is IController ? Scope.Controller
            : Parent is IProgram ? Scope.Program
            : Scope.Null;

        public TagUsage Usage { get; private set; }
        public bool Constant { get; set; }
        public ILogixComponent Parent { get; }

        
        public TDataType GetData()
        {
            return _dataType;
        }

        public void SetData(IDataType data)
        {
            _dataType.SetData(data);
        }

        public void SetRadix(Radix radix)
        {
            if (radix == null)
                throw new ArgumentNullException(nameof(radix));

            if (!(_dataType is IAtomic atomic))
                throw new ComponentNotConfigurableException(nameof(Radix), GetType(),
                    "Radix can only be set on atomic members");

            atomic.SetRadix(radix);

            if (Elements.Length == 0) return;

            foreach (var element in Elements)
                if (element.DataType is IAtomic atomicType)
                    atomicType.SetRadix(radix);
        }

        public void SetExternalAccess(ExternalAccess externalAccess)
        {
            if (externalAccess == null)
                throw new ArgumentNullException(nameof(externalAccess), "External Access can not be null");

            ExternalAccess = externalAccess;
        }

        public override void SetDescription(string description)
        {
            _description = description;

            SetMemberDescriptions(description);

            if (Elements.Length > 0)
                SetElementDescription(description);
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

        public IEnumerable<string> GetMembersList() => _dataType.GetMembers().Select(m => m.Name);

        public IEnumerable<string> GetDeepMembersList() => _dataType.GetMemberNames();

        public IEnumerable<ITagMember<IDataType>> GetMembers() =>
            _dataType.GetMembers().Select(m => new TagMember<IDataType>(m, this));

        public ITagMember<IDataType> GetMember(string name)
        {
            var member = _dataType.GetMember(name);
            return member != null ? new TagMember<IDataType>(member, this) : null;
        }

        public ITagMember<IDataType> GetElement(ushort index)
        {
            return index < Elements.Length
                ? new TagMember<IDataType>((IMember<IDataType>)Elements[index], this)
                : null;
        }

        public ITagMember<TType> GetMember<TType>(Func<TDataType, IMember<TType>> expression)
            where TType : IDataType
        {
            var member = expression.Invoke(_dataType);

            if (!member.DataType.GetType().IsAssignableFrom(typeof(TType)))
                throw new InvalidOperationException();

            return new TagMember<TType>(member, this);
        }

        public void SetMember<TAtomic>(Func<TDataType, IMember<TAtomic>> expression, TAtomic value)
            where TAtomic : IAtomic
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression), "Expression can not be null");

            var member = expression.Invoke(_dataType);
            member.DataType.SetValue(value);
        }

        public void SetMember<TAtomic>(Func<TDataType, IMember<TAtomic>> expression, Radix radix)
            where TAtomic : IAtomic
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression), "Expression can not be null");

            var member = expression.Invoke(_dataType);
            member.SetRadix(radix);
        }

        public void SetMember<TAtomic>(Func<TDataType, IMember<TAtomic>> expression, string description)
            where TAtomic : IAtomic
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression), "Expression can not be null");

            var member = expression.Invoke(_dataType);
            member.SetDescription(description);
        }

        public void SetElement<TAtomic>(ushort index, TAtomic value) where TAtomic : IAtomic
        {
            if (index >= Elements.Length) return;

            var element = Elements[index];

            if (element.DataType is IAtomic atomic)
                atomic.SetValue(value);
        }

        public void SetElement(ushort index, Radix radix)
        {
            if (index >= Elements.Length) return;

            var element = Elements[index];
            element.SetRadix(radix);
        }

        public void SetElement(ushort index, string description)
        {
            if (index >= Elements.Length) return;

            var element = Elements[index];
            element.SetDescription(description);
        }

        public ITag<TType> ChangeDataType<TType>(TType dataType) where TType : IDataType
        {
            if (dataType == null)
                throw new ArgumentNullException(nameof(dataType), "Dimensions can not be null");

            return new Tag<TType>(Name, dataType, Dimensions, Radix, ExternalAccess, Description,
                Usage, Constant, Parent);
        }

        public ITag<TDataType> ChangeDimensions(Dimensions dimensions)
        {
            if (dimensions == null)
                throw new ArgumentNullException(nameof(dimensions), "Dimensions can not be null");

            return new Tag<TDataType>(Name, _dataType, dimensions, Radix, ExternalAccess, Description, Usage,
                Constant,
                Parent);
        }

        public ITag<TDataType> ChangeTagType(TagType type)
        {
            return new Tag<TDataType>(Name, _dataType, Dimensions, Radix, ExternalAccess, Description, Usage,
                Constant,
                Parent);
        }

        private IMember<TDataType>[] InstantiateElements()
        {
            var elements = new List<IMember<TDataType>>(Dimensions);

            for (var i = 0; i < Dimensions; i++)
            {
                var member = new Member<TDataType>($"[{i}]", (TDataType)_dataType.Instantiate(),
                    Dimensions.Empty, Radix, ExternalAccess, Description);
                elements.Add(member);
            }

            return elements.ToArray();
        }

        private void SetMemberDescriptions(string description)
        {
            foreach (var member in _dataType.GetMembers())
                ((Member<IDataType>)member).SetParentDescription(description);
        }

        private void SetElementDescription(string description)
        {
            foreach (var element in Elements)
                ((Member<IDataType>)element).SetParentDescription(description);
        }
    }

    public static class Tag
    {
        public static ITag<IDataType> New(string name, IDataType dataType, Dimensions dimensions = null,
            Radix radix = null, ExternalAccess externalAccess = null, string description = null, TagUsage usage = null,
            bool constant = false)
        {
            if (dataType is IUserDefined userDefined)
                return new Tag<IDataType>(name, userDefined.Instantiate(), dimensions, radix, externalAccess,
                    description, usage, constant);

            return new Tag<IDataType>(name, dataType, dimensions, radix, externalAccess, description, usage, constant);
        }

        public static ITag<TDataType> OfType<TDataType>(string name, TDataType dataType,
            Dimensions dimensions = null, Radix radix = null, ExternalAccess externalAccess = null,
            string description = null, TagUsage usage = null, bool constant = false)
            where TDataType : IDataType, new()
        {
            return new Tag<TDataType>(name, dataType, dimensions, radix, externalAccess, description, usage, constant);
        }

        public static ITag<TDataType> OfType<TDataType>(string name, string description = null,
            ExternalAccess externalAccess = null)
            where TDataType : IDataType, new()
        {
            var dataType = new TDataType();
            return new Tag<TDataType>(name, dataType, Dimensions.Empty, null, externalAccess, description, null, false);
        }

        public static ITag<IAtomic> Atomic<TAtomic>(string name, Dimensions dimensions = null, Radix radix = null,
            ExternalAccess externalAccess = null, string description = null, TagUsage usage = null,
            bool constant = false)
            where TAtomic : IAtomic, new()
        {
            var dataType = new TAtomic();
            return new Tag<IAtomic>(name, dataType, dimensions, radix, externalAccess, description, usage, constant);
        }

        public static ITag<IPredefined> Predefined<TPredefined>(string name, Dimensions dimensions = null,
            Radix radix = null, ExternalAccess externalAccess = null, string description = null, TagUsage usage = null,
            bool constant = false)
            where TPredefined : IPredefined, new()
        {
            var dataType = new TPredefined();
            return new Tag<IPredefined>(name, dataType, dimensions, radix, externalAccess, description, usage,
                constant);
        }
    }
}