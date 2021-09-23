using LogixHelper.Abstractions;
using LogixHelper.Primitives;

namespace LogixHelper.Builders
{
    internal class ControllerBuilder : IControllerBuilder
    {
        private readonly Controller _controller;

        public ControllerBuilder(Controller controller)
        {
            _controller = controller;
        }
        
        public IDataTypeBuilder DataType(string name, string description)
        {
            return new DataTypeBuilder(_controller, name, description);
        }
    }
}