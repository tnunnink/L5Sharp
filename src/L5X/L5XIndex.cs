using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.Serialization;
using L5Sharp.Types;

namespace L5Sharp.L5X
{
    internal class L5XIndex
    {
        private readonly L5XDocument _document;
        private readonly Dictionary<Type, Dictionary<string, XElement>> _index;
        
        private readonly Dictionary<string, XElement> _dataTypeIndex;
        private readonly Dictionary<string, XElement> _moduleIndex;
        private readonly Dictionary<string, XElement> _moduleTypeIndex;
        private readonly Dictionary<string, XElement> _instructionIndex;
        private readonly Dictionary<string, XElement> _tagIndex;

        public L5XIndex(L5XDocument document)
        {
            _document = document ?? throw new ArgumentNullException(nameof(document));
            _index = new Dictionary<Type, Dictionary<string, XElement>>();
            
            _dataTypeIndex = new Dictionary<string, XElement>();
            _moduleIndex = new Dictionary<string, XElement>();
            _moduleTypeIndex = new Dictionary<string, XElement>();
            _instructionIndex = new Dictionary<string, XElement>();
            _tagIndex = new Dictionary<string, XElement>();
            
            _index.Add(typeof(IComplexType), _dataTypeIndex);
            _index.Add(typeof(ModuleDefined), _moduleTypeIndex);
            _index.Add(typeof(IModule), _moduleIndex);
            _index.Add(typeof(IAddOnInstruction), _instructionIndex);
            _index.Add(typeof(ITag<IDataType>), _tagIndex);

            IndexDocument();
        }
        
        public void Run() => IndexDocument();

        public IDataType LookupType(string name)
        {
            if (DataType.IsDefined(name))
                return DataType.Create(name);

            if (_dataTypeIndex.TryGetValue(name, out var userDefined))
                return _document.Serializers.Get<DataTypeSerializer>().Deserialize(userDefined);

            if (_moduleTypeIndex.TryGetValue(name, out var moduleDefined))
                return _document.Serializers.Get<StructureSerializer>().Deserialize(moduleDefined);
            
            if (_instructionIndex.TryGetValue(name, out var addOnDefined))
                return _document.Serializers.Get<AddOnInstructionSerializer>().Deserialize(addOnDefined);

            return new UNDEFINED(name);
        }

        public bool IsReferenced(ComponentName name) => 
            _index.Any(i => i.Value.Any(e => e.ToString().Contains(name, StringComparison.OrdinalIgnoreCase)));

        private void IndexDocument()
        {
            IndexDataTypes();
            IndexModules();
            IndexModuleTypes();
            IndexInstructions();
            IndexTags();
            //programs
            //tasks
        }

        /*private void IndexType<TComponent>() where TComponent : ILogixComponent 
        {
            var name = L5XNames.GetComponentName<TComponent>();
            var index = Index
            foreach (var index in Index)
            {
                
            }
        }*/

        private void IndexDataTypes()
        {
           _dataTypeIndex.Clear();

            var elements = _document.Content
                .Descendants(L5XElement.DataType.ToString())
                .Where(e => e.Attribute(L5XAttribute.Name.ToString()) is not null);

            foreach (var element in elements)
                _dataTypeIndex.TryAdd(element.ComponentName(), element);
        }

        private void IndexModuleTypes()
        {
            _moduleTypeIndex.Clear();
            
            var elements = _document.Content
                .Descendants(L5XElement.Module.ToString())
                .Descendants(L5XElement.Structure.ToString())
                .Where(e => e.Attribute(L5XAttribute.DataType.ToString()) is not null);
            
            foreach (var element in elements)
                _moduleTypeIndex.TryAdd(element.DataTypeName(), element);
        }

        private void IndexModules()
        {
            _moduleIndex.Clear();

            var elements = _document.Content
                .Descendants(L5XElement.Module.ToString())
                .Where(e => e.Attribute(L5XAttribute.Name.ToString()) is not null);

            foreach (var element in elements)
                _moduleIndex.TryAdd(element.ComponentName(), element);
        }

        private void IndexInstructions()
        {
            _instructionIndex.Clear();

            var elements = _document.Content
                .Descendants(L5XElement.AddOnInstructionDefinition.ToString())
                .Where(e => e.Attribute(L5XAttribute.Name.ToString()) is not null);

            foreach (var element in elements)
                _instructionIndex.TryAdd(element.ComponentName(), element);
        }

        private void IndexTags()
        {
            _tagIndex.Clear();

            var elements = _document.Content
                .Descendants(L5XElement.Tag.ToString())
                .Where(e => e.Attribute(L5XAttribute.Name.ToString()) is not null);

            foreach (var element in elements)
            {
                var program = element.Ancestors(L5XElement.Program.ToString()).FirstOrDefault()?.ComponentName();
                var name = program is not null ? $"Program:{program}.{element.ComponentName()}" : element.ComponentName();
                _tagIndex.TryAdd(name, element);
            }
        }
    }
}