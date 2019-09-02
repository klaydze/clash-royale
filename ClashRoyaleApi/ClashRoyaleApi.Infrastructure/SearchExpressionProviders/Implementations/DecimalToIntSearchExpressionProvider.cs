using System;
using System.Linq.Expressions;

namespace ClashRoyaleApi.Infrastructure.SearchExpressionProviders.Implementations
{
    /// <summary>
    /// Decimal search expression provider. It support Equals (eq), LessThan (lt), LessThanEqual (lte), GreaterThan (gt) and GreaterThanEqual (gte) searching.
    /// </summary>
    public class DecimalToIntSearchExpressionProvider : ComparableSearchExpressionProvider
    {
        public override ConstantExpression GetValue(string value)
        {
            if (!decimal.TryParse(value, out var decimalValue))
                throw new ArgumentException("Invalid search value.");

            var places = BitConverter.GetBytes(decimal.GetBits(decimalValue)[3])[2];

            if (places < 2)
                places = 2;

            var justDigits = (int)(decimalValue * (decimal)Math.Pow(10, places));

            return Expression.Constant(justDigits);
        }
    }
}
