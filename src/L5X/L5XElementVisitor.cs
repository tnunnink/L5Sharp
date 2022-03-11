using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Linq;
using L5Sharp.Extensions;

namespace L5Sharp.L5X
{
    /// <summary>
    /// Converts the provided type expression to an expression that takes an XElement and returns the specified return type.
    /// </summary>
    /// <remarks>
    /// This expression converter helps us convert generic component functions/predicates to XML function/predicates
    /// so that we can operate over the XML straight away and not have to first deserialize components and then perform
    /// a given function/query. 
    /// </remarks>
    internal class XElementVisitor<TType, TReturn> : ExpressionVisitor
    {
        private ParameterExpression? _typeParameter;
        private readonly ParameterExpression _elementParameter = Expression.Parameter(typeof(XElement), "e");
        private readonly List<Expression> _nullChecks = new();

        public override Expression Visit(Expression node)
        {
            if (_typeParameter is not null) return base.Visit(node);

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

            return Expression.Lambda<Func<XElement, TReturn>>(expression, _elementParameter);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            //This makes sure the current member we are visiting is one that is a property of the generic type
            //(i.e. one that needs replacing)
            if (node.Expression != _typeParameter || node.Member.MemberType != MemberTypes.Property)
                return base.VisitMember(node);

            var valueExpression = GenerateValueExpression(node);

            //Add a null check for each parameter we find to ensure we wont get object null reference exceptions.
            //These are prepended later when forming the full lambda expression.
            _nullChecks.Add(GenerateNullCheckExpression(node));

            //A converter must be retrieved for the current property type and wrapped around the value expression.
            var propertyType = ((PropertyInfo)node.Member).PropertyType;
            var converter = L5XParser.Get(propertyType);

            var convertExpression = Expression.Invoke(Expression.Constant(converter), valueExpression);

            return Expression.Convert(convertExpression, propertyType);
        }

        /// <summary>
        /// Generates an expression that represents the XElement replacement for the current generic type.
        /// </summary>
        /// <param name="expression">The member expression to replace.</param>
        /// <returns></returns>
        private Expression GenerateValueExpression(MemberExpression expression)
        {
            var callExpression = GenerateElementExpression(expression);
            return Expression.Property(callExpression, "Value");
        }

        /// <summary>
        /// Generates a null check expression for the member call.
        /// </summary>
        /// <param name="expression">The current member expression to generate a null check expression for.</param>
        /// <returns>An expression that represents a null check for the given member call.</returns>
        private Expression GenerateNullCheckExpression(MemberExpression expression)
        {
            var callExpression = GenerateElementExpression(expression);
            return Expression.NotEqual(callExpression, Expression.Constant(null));
        }

        /// <summary>
        /// Generates a XElement call expression based on the provided type expression.
        /// </summary>
        /// <param name="expression">The current type expression used to create the element expression.</param>
        /// <returns>A new <see cref="MethodCallExpression"/> that represents the XElement getter for the current type member.</returns>
        private Expression GenerateElementExpression(MemberExpression expression)
        {
            var xName = expression.Member.GetXName();
            var xInfo = expression.Member.GetXMethod();
            var elementName = Expression.Constant(xName);
            return Expression.Call(_elementParameter, xInfo, elementName);
        }

        /// <summary>
        /// Performs validation of the provided lambda expression and returns the strongly type lambda.
        /// </summary>
        /// <param name="node">The node to validate.</param>
        /// <returns>The lambda expression for the current visitor.</returns>
        /// <exception cref="ArgumentException">Thrown when the expression is not a lambda expression,
        /// does not contain only one parameter, the parameter is not of the specified type, or the return type is not
        /// of the specified return type. 
        /// </exception>
        private static LambdaExpression ValidateExpression(Expression node)
        {
            if (node is not LambdaExpression lambda)
                throw new ArgumentException($"Expression must be of type {typeof(LambdaExpression)}");

            if (lambda.Parameters.Count != 1)
                throw new ArgumentException(
                    $"Expression must have one parameter. This expression has '{lambda.Parameters.Count}'");

            if (lambda.Parameters[0].Type != typeof(TType))
                throw new ArgumentException(
                    $"Expression parameter must be of type {typeof(TType)}. This expression is of type {lambda.Parameters[0].Type}");

            if (lambda.ReturnType != typeof(TReturn))
                throw new ArgumentException(
                    $"Expression must return a {typeof(TReturn)}. This expression return {lambda.ReturnType}");

            return lambda;
        }

        /// <summary>
        /// Prepends all null check expressions to the converted expression.
        /// </summary>
        /// <param name="node">The node to prepend the checks to.</param>
        /// <returns>A new expression with prepended null checks.</returns>
        private Expression PrependNullChecks(Expression node)
        {
            return _nullChecks.Aggregate(node, (current, nullCheck) => Expression.AndAlso(nullCheck, current));
        }
    }
}