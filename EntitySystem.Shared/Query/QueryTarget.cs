using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Shared.Query;

public class QueryTarget : IQueryParameter
{
    public Guid Guid { get; set; }

    public string Property { get; set; }

    public QueryTarget Clone(IDictionary<Guid, Guid> mapping = null)
    {
        return new QueryTarget
        {
            Guid = mapping != null && mapping.TryGetValue(Guid, out var mapped) ? mapped : Guid,
            Property = Property
        };
    }

    public static QueryTarget From(Guid guid, Expression expression)
    {
        return new QueryTarget
        {
            Guid = guid,
            Property = ToString(expression)
        };
    }

    public static string ToString(Expression expression, bool first = true)
    {
        return expression switch
        {
            UnaryExpression { NodeType: ExpressionType.Convert } unary => ToString(unary.Operand, first),
            ParameterExpression when first => $"{nameof(IEntity.Id)}",
            MemberExpression memberExpression when first && typeof(IEntity).IsAssignableFrom(memberExpression.Type) => $"{ToString(memberExpression, false)}.{nameof(IEntity.Id)}",
            MemberExpression memberExpression => memberExpression.Expression switch
            {
                UnaryExpression { NodeType: ExpressionType.Convert, Operand: ParameterExpression } => $"{memberExpression.Member.Name}",
                UnaryExpression { NodeType: ExpressionType.Convert, Operand: MemberExpression next } => $"{ToString(next, false)}.{memberExpression.Member.Name}",
                ParameterExpression => $"{memberExpression.Member.Name}",
                MemberExpression next => $"{ToString(next, false)}.{memberExpression.Member.Name}",
                _ => throw new InvalidOperationException("Invalid expression")
            },
            _ => throw new InvalidOperationException("Invalid expression")
        };
    }

    public override string ToString()
    {
        return $"{Guid}.{Property}";
    }
}