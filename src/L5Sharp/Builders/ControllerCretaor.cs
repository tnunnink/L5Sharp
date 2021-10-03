using System;
using L5Sharp.Builders.Abstractions;
using L5Sharp.Primitives;

namespace L5Sharp.Builders
{
    internal class ControllerCreator : IControllerCreator
    {
        private readonly Controller _controller;

        public ControllerCreator(Controller controller)
        {
            _controller = controller;
        }

        public void DataType(string name, Action<IDataTypeBuilder> builder)
        {
            var typeBuilder = new DataTypeBuilder(name);
            builder.Invoke(typeBuilder);
            
            var type = typeBuilder.Build();
            
           _controller.Add(type);
        }

        public void Tag(string name, Action<ITagBuilder> builder)
        {
            /*var typeBuilder = DataTypeBuilder.HasName(name);
            builder.Invoke(typeBuilder);
            var type = typeBuilder.Build();
            _controller.CreateDataType(type);*/
        }

        public void Task(string name, Action<ITaskBuilder> builder)
        {
            throw new NotImplementedException();
        }
    }
}