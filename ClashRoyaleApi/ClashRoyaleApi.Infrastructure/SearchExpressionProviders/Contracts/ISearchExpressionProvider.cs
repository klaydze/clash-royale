using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ClashRoyaleApi.Infrastructure.SearchExpressionProviders.Contracts
{
    public interface ISearchExpressionProvider
    {
        /// <summary>
        /// Get the list of valid search and comparable operators.
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetOperators();

        /// <summary>
        /// Get the value to search.
        /// </summary>
        /// <param name="value">The value to be search.</param>
        /// <returns></returns>
        ConstantExpression GetValue(string value);

        /// <summary>
        /// Return a comparison expression.
        /// </summary>
        /// <param name="left">The property to compare.</param>
        /// <param name="searchOperator">The search or comparable operator to use. Default operator is equals "eq".</param>
        /// <param name="right">The value that will be search or compare from the property.</param>
        /// <returns></returns>
        Expression GetComparison(MemberExpression left,
            string searchOperator,
            ConstantExpression right);
    }
}
