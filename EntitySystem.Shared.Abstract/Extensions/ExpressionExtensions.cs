using System;
using System.Linq.Expressions;
using System.Reflection;

namespace EntitySystem.Shared.Abstract.Extensions;

public static class ExpressionExtensions
{
    public static Expression<Func<TEntity, TConverted>> Convert<TEntity, TOriginal, TConverted>(this Expression<Func<TEntity, TOriginal>> expression)
    {
        var converted = Expression.Convert(expression.Body, typeof(TConverted));

        return Expression.Lambda<Func<TEntity, TConverted>>(converted, expression.Parameters);
    }

    public static Action<TEntity, TValue> CompileSetter<TEntity, TValue>(this Expression<Func<TEntity, TValue>> property)
    {
        if (property.Body.NodeType == ExpressionType.Parameter) return (_, _) => { };

        /*if (property.Body.NodeType != ExpressionType.MemberAccess || property.Body is not MemberExpression propertyMember)
            throw new ArgumentException("Must be member");*/

        var current = property.Body;

        while (current.NodeType != ExpressionType.Parameter)
        {
            // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
            current = current.NodeType switch
            {
                ExpressionType.Call when current is MethodCallExpression innerCall => innerCall.Object,
                ExpressionType.MemberAccess when current is MemberExpression member => member.Expression,
                ExpressionType.Convert when current is UnaryExpression unary => unary.Operand,
                _ => throw new ArgumentException("Invalid expression node")

            } ?? throw new ArgumentException("Expression cannot be null");
        }

        if (current is not ParameterExpression parameter) throw new ArgumentException("Must be parameter");

        var value = Expression.Parameter(typeof(TValue), "value");

        current = property.Body;

        while (current.NodeType != ExpressionType.MemberAccess)
        {
            // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
            current = current.NodeType switch
            {
                ExpressionType.MemberAccess when current is MemberExpression member => member.Expression,
                ExpressionType.Convert when current is UnaryExpression unary => unary.Operand,
                _ => throw new ArgumentException("Invalid expression node")

            } ?? throw new ArgumentException("Expression cannot be null");
        }

        if (current is not MemberExpression { Member: PropertyInfo propertyInfo } propertyMember) throw new InvalidOperationException("Must be property info");

        var setter = propertyInfo.GetSetMethod() ?? throw new InvalidOperationException("Must have setter");

        var call = property.Body.NodeType == ExpressionType.Convert && property.Body is UnaryExpression convertExpression ? 
            Expression.Call(propertyMember.Expression, setter, Expression.Convert(value, convertExpression.Type)) : 
            Expression.Call(propertyMember.Expression, setter, value);

        return Expression.Lambda<Action<TEntity, TValue>>(call, parameter, value).Compile();
    }

    public static Expression<Func<TSource, TResult>> Compose<TSource, TIntermediate, TResult>(
        this Expression<Func<TSource, TIntermediate>> first,
        Expression<Func<TIntermediate, TResult>> second)
    {
        var param = Expression.Parameter(typeof(TSource));

        var intermediateValue = first.Body.ReplaceParameter(first.Parameters[0], param);

        var body = second.Body.ReplaceParameter(second.Parameters[0], intermediateValue);

        return Expression.Lambda<Func<TSource, TResult>>(body, param);
    }

    public static Expression ReplaceParameter(this Expression expression,
        ParameterExpression toReplace,
        Expression newExpression)
    {
        return new ParameterReplaceVisitor(toReplace, newExpression)
            .Visit(expression);
    }

    public class ParameterReplaceVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression _from;
        private readonly Expression _to;

        public ParameterReplaceVisitor(ParameterExpression from, Expression to)
        {
            _from = from;
            _to = to;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return node == _from ? _to : node;
        }
    }
}