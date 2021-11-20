using L5Sharp.Abstractions;

namespace L5Sharp.Core
{
    public class DataTypes : ComponentCollection<IUserDefined>
    {
        private readonly IController _controller;

        public DataTypes(IController controller)
        {
            _controller = controller;
        }
    }
}