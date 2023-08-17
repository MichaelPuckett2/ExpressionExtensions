using System.Linq.Expressions;

namespace TripleG3.ExpressionExtensions;
public static class Extensions
{
    /// <summary>
    /// Gets the name of the member used in an expression, typically the property name.
    /// </summary>
    /// <param name="lambdaExpression">The La</param>
    /// <returns>Name of member if found. Throws LambdaExpressionMemberNotFoundException</returns
    /// <exception cref="LambdaExpressionMemberNotFoundException">Thrown when <paramref name="lambdaExpression"/> member name cannot be parsed.</exception>
    public static string GetMemberName(this LambdaExpression lambdaExpression)
    {
        string result;
        MemberExpression memberExpression;
        if (lambdaExpression.Body is UnaryExpression unaryExpression
        && unaryExpression.Operand is MemberExpression unaryMemberExpression)
        {
            memberExpression = unaryMemberExpression;
        }
        else if (lambdaExpression.Body is MemberExpression lambdaMemberExpression)
        {
            memberExpression = lambdaMemberExpression;
        }
        else
        {
            throw new LambdaExpressionMemberNotFoundException($"Could not get member name {lambdaExpression.Name ?? string.Empty}");
        }
        result = memberExpression.Member.Name;
        return result;
    }

    public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer,
                                                                           IEnumerable<TInner> inner,
                                                                           Func<TOuter, TKey> outerKeySelector,
                                                                           Func<TInner, TKey> innerKeySelector,
                                                                           Func<TOuter, TInner, TResult> resultSelector,
                                                                           Expression<Func<TKey, TKey, bool>> expression)
        => outer.Join(inner, outerKeySelector, innerKeySelector, resultSelector, (G3EqualityComparer<TKey>)expression);
}
