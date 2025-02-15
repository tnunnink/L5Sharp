using System;
using AutoFixture.Kernel;

namespace L5Sharp.Tests.Core.Specimens
{
    public class LintGenerator : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (request is not Type type)
                return new NoSpecimen();

            if (type != typeof(LINT))
                return new NoSpecimen();
            
            return new LINT();
        }
    }
}