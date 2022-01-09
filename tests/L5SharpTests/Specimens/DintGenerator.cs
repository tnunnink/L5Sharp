using System;
using AutoFixture.Kernel;
using L5Sharp.Types;

namespace L5SharpTests.Specimens
{
    public class DintGenerator : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (request is not Type type)
                return new NoSpecimen();

            if (type != typeof(Dint))
                return new NoSpecimen();
            
            return new Dint();
        }
    }
}