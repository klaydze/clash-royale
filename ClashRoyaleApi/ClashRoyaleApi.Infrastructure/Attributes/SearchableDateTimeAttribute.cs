using ClashRoyaleApi.Infrastructure.SearchExpressionProviders.Implementations;

namespace ClashRoyaleApi.Infrastructure.Attributes
{
    public class SearchableDateTimeAttribute : SearchableAttribute
    {
        public SearchableDateTimeAttribute()
        {
            ExpressionProvider = new DateTimeSearchExpressionProvider();
        }
    }
}
