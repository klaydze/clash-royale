using ClashRoyaleApi.Infrastructure.SearchExpressionProviders.Implementations;
using System;

namespace ClashRoyaleApi.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SearchableIntegerAttribute : SearchableAttribute
    {
        public SearchableIntegerAttribute()
        {
            ExpressionProvider = new IntegerSearchExpressionProvider();
        }
    }
}
