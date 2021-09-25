namespace L5Sharp.Builders
{
    public abstract class BaseBuilder<T> : IBuilder<T>
    {
        protected readonly T Model;

        protected BaseBuilder(T model)
        {
            Model = model;
        }

        public virtual T Build()
        {
            return Model;
        }
    }
}