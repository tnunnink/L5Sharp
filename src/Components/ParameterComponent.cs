﻿using System;
using L5Sharp.Core;

namespace L5Sharp.Components
{
    /// <summary>
    /// 
    /// </summary>
    public static class Parameter
    {
        public static IParameter<IDataType> Create(ComponentName name, IDataType dataType)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            
            return new Parameter<IDataType>(name, dataType);
        }

        public static IParameter<TDataType> Create<TDataType>(ComponentName name)
            where TDataType : IDataType, new()
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            
            return new Parameter<TDataType>(name, new TDataType());
        }

        public static IParameter<TDataType> Create<TDataType>(ComponentName name, TDataType dataType)
            where TDataType : IDataType, new()
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            
            return new Parameter<TDataType>(name, dataType);
        }
    }
}