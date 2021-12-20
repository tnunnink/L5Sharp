using System;
using System.Linq.Expressions;
using System.Xml.Linq;
using L5Sharp.Common;

namespace L5Sharp.Extensions
{
    internal static class ExpressionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <typeparam name="TType"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <returns></returns>
        public static Func<XElement, TReturn> ToXExpression<TType, TReturn>(this Expression<Func<TType, TReturn>> expression)
        {
            var elementVisitor = new XElementVisitor<TType, TReturn>();

            var converted = (Expression<Func<XElement, TReturn>>) elementVisitor.Visit(expression);
            
            return converted?.Compile();
        }
    }
}