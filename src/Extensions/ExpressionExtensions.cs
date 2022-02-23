using System;
using System.Linq.Expressions;
using System.Xml.Linq;
using L5Sharp.L5X;

namespace L5Sharp.Extensions
{
    internal static class ExpressionExtensions
    {
        public static Func<XElement, TReturn> ToXExpression<TType, TReturn>(this Expression<Func<TType,
            TReturn>> expression)
        {
            var elementVisitor = new XElementVisitor<TType, TReturn>();

            var converted = (Expression<Func<XElement, TReturn>>) elementVisitor.Visit(expression);
            
            return converted.Compile();
        }
    }
}