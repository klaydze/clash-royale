using ClashRoyaleApi.Infrastructure.SearchExpressionProviders.Implementations;
using System;

namespace ClashRoyaleApi.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SearchableStringAttribute : SearchableAttribute
    {
        public SearchableStringAttribute()
        {
            ExpressionProvider = new StringSearchExpressionProvider();
        }
    }
}
