using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ClashRoyaleApi.Infrastructure.SearchExpressionProviders.Implementations
{
    /// <summary>
    /// Integer search expression provider. It support Equals, LessThan, LessThanEqual, GreaterThan and GreaterThanEqual searching.
    /// </summary>
    public class IntegerSearchExpressionProvider : ComparableSearchExpressionProvider
    {
        public override ConstantExpression GetValue(string value)
        {
            if (!int.TryParse(value, out var intValue))
                throw new ArgumentException("Invalid search value.");

            return Expression.Constant(intValue);
        }
    }
}
