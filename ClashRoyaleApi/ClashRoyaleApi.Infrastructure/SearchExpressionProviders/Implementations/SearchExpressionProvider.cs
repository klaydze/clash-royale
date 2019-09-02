using ClashRoyaleApi.Infrastructure.SearchExpressionProviders.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ClashRoyaleApi.Infrastructure.SearchExpressionProviders.Implementations
{
    /// <summary>
    /// Default search expression provider.
    /// </summary>
    public class SearchExpressionProvider : ISearchExpressionProvider
    {
        protected const string EqualsOperator = "eq";

        public virtual Expression GetComparison(MemberExpression left, string searchOperator, ConstantExpression right)
        {
            switch (searchOperator.ToLower())
            {
                case EqualsOperator:
                    return Expression.Equal(left, right);
                default:
                    throw new ArgumentException($"Invalid operator '{searchOperator}'.");
            }
        }

        public virtual IEnumerable<string> GetOperators()
        {
            yield return EqualsOperator;
        }

        public virtual ConstantExpression GetValue(string value)
            => Expression.Constant(value);
    }
}
