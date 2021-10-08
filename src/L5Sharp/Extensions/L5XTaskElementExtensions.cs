using System;
using System.Xml.Linq;
using L5Sharp.Enumerations;
using L5Sharp.Utilities;

namespace L5Sharp.Extensions
{
    public static class L5XTaskElementExtensions
    {
        private const string ExpectedElementName = L5XNames.Components.Task;

        /// <summary>
        /// Helper extension that gets the "Priority" attribute from the current element
        /// </summary>
        /// <param name="element">The current element instance</param>
        /// <returns>Parsed value of the specified attribute</returns>
        public static byte GetPriority(this XElement element)
        {
            if (element.Name != ExpectedElementName)
                throw new InvalidOperationException(
                    $"Element name '{element.Name}' is not expected value '{ExpectedElementName}'");
                    
            var value = element.Attribute("Priority")?.Value;
            
            return Convert.ToByte(value);
        }

        /// <summary>
        /// Helper extension that gets the attribute "Watchdog" from the current element
        /// </summary>
        /// <param name="element">The current element instance</param>
        /// <returns>Parsed value of the specified attribute</returns>
        public static float GetWatchdog(this XElement element)
        {
            if (element.Name != ExpectedElementName)
                throw new InvalidOperationException(
                    $"Element name '{element.Name}' is not expected value '{ExpectedElementName}'");
                    
            var value = element.Attribute("Watchdog")?.Value;
            
            return Convert.ToSingle(value);
        }

        /// <summary>
        /// Helper extension that gets the attribute "Rate" from the current element
        /// </summary>
        /// <param name="element">The current element instance</param>
        /// <returns>Parsed value of the specified attribute</returns>
        public static float GetRate(this XElement element)
        {
            if (element.Name != ExpectedElementName)
                throw new InvalidOperationException(
                    $"Element name '{element.Name}' is not expected value '{ExpectedElementName}'");
                    
            var value = element.Attribute("Rate")?.Value;
            
            return Convert.ToSingle(value);
        }
        
        /// <summary>
        /// Helper extension that gets the attribute "InhibitTask" from the current element
        /// </summary>
        /// <param name="element">The current element instance</param>
        /// <returns>Parsed value of the specified attribute</returns>
        public static bool GetInhibitTask(this XElement element)
        {
            if (element.Name != ExpectedElementName)
                throw new InvalidOperationException(
                    $"Element name '{element.Name}' is not expected value '{ExpectedElementName}'");
                    
            var value = element.Attribute("InhibitTask")?.Value;
            
            return Convert.ToBoolean(value);
        }
        
        /// <summary>
        /// Helper extension that gets the attribute "DisableUpdateOutputs" from the current element
        /// </summary>
        /// <param name="element">The current element instance</param>
        /// <returns>Parsed value of the specified attribute</returns>
        public static bool GetDisableUpdateOutputs(this XElement element)
        {
            if (element.Name != ExpectedElementName)
                throw new InvalidOperationException(
                    $"Element name '{element.Name}' is not expected value '{ExpectedElementName}'");
                    
            var value = element.Attribute("DisableUpdateOutputs")?.Value;
            
            return Convert.ToBoolean(value);
        }

        /// <summary>
        /// Helper extension that gets the attribute "EventTrigger" from the current element
        /// </summary>
        /// <param name="element">The current element instance</param>
        /// <returns>Parsed value of the specified attribute</returns>
        public static TaskTrigger GetEventTrigger(this XElement element)
        {
            if (element.Name != ExpectedElementName)
                throw new InvalidOperationException(
                    $"Element name '{element.Name}' is not expected value '{ExpectedElementName}'");
                    
            var value = element.Attribute("EventTrigger")?.Value;
            
            return value != null ? TaskTrigger.FromName(value) : null;
        }
        
        /// <summary>
        /// Helper extension that gets the attribute "EventTag" from the current element
        /// </summary>
        /// <param name="element">The current element instance</param>
        /// <returns>Parsed value of the specified attribute</returns>
        public static string GetEventTag(this XElement element)
        {
            if (element.Name != ExpectedElementName)
                throw new InvalidOperationException(
                    $"Element name '{element.Name}' is not expected value '{ExpectedElementName}'");
                    
            return element.Attribute("EventTag")?.Value;
        }
        
        /// <summary>
        /// Helper extension that gets the attribute "EnableTimeout" from the current element
        /// </summary>
        /// <param name="element">The current element instance</param>
        /// <returns>Parsed value of the specified attribute</returns>
        public static bool GetEnableTimeout(this XElement element)
        {
            if (element.Name != ExpectedElementName)
                throw new InvalidOperationException(
                    $"Element name '{element.Name}' is not expected value '{ExpectedElementName}'");
                    
            return Convert.ToBoolean(element.Attribute("EnableTimeout")?.Value);
        }
    }
}