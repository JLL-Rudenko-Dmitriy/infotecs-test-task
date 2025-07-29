using System.Linq.Expressions;
using TimescaleApplication.Specifications.CompositeSpecifications;
using TimescaleDomain.Specifications.Abstractions;

namespace TimescaleApplication.Specifications.Abstractions;

public abstract class Specification<T> : ISpecification<T>
{
    public abstract Expression<Func<T, bool>> ToExpression();
    
    public Specification<T> And(Specification<T> left, Specification<T> right) => new AndSpecification<T>(left, right);
    public Specification<T> Or(Specification<T> left, Specification<T> right) => new OrSpecification<T>(left, right);
    public Specification<T> Not(Specification<T> expression) => new NotSpecification<T>(expression);
    
    internal sealed class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _target;
        private readonly ParameterExpression _replacement;

        public ParameterReplacer(ParameterExpression target, ParameterExpression replacement)
        {
            _target = target;
            _replacement = replacement;
        }

        protected override Expression VisitParameter(ParameterExpression node)
            => node == _target ? _replacement : base.VisitParameter(node);
    }
}