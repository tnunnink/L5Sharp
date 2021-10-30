using System;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;

namespace L5Sharp.Utilities
{
    internal static class Throw
    {
        public static void PredefinedCollisionException(string dataType) =>
            throw new PredefinedCollisionException(
                $"Data type {dataType} already exists either as a predefined type.");
    }
}