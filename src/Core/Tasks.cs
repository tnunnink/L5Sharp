using System;
using L5Sharp.Abstractions;
using L5Sharp.Enums;

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

        /// <inheritdoc />
        public override void Add(ITask component)
        {
            if (component.Type == TaskType.Continuous && _controller.Tasks.HasContinuous())
                throw new ArgumentException();
            
            base.Add(component);
        }

        /// <inheritdoc />
        public override void Update(ITask component)
        {
            if (component.Type == TaskType.Continuous && _controller.Tasks.HasContinuous())
                throw new ArgumentException();
            
            base.Update(component);
        }

        /// <inheritdoc />
        public bool HasContinuous()
        {
            return Contains(t => t.Type == TaskType.Continuous);
        }

        /// <inheritdoc />
        public bool AtCapacity()
        {
            return Count == 32;
        }
    }
}