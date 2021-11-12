using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Extensions;
using L5Sharp.Factories;

[assembly: InternalsVisibleTo("L5Sharp.Repositories.Tests")]

namespace L5Sharp.Repositories
{
    internal class UserDefinedRepository : IUserDefinedRepository
    {
        private readonly LogixContext _context;

        public UserDefinedRepository(LogixContext context)
        {
            _context = context;
        }

        public bool Exists(string name)
        {
            return _context.L5X.DataTypes.Descendants().Any(x => x.GetName() == name);
        }

        public IUserDefined Get(string name)
        {
            var element = _context.L5X.DataTypes.Descendants().SingleOrDefault(x => x.GetName() == name);
            var factory = new UserDefinedFactory(_context);
            return factory.Create(element);
        }

        public IEnumerable<IUserDefined> GetAll()
        {
            var elements = _context.L5X.DataTypes.Descendants();
            var factory = new UserDefinedFactory(_context);
            return elements.Select(e => factory.Create(e));
        }

        public IEnumerable<IDataType> WithMemberType(IDataType dataType)
        {
            /*return Container.Descendants(LogixNames.GetComponentName<IMember<IDataType>>())
                .Where(x => x.GetValue<IMember<IDataType>, IDataType, string>(m => m.DataType, s => s) == dataType.Name)
                .Select(x => Factory.Create(x.Parent));*/
            return null;
        }

        public void Add(IUserDefined component)
        {
            if (Exists(component.Name))
                throw new ComponentNameCollisionException(component.Name, typeof(IDataType));

            var element = component.Serialize();
            _context.L5X.DataTypes.Add(element);

            var dependents = component.GetDependentTypes().Where(t => t.Class == DataTypeClass.User)
                .Cast<IUserDefined>();
            
            foreach (var dependent in dependents)
                Add(dependent);
        }

        public void Remove(IUserDefined component)
        {
            var element = _context.L5X.DataTypes.Descendants().SingleOrDefault(x => x.GetName() == component.Name);
            element?.Remove();
        }

        public void Update(IUserDefined component)
        {
            throw new System.NotImplementedException();
        }
    }
}