using System;
using AutoFixture.Kernel;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Internal.Tests.Specimens
{
    public class ULintGenerator : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (request is not Type type)
                return new NoSpecimen();

            if (type != typeof(ULint))
                return new NoSpecimen();
            
            return new ULint();
        }
    }
}