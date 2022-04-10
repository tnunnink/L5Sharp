using System;
using System.Net;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Builders
{
    /// <summary>
    /// A fluent api for building <see cref="IModule"/> components.
    /// </summary>
    public interface IModuleBuilder : IComponentBuilder<IModule>
    {
        /// <summary>
        /// Configures the parent module parameters for the new <see cref="IModule"/>.
        /// </summary>
        /// <param name="parent">The <see cref="IModule"/> instance that is the parent of the new module.</param>
        /// <returns>A <see cref="IModuleBuilder"/> instance with the configured parent module.</returns>
        /// <remarks>
        /// This method will use the parent name and the first found downstream port as the parent module port id.
        /// </remarks>
        IModuleBuilder WithParent(IModule parent);

        /*
        /// <summary>
        /// Configures the connection to the parent module for the new <see cref="IModule"/>.
        /// </summary>
        /// <param name="parentName">The name of the parent of the new module.</param>
        /// <param name="parentPortId">The port id upstream for which the new module is connected.</param>
        /// <returns>A <see cref="IModuleBuilder"/> instance with the configured parent properties.</returns>
        IModuleBuilder WithParent(string parentName, int parentPortId);*/
        
        /// <summary>
        /// Configures the IP address of the new <see cref="IModule"/> to the provided value.
        /// </summary>
        /// <param name="ip">The <see cref="IPAddress"/> value of the new module.</param>
        /// <returns>A <see cref="IModuleBuilder"/> instance with the configured IP address.</returns>
        /// <exception cref="InvalidOperationException">
        /// The definition for the specified module catalog does not have an ethernet type port.
        /// </exception>
        IModuleBuilder WithIP(IPAddress ip);
        
        /// <summary>
        /// Configures the description of the new <see cref="IModule"/> to the provided value.
        /// </summary>
        /// <param name="description">The description of the new module.</param>
        /// <returns>A <see cref="IModuleBuilder"/> instance with the configured description.</returns>
        IModuleBuilder WithDescription(string description);
        
        /// <summary>
        /// Configures the revision of the new <see cref="IModule"/> to the provided value.
        /// </summary>
        /// <param name="revision">The <see cref="Revision"/> number of the module.</param>
        /// <returns>A <see cref="IModuleBuilder"/> instance with the configured revision number.</returns>
        /// <exception cref="InvalidOperationException">
        /// The definition for the specified module catalog number does not have a revision value equal to <c>revision</c>.
        /// </exception>
        IModuleBuilder WithRevision(Revision revision);
        
        /// <summary>
        /// Configures the slot number of the new <see cref="IModule"/> to the provided value.
        /// </summary>
        /// <param name="slot">The byte number that represents the slot number of the module.</param>
        /// <returns>A <see cref="IModuleBuilder"/> instance with the configured slot number.</returns>
        /// <exception cref="InvalidOperationException">The definition for the specified module catalog number does
        /// not have a non-Ethernet type port that can be configured with a slot number.</exception>
        IModuleBuilder WithSlot(byte slot);
        
        /// <summary>
        /// Configures the inhibited value of the mew <see cref="IModule"/> to be true.
        /// </summary>
        /// <returns>A <see cref="IModuleBuilder"/> instance with the inhibited property set to true.</returns>
        IModuleBuilder IsInhibited();
        
        /// <summary>
        /// Configures the major fault value of the mew <see cref="IModule"/> to be true.
        /// </summary>
        /// <returns>A <see cref="IModuleBuilder"/> instance with the major fault property set to true.</returns>
        IModuleBuilder MajorFaults();
        
        /// <summary>
        /// Configures the safety enabled value of the mew <see cref="IModule"/> to be true.
        /// </summary>
        /// <returns>A <see cref="IModuleBuilder"/> instance with the safety enabled property set to true.</returns>
        IModuleBuilder IsSafetyEnabled();
        
        /// <summary>
        /// Configures the keying state of the new <see cref="IModule"/> to the provided value.
        /// </summary>
        /// <param name="keying">The <see cref="ElectronicKeying"/> value.</param>
        /// <returns>A <see cref="IModuleBuilder"/> instance with the configured keying state.</returns>
        IModuleBuilder WithState(ElectronicKeying keying);
    }
}