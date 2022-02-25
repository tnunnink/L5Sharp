using System;
using AutoFixture.Kernel;
using L5Sharp.Atomics;

namespace L5Sharp.Internal.Tests.Specimens
{
    public class USintGenerator : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (request is not Type type)
                return new NoSpecimen();

            if (type != typeof(USint))
                return new NoSpecimen();
            
            return new USint();
        }
    }
}