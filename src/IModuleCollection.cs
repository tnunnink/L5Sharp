using System;
using System.Collections.Generic;
using System.Net;
using L5Sharp.Core;
using L5Sharp.Exceptions;

namespace L5Sharp
{
    /// <summary>
    /// 
    /// </summary>
    public interface IModuleCollection : IEnumerable<IModule>
    {
        /// <summary>
        /// Gets a child <see cref="IModule"/> object at the specified address.
        /// </summary>
        /// <param name="address">The address (slot/IP) of the module to get.</param>
        /// <returns>The <see cref="IModule"/> instance of the child at the provided address if one exists and the
        /// current parent module has a local bus; otherwise, null.</returns>
        IModule? AtAddress(string address);

        /// <summary>
        /// Gets a child <see cref="IModule"/> object with the specified name.
        /// </summary>
        /// <param name="name">The name of the module to get.</param>
        /// <returns>The <see cref="IModule"/> instance of the child that has the provided name, if one exists, and the
        /// current parent module has a local bus; otherwise, null.</returns>
        IModule? Named(ComponentName name);

        /// <summary>
        /// Creates a new <see cref="IModule"/> object and adds it as a child to the current module local port.
        /// </summary>
        /// <param name="name">The name of the module to create.</param>
        /// <param name="catalogNumber">The catalog number of the module to create.</param>
        /// <param name="slot">The optional slot number at which to assign the new module.
        /// If the provided slot is not available (i.e. taken by another module) and the bus type is a chassis based port,
        /// the next available slot address will be determined. If not provided will default to '0'.</param>
        /// <param name="description">The optional description of the module. Will default to an empty string.</param>
        /// <param name="catalogService">The optional service provider implementation that retrieves the
        /// correct <see cref="ModuleDefinition"/> using the provided catalog number. If not provided, the default will
        /// use the built in <see cref="ModuleCatalog"/> implementation, which relies on Logix software installation
        /// on the current environment.</param>
        /// <returns>The new <see cref="IModule"/> instance that was created if the current local port bus is not null
        /// (i.e. this module can actually hold child modules); otherwise null.</returns>
        /// <exception cref="ArgumentNullException">name or catalog number are null.</exception>
        /// <exception cref="ComponentNameCollisionException">name already exists as a child of the module.</exception>
        /// <exception cref="ModuleNotFoundException">catalogNumber could not be found by the catalogService.</exception>
        /// <exception cref="ArgumentException">The module definition obtained for catalogNumber
        /// does not have a valid upstream port needed to form a connection to the current module local port bus.
        /// </exception>
        /// <remarks>
        /// This overload is intended for adding rack or chassis based modules to the current parent module. This is
        /// just due to the order of the arguments to make the api as succinct as possible. This will not technically
        /// fail if the current module local port is an ethernet type port.
        /// </remarks>
        IModule? New(ComponentName name, CatalogNumber catalogNumber, byte slot = default, string? description = null,
            ICatalogService? catalogService = null);

        /// <summary>
        /// Creates a new <see cref="IModule"/> object and adds it as a child to the current module local port.
        /// </summary>
        /// <param name="name">The name of the module to create.</param>
        /// <param name="catalogNumber">The catalog number of the module to create.</param>
        /// <param name="ipAddress">The optional ip address at which to assign the new module.
        /// If the provided address is not available (i.e. taken by another module), and the bus type is a ethernet
        /// based port, the next available slot address will be determined. If not provided will default to '0.0.0.0'.
        /// </param>
        /// <param name="description">The optional description of the module. Will default to an empty string.</param>
        /// <param name="catalogService">The optional service provider implementation that retrieves the
        /// correct <see cref="ModuleDefinition"/> using the provided catalog number. If not provided, the default will
        /// use the built in <see cref="ModuleCatalog"/> implementation, which relies on Logix software installation
        /// on the current environment.</param>
        /// <returns>The new <see cref="IModule"/> instance that was created if the current local port bus is not null
        /// (i.e. this module can actually hold child modules); otherwise null.</returns>
        /// <exception cref="ArgumentNullException">name or catalog number are null.</exception>
        /// <exception cref="ComponentNameCollisionException">name already exists as a child of the module.</exception>
        /// <exception cref="ModuleNotFoundException">catalogNumber could not be found by the catalogService.</exception>
        /// <exception cref="ArgumentException">The module definition obtained for catalogNumber
        /// does not have a valid upstream port needed to form a connection to the current module local port bus.
        /// </exception>
        /// <remarks>
        /// This overload is intended for adding rack or chassis based modules to the current parent module. This is
        /// just due to the order of the arguments to make the api as succinct as possible. This will not technically
        /// fail if the current module local port is an ethernet type port.
        /// </remarks>
        IModule? New(ComponentName name, CatalogNumber catalogNumber, IPAddress? ipAddress = null,
            string? description = null, ICatalogService? catalogService = null);

        /// <summary>
        /// Creates a new <see cref="IModule"/> object and adds it as a child to the current module local port.
        /// </summary>
        /// <param name="name">The name of the module to create.</param>
        /// <param name="catalogNumber">The catalog number of the module to create.</param>
        /// <param name="slot">The slot number at which to assign the new module.
        /// If the provided slot is not available (i.e. taken by another module) and the bus type is a chassis based port,
        /// the next available slot address will be determined.</param>
        /// <param name="ipAddress">The IP address of the ethernet based port for the new module.</param>
        /// <param name="description">The optional description of the module. Will default to an empty string.</param>
        /// <param name="catalogService">The optional service provider implementation that retrieves the
        /// correct <see cref="ModuleDefinition"/> using the provided catalog number. If not provided, the default will
        /// use the built in <see cref="ModuleCatalog"/> implementation, which relies on Logix software installation
        /// on the current environment.</param>
        /// <returns>The new <see cref="IModule"/> instance that was created if the current local port bus is not null
        /// (i.e. this module can actually hold child modules); otherwise null.</returns>
        /// <exception cref="ArgumentNullException">name or catalog number are null.</exception>
        /// <exception cref="ComponentNameCollisionException">name already exists as a child of the module.</exception>
        /// <exception cref="ModuleNotFoundException">catalogNumber could not be found by the catalogService.</exception>
        /// <exception cref="ArgumentException">The module definition obtained for catalogNumber
        /// does not have a valid upstream port needed to form a connection to the current module local port bus.
        /// </exception>
        IModule? New(ComponentName name, CatalogNumber catalogNumber, byte slot, IPAddress ipAddress,
            string? description = null, ICatalogService? catalogService = null);

        /// <summary>
        /// Creates a new <see cref="IModule"/> with the provided name and module definition and adds it as a child
        /// to the current module local port.
        /// </summary>
        /// <param name="name">The name of the module to create.</param>
        /// <param name="definition">The <see cref="ModuleDefinition"/> of the module to create.</param>
        /// <param name="description">The optional description for the module. Will default to an empty string.</param>
        /// <returns>The new <see cref="IModule"/> instance that was created if the current local port bus is not null
        /// (i.e. this module can actually hold child modules); otherwise null.</returns>
        /// <exception cref="ArgumentNullException">definition is null.</exception>
        /// <exception cref="ComponentNameCollisionException">name already exists on the Bus instance.</exception>
        /// <exception cref="ArgumentException">
        /// definition does not have a configured upstream port that matches the bus type -or-
        /// definition does not a connecting port address that is available on the bus.
        /// </exception>
        IModule? New(ComponentName name, ModuleDefinition definition, string? description = null);

        /// <summary>
        /// Removes the <see cref="IModule"/> with the specified name from the <see cref="Bus"/> if it exists.
        /// </summary>
        /// <param name="name">The name of the module to remove.</param>
        /// <returns>true if the module was found and removed from the local port bus.
        /// If the module was not found or the provided name is that of the current module, then false.
        /// </returns>
        bool Remove(ComponentName name);
    }
}