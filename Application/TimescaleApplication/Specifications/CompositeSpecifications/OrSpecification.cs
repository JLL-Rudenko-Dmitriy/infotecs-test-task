using System.Linq.Expressions;
using TimescaleApplication.Specifications.Abstractions;

namespace TimescaleApplication.Specifications.CompositeSpecifications;

public class OrSpecification<T> : Specification<T>
{
    private readonly Specification<T> _left;
    private readonly Specification<T> _right;

    public OrSpecification(Specification<T> left, Specification<T> right)
    {
        _left = left;
        _right = right;
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        var leftExpr  = _left.ToExpression();
        var rightExpr = _right.ToExpression();
        
        var param = Expression.Parameter(typeof(T));
        
        var leftBody = new ParameterReplacer(leftExpr.Parameters[0],  param)
            .Visit(leftExpr.Body);
        
        var rightBody = new ParameterReplacer(rightExpr.Parameters[0], param)
            .Visit(rightExpr.Body);

        var body = Expression.Or(leftBody, rightBody);
        
        return Expression.Lambda<Func<T,bool>>(body, param);
    }
}