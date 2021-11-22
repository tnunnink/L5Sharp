using L5Sharp.Builders;
using L5Sharp.Core;

namespace L5Sharp
{
    /// <summary>
    /// 
    /// </summary>
    public static class Parameter
    {
        public static IParameter<IDataType> Create(ComponentName name, IDataType dataType)
        {
            return new Parameter<IDataType>(name, dataType);
        }

        public static IParameter<TDataType> Create<TDataType>(ComponentName name)
            where TDataType : IDataType, new()
        {
            return new Parameter<TDataType>(name, new TDataType());
        }

        public static IParameter<TDataType> Create<TDataType>(ComponentName name, TDataType dataType)
            where TDataType : IDataType, new()
        {
            return new Parameter<TDataType>(name, dataType);
        }

        public static IParameterBuilder<IDataType> Build(ComponentName name, IDataType dataType)
        {
            return new ParameterBuilder<IDataType>(name, dataType);
        }

        public static IParameterBuilder<TDataType> Build<TDataType>(ComponentName name)
            where TDataType : IDataType, new()
        {
            var dataType = new TDataType();
            return new ParameterBuilder<TDataType>(name, dataType);
        }

        public static IParameterBuilder<TDataType> Build<TDataType>(ComponentName name, TDataType dataType)
            where TDataType : IDataType, new()
        {
            return new ParameterBuilder<TDataType>(name, dataType);
        }
    }
}