using L5Sharp.Abstractions;

namespace L5Sharp.Core
{
    /// <inheritdoc cref="L5Sharp.ITasks" />
    public class Tasks : ComponentCollection<ITask>, ITasks
    {
        private readonly IController _controller;

        /// <summary>
        /// Creates a new instance of the Tasks collection given the current controller instance 
        /// </summary>
        /// <param name="controller"></param>
        public Tasks(IController controller)
        {
            _controller = controller;
        }
        
        
    }
}