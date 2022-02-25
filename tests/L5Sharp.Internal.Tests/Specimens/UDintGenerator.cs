using System;
using AutoFixture.Kernel;
using L5Sharp.Atomics;

namespace L5Sharp.Internal.Tests.Specimens
{
    public class UDintGenerator : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (request is not Type type)
                return new NoSpecimen();

            if (type != typeof(UDint))
                return new NoSpecimen();
            
            return new UDint();
        }
    }
}