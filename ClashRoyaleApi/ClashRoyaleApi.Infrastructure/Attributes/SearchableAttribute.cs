using ClashRoyaleApi.Infrastructure.SearchExpressionProviders.Contracts;
using System;

namespace ClashRoyaleApi.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SearchableAttribute : Attribute
    {
        public string EntityProperty { get; set; }

        public ISearchExpressionProvider ExpressionProvider { get; set; }
            = new ClashRoyaleApi.Infrastructure.SearchExpressionProviders.Implementations.SearchExpressionProvider();
    }
}
