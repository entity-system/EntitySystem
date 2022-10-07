using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EntitySystem.Shared.Query;

public class QueryConstant : IQueryParameter
{
    public string Text { get; set; }

    public int? Integer { get; set; }

    public long? Long { get; set; }

    public bool? Bool { get; set; }

    public bool Null { get; set; }

    public QueryConstant Clone(IDictionary<Guid, Guid> mapping = null)
    {
        return new QueryConstant
        {
            Text = Text,
            Integer = Integer,
            Long = Long,
            Bool = Bool,
            Null = Null
        };
    }

    public object GetValue()
    {
        if (Text != null) return Text;

        if (Integer != null) return Integer;

        if (Long != null) return Long;

        if (Bool != null) return Bool;

        if (Null) return null;

        throw new InvalidOperationException($"Unable to do action {nameof(GetValue)} for {nameof(QueryConstant)}, all supported types are null");
    }

    public static QueryConstant From(ConstantExpression constant)
    {
        return FromValue(constant.Value);
    }

    public static QueryConstant From(MemberExpression member)
    {
        var objectMember = Expression.Convert(member, typeof(object));

        var getterLambda = Expression.Lambda<Func<object>>(objectMember);

        var getter = getterLambda.Compile();

        return FromValue(getter());
    }

    public static QueryConstant FromValue(object value)
    {
        var result = new QueryConstant();

        switch (value)
        {
            case string text:
                result.Text = text;
                break;
            case int integer:
                result.Integer = integer;
                break;
            case long @long:
                result.Long = @long;
                break;
            case bool @bool:
                result.Bool = @bool;
                break;
            case null:
                result.Null = true;
                break;
            default:
                throw new InvalidOperationException($"Unable to do action {nameof(FromValue)} for {nameof(QueryConstant)}, not supported type {value.GetType().Name}");
        }

        return result;
    }

    public override string ToString()
    {
        try
        {
            return $"{GetValue()}";
        }
        catch
        {
            return "NULL";
        }
    }
}