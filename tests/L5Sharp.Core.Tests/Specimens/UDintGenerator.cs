using System;
using AutoFixture.Kernel;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Core.Tests.Specimens
{
    public class UDintGenerator : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (request is not Type type)
                return new NoSpecimen();

            if (type != typeof(UDINT))
                return new NoSpecimen();
            
            return new UDINT();
        }
    }
}