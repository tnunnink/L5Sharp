using System;
using L5Sharp.L5X;
using L5Sharp.Querying;

namespace L5Sharp.Repositories
{
    /// <summary>
    /// An <see cref="IComponentRepository{TComponent}"/> for the <see cref="IComplexType"/> component that provides
    /// the ability to preform CRUD operations of user defined types in a <see cref="L5XContext"/>.
    /// </summary>
    public interface IDataTypeRepository : IComponentRepository<IComplexType>, IDataTypeQuery
    {
        /// <summary>
        /// Adds the provided <see cref="IComplexType"/> and all direct and indirectly dependent user defined
        /// member types to the L5X document.
        /// </summary>
        /// <param name="dataType">The <see cref="IComplexType"/> instance to add.</param>
        /// <exception cref="ArgumentNullException">dataType is null.</exception>
        void AddAll(IComplexType dataType);
    }
}