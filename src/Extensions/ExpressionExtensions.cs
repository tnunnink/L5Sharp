using System;
using System.Linq.Expressions;
using System.Xml.Linq;
using L5Sharp.Utilities;

namespace L5Sharp.Extensions
{
    internal static class ExpressionExtensions
    {
        /// <summary>
        /// Converts an expression for a given type to an expression of the 
        /// </summary>
        /// <param name="expression"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static Func<XElement, bool> ToElementFunc<T>(this Expression<Func<T, bool>> expression)
        {
            var elementVisitor = new XElementVisitor();

            var converted = (Expression<Func<XElement, bool>>) elementVisitor.Visit(expression);
            
            return converted?.Compile();
        }
    }
}