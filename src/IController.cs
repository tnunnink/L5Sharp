using System;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    public interface IController : ILogixComponent
    {
        Use Use { get; }
        ProcessorType ProcessorType { get; }
        Revision Revision { get; }
        DateTime ProjectCreationDate { get; }
        DateTime LastModifiedDate { get; }
    }
}