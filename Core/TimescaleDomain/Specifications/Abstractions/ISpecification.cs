using System.Linq.Expressions;

namespace TimescaleDomain.Specifications.Abstractions;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> ToExpression();
}