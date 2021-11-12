namespace L5Sharp
{
    public interface ILogixComponent
    {
        /// <summary>
        /// The name property of the Logix component.
        /// The name servers as a unique identifier for various different types of components.
        /// The component name should satisfy RSLogix naming constraints of alphanumeric and '_' characters,
        /// not start with a number, and be less that 40 characters
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// The description property of the Logix component. Simple string that describes the component.
        /// This value can be empty or null
        /// </summary>
        public string Description { get; }
    }
}