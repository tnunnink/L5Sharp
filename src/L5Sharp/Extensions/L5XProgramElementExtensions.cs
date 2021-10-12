using System;
using System.Xml.Linq;
using L5Sharp.Utilities;

namespace L5Sharp.Extensions
{
    public static class L5XProgramElementExtensions
    {
        private const string ExpectedElementName = LogixNames.Components.Program;
        private static readonly string TestEdits = nameof(TestEdits);
        private static readonly string Disabled = nameof(Disabled);
        private static readonly string UseAsFolder = nameof(UseAsFolder);
        private static readonly string MainRoutineName = nameof(MainRoutineName);
        private static readonly string FaultRoutineName = nameof(FaultRoutineName);
        private static readonly string InitialStepIndex = nameof(InitialStepIndex);
        private static readonly string InitialState = nameof(InitialState);
        private static readonly string CompleteStateIfNotImpl = nameof(CompleteStateIfNotImpl);
        private static readonly string LossOfCommCmd = nameof(LossOfCommCmd);
        private static readonly string ExternalRequestAction = nameof(ExternalRequestAction);
        private static readonly string AutoValueAssignStepToPhase = nameof(AutoValueAssignStepToPhase);
        private static readonly string AutoValueAssignPhaseToStepOnComplete = nameof(AutoValueAssignPhaseToStepOnComplete);
        private static readonly string AutoValueAssignPhaseToStepOnStopped = nameof(AutoValueAssignPhaseToStepOnStopped);
        private static readonly string AutoValueAssignPhaseToStepOnAborted = nameof(AutoValueAssignPhaseToStepOnAborted);

        /// <summary>
        /// Helper extension that gets the "TestEdits" attribute from the current element
        /// </summary>
        /// <param name="element">The current element instance</param>
        /// <returns>Parsed value of the specified attribute</returns>
        public static bool GetTestEdits(this XElement element)
        {
            if (element.Name != ExpectedElementName)
                throw new InvalidOperationException(
                    $"Element name '{element.Name}' is not expected value '{ExpectedElementName}'");
                    
            var value = element.Attribute(TestEdits)?.Value;
            
            return Convert.ToBoolean(value);
        }
        
        /// <summary>
        /// Helper extension that gets the "Disabled" attribute from the current element
        /// </summary>
        /// <param name="element">The current element instance</param>
        /// <returns>Parsed value of the specified attribute</returns>
        public static bool GetDisabled(this XElement element)
        {
            if (element.Name != ExpectedElementName)
                throw new InvalidOperationException(
                    $"Element name '{element.Name}' is not expected value '{ExpectedElementName}'");
                    
            var value = element.Attribute(Disabled)?.Value;
            
            return Convert.ToBoolean(value);
        }
        
        /// <summary>
        /// Helper extension that gets the "UseAsFolder" attribute from the current element
        /// </summary>
        /// <param name="element">The current element instance</param>
        /// <returns>Parsed value of the specified attribute</returns>
        public static bool GetUseAsFolder(this XElement element)
        {
            if (element.Name != ExpectedElementName)
                throw new InvalidOperationException(
                    $"Element name '{element.Name}' is not expected value '{ExpectedElementName}'");
                    
            var value = element.Attribute(UseAsFolder)?.Value;
            
            return Convert.ToBoolean(value);
        }
        
        /// <summary>
        /// Helper extension that gets the "MainRoutineName" attribute from the current element
        /// </summary>
        /// <param name="element">The current element instance</param>
        /// <returns>Parsed value of the specified attribute</returns>
        public static string GetMainRoutineName(this XElement element)
        {
            if (element.Name != ExpectedElementName)
                throw new InvalidOperationException(
                    $"Element name '{element.Name}' is not expected value '{ExpectedElementName}'");
                    
            return element.Attribute(MainRoutineName)?.Value;
        }
        
        /// <summary>
        /// Helper extension that gets the "FaultRoutineName" attribute from the current element
        /// </summary>
        /// <param name="element">The current element instance</param>
        /// <returns>Parsed value of the specified attribute</returns>
        public static string GetFaultRoutineName(this XElement element)
        {
            if (element.Name != ExpectedElementName)
                throw new InvalidOperationException(
                    $"Element name '{element.Name}' is not expected value '{ExpectedElementName}'");
                    
            return element.Attribute(FaultRoutineName)?.Value;
        }
    }
}