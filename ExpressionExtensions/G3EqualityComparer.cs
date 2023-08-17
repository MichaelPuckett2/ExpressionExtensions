using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace TripleG3.ExpressionExtensions;

public class G3EqualityComparer<T> : IEqualityComparer<T>
{
    private readonly Func<T?, T?, bool> equals;
    private Func<int> getHashCode;

    public G3EqualityComparer(Func<T?, T?, bool> equals) : this(equals, () => 0) { }
    public G3EqualityComparer(Func<T?, T?, bool> equals, Func<int> getHashCode)
    {
        this.equals = equals;
        this.getHashCode = getHashCode;
    }

    public bool Equals(T? x, T? y) => equals.Invoke(x, y);
    public int GetHashCode([DisallowNull] T obj) => getHashCode.Invoke();
    public static implicit operator G3EqualityComparer<T>(Func<T?, T?, bool> equals) => new(equals);

    public static implicit operator G3EqualityComparer<T>(LambdaExpression expression)
    {
        if (expression.Type == typeof(Func<T?, T?, bool>))
        {
            return new((Func<T?, T?, bool>)expression.Compile());
        }
        else
        {
            return new((a, b) => a is null ? b is null : a.Equals(b));
        }
    }
}
