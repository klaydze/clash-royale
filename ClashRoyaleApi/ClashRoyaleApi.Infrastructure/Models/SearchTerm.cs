using ClashRoyaleApi.Infrastructure.SearchExpressionProviders.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClashRoyaleApi.Infrastructure.Models
{
    public class SearchTerm
    {
        public string Name { get; set; }

        public string EntityName { get; set; }

        public string Operator { get; set; }

        public string Value { get; set; }

        public bool ValidSyntax { get; set; }

        public ISearchExpressionProvider ExpressionProvider { get; set; }
    }
}
