using System;

namespace L5Sharp.Abstractions
{
    public interface IComponent
    {
        /// <summary>
        /// The name property of the Logix component
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// The description property of the Logix component
        /// </summary>
        public string Description { get; }
    }
}