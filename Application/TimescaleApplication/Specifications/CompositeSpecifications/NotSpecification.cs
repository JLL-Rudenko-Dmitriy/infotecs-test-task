using System.Linq.Expressions;
using TimescaleApplication.Specifications.Abstractions;

namespace TimescaleApplication.Specifications.CompositeSpecifications;

public class NotSpecification<T> : Specification<T>
{
    private readonly Specification<T> _expression;

    public NotSpecification(Specification<T> expression)
    {
        _expression = expression;
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        var expression = _expression.ToExpression();
        
        var param = Expression.Parameter(typeof(T));
        
        var exprBody = new ParameterReplacer(expression.Parameters[0],  param)
            .Visit(expression.Body);
        
        var body = Expression.Not(expression);
        
        return Expression.Lambda<Func<T,bool>>(body, param);
    }
}