using System.Linq.Expressions;
using TimescaleApplication.Specifications.Abstractions;

namespace TimescaleApplication.Specifications.NullSpecification;

public class NullSpecification<T> : Specification<T>
{
    public override Expression<Func<T, bool>> ToExpression() =>
        x => true;
}