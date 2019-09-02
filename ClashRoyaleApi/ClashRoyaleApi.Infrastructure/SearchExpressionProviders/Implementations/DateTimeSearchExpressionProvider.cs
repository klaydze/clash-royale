using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ClashRoyaleApi.Infrastructure.SearchExpressionProviders.Implementations
{
    /// <summary>
    /// DateTime search expression provider. It support Equals (eq), LessThan (lt), LessThanEqual (lte), GreaterThan (gt) and GreaterThanEqual (gte) searching.
    /// </summary>
    public class DateTimeSearchExpressionProvider : ComparableSearchExpressionProvider
    {
        public override ConstantExpression GetValue(string value)
        {
            if (!DateTimeOffset.TryParse(value, out var dateTimeValue))
                throw new ArgumentException("Invalid search value.");

            return Expression.Constant(dateTimeValue);
        }
    }
}
