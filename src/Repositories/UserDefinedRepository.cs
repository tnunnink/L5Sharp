using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Extensions;
using L5Sharp.Serialization;

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

        public bool Contains(string name)
        {
            return _context.L5X.DataTypes.Descendants().Any(x => x.GetName() == name);
        }

        public IUserDefined Get(string name)
        {
            var element = _context.L5X.DataTypes.Descendants().SingleOrDefault(x => x.GetName() == name);
            return _context.Serializer.Deserialize<IUserDefined>(element);
        }

        public IEnumerable<IUserDefined> GetAll()
        {
            var elements = _context.L5X.DataTypes.Descendants();
            return elements.Select(e => _context.Serializer.Deserialize<IUserDefined>(e));
        }

        public IEnumerable<IUserDefined> Find(Expression<Func<IUserDefined, bool>> predicate)
        {
            throw new NotImplementedException();
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
            if (Contains(component.Name))
                throw new ComponentNameCollisionException(component.Name, typeof(IDataType));

            var element = _context.Serializer.Serialize(component);
            _context.L5X.DataTypes.Add(element);

            var dependents = component.GetDependentTypes()
                .Where(t => t.Class == DataTypeClass.User)
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