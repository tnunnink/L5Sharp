using System;

namespace L5Sharp.Core;

internal class ConfigureOperation(Action<LogixComponent> action) : IImportOperation
{
    public bool ApplyTo(LogixComponent input)
    {
        action.Invoke(input);
        return true;
    }
}