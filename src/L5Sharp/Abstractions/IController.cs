using L5Sharp.Builders;

namespace L5Sharp.Abstractions
{
    public interface IController
    {
        IControllerBuilder Create();
    }
}