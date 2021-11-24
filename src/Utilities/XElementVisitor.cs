using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Linq;
using L5Sharp.Extensions;

namespace L5Sharp.Utilities
{
    /// <summary>
    /// Converts the provided Func delegate predicate to an expression for XElement
    /// </summary>
    internal class XElementVisitor : ExpressionVisitor
    {
        private ParameterExpression _typeParameter;
        private readonly ParameterExpression _elementParameter = Expression.Parameter(typeof(XElement), "e");
        private readonly List<Expression> _nullChecks = new List<Expression>();

        public override Expression Visit(Expression node)
        {
            if (_typeParameter != null) return base.Visit(node);

            var lambda = ValidateExpression(node);

            _typeParameter = lambda.Parameters[0];
            _nullChecks.Clear();

            return base.Visit(node);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return _typeParameter == node ? _elementParameter : base.VisitParameter(node);
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            var body = Visit(node.Body);
            
            if (body == null)
                throw new InvalidOperationException();
            
            var expression = PrependNullChecks(body);
            
            return Expression.Lambda<Func<XElement, bool>>(expression, _elementParameter);
        }
        
        private Expression PrependNullChecks(Expression node)
        {
            return _nullChecks.Aggregate(node, (current, nullCheck) => Expression.AndAlso(nullCheck, current));
        }

        /*protected override Expression VisitBinary(BinaryExpression node)
        {
            if (!(node.Left is MemberExpression memberExpression && memberExpression.Expression == _typeParameter))
                return base.VisitBinary(node);

            var hasExpression = GenerateNullCheckExpression(memberExpression);

            return Expression.AndAlso(hasExpression, base.VisitBinary(node));
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (!(node.Object is MemberExpression memberExpression && memberExpression.Expression == _typeParameter))
                return base.VisitMethodCall(node);

            var hasExpression = GenerateNullCheckExpression(memberExpression);

            return Expression.AndAlso(hasExpression, base.VisitMethodCall(node));
        }*/

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Expression != _typeParameter || node.Member.MemberType != MemberTypes.Property)
                return base.VisitMember(node);

            var valueExpression = GenerateValueExpression(node);
            
            //Add a null check for each parameter we find to ensure we wont get object null reference exceptions
            _nullChecks.Add(GenerateNullCheckExpression(node)); 

            var propertyType = ((PropertyInfo)node.Member).PropertyType;
            var converter = GetConverter(propertyType);

            var convertExpression = Expression.Invoke(Expression.Constant(converter), valueExpression);

            return Expression.Convert(convertExpression, propertyType);
        }

        private Expression GenerateValueExpression(MemberExpression expression)
        {
            var callExpression = GetMemberCallExpression(expression);
            return Expression.Property(callExpression, "Value");
        }

        private Expression GenerateNullCheckExpression(MemberExpression expression)
        {
            var callExpression = GetMemberCallExpression(expression);
            return Expression.NotEqual(callExpression, Expression.Constant(null));
        }

        private Expression GetMemberCallExpression(MemberExpression expression)
        {
            var xName = expression.Member.GetXName();
            var xInfo = expression.Member.GetXMethod();

            var elementName = Expression.Constant(xName);
            return Expression.Call(_elementParameter, xInfo, elementName);
        }

        private static Func<string, object> GetConverter(Type type)
        {
            if (LogixParsers.Contains(type))
                return LogixParsers.Get(type);

            var converter = TypeDescriptor.GetConverter(type);

            if (converter.CanConvertFrom(typeof(string)))
                return s => converter.ConvertFrom(s);

            return s => s;
        }

        private static LambdaExpression ValidateExpression(Expression node)
        {
            if (!(node is LambdaExpression lambda))
                throw new ArgumentException($"Expression must be of type {typeof(LambdaExpression)}");

            if (lambda.ReturnType != typeof(bool))
                throw new ArgumentException(
                    $"Expression must return a {typeof(bool)}. This expression return {lambda.ReturnType}");

            if (lambda.Parameters.Count != 1)
                throw new ArgumentException(
                    $"Expression must have only one parameter. This expression has '{lambda.Parameters.Count}");

            return lambda;
        }
    }
}