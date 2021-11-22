namespace L5Sharp
{
    /// <summary>
    /// Collection of <see cref="ITask"/> components contained within a controller. 
    /// </summary>
    public interface ITasks : IComponentCollection<ITask>
    {
        /// <summary>
        /// Determines if the task collection contains a continuous task.
        /// </summary>
        /// <returns>true if the collection contains a continuous task.</returns>
        bool HasContinuous();
    }
}