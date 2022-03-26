using System;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Exceptions;
using L5Sharp.Extensions;
using L5Sharp.L5X;
using L5Sharp.Querying;
using L5Sharp.Serialization.Components;

namespace L5Sharp.Repositories
{
    internal class DataTypeRepository : DataTypeQuery, IDataTypeRepository
    {
        private readonly L5XDocument _document;

        public DataTypeRepository(L5XDocument document)
            : base(document.Components.Get<IComplexType>(), document.Serializers.Get<DataTypeSerializer>())
        {
            _document = document;
        }

        public void Add(IComplexType component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            if (!Elements.Any())
                _document.Containers.Create<IComplexType>();

            if (Elements.Any(e => e.ComponentName() == component.Name))
                throw new ComponentNameCollisionException(component.Name, component.GetType());

            var element = Serializer.Serialize(component);

            Elements.Last().AddAfterSelf(element);
            
            _document.Index.Run();

            /*var dependents = component.GetDependentTypes()
                .Where(t => t.Class == DataTypeClass.User)
                .Cast<IUserDefined>();

            foreach (var dependent in dependents)
                Add(dependent);*/
        }
        
        public void Remove(ComponentName name)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            var memberTypes = Elements.SelectMany(e =>
                e.Descendants(L5XElement.Member.ToString()).Select(m => m.DataTypeName()));

            if (memberTypes.Any(s => s == name))
                throw new ComponentReferencedException(name, typeof(IComplexType));

            Elements.FirstOrDefault(e => e.ComponentName() == name)?.Remove();
            
            _document.Index.Run();
        }

        public void Update(IComplexType component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));
            
            if (!Elements.Any())
                _document.Containers.Create<IComplexType>();

            var element = Serializer.Serialize(component);

            var current = Elements.FirstOrDefault(x => x.ComponentName() == component.Name);

            if (current is not null)
            {
                current.ReplaceWith(element);
                return;
            }

            Elements.Last().AddAfterSelf(element);
            
            _document.Index.Run();
        }
    }
}