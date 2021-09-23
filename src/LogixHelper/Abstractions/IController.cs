using LogixHelper.Builders;

namespace LogixHelper.Abstractions
{
    public interface IController
    {
        IControllerBuilder Create();
    }
}