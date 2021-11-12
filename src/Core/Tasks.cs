using System;
using L5Sharp.Abstractions;
using L5Sharp.Builders;

namespace L5Sharp.Core
{
    public class Tasks : ComponentCollection<ITask>, ITasks
    {
        private readonly IController _controller;

        public Tasks(IController controller)
        {
            _controller = controller;
        }

        public void Add(Action<ITaskBuilder> builder)
        {
            var taskBuilder = new TaskBuilder();
            builder.Invoke(taskBuilder);
            Add(taskBuilder.Create());
        }
    }
}