namespace L5Sharp
{
    public interface ILogixComponent
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