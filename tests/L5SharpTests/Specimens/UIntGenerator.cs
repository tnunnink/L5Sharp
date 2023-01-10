using System;
using AutoFixture.Kernel;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;

namespace L5SharpTests.Specimens
{
    public class UIntGenerator : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (request is not Type type)
                return new NoSpecimen();

            if (type != typeof(UINT))
                return new NoSpecimen();
            
            return new UINT();
        }
    }
}