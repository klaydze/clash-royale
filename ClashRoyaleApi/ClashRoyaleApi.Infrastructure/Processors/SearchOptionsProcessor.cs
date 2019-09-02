using ClashRoyaleApi.Infrastructure.Attributes;
using ClashRoyaleApi.Infrastructure.Helpers;
using ClashRoyaleApi.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ClashRoyaleApi.Infrastructure.Processors
{
    public class SearchOptionsProcessor<T, TEntity>
    {
        private readonly string[] _searchQuery;

        public SearchOptionsProcessor(string[] search)
        {
            _searchQuery = search;
        }

        /// <summary>
        /// Get all the search terms pass from the endpoint as query string and return as deserialized object.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SearchTerm> GetSearchTerms()
        {
            if (_searchQuery == null)
                yield break;

            foreach(var expression in _searchQuery)
            {
                if (string.IsNullOrEmpty(expression))
                    continue;

                // expression value looks like:
                // "fieldName opetor value"
                var tokens = expression.Split(' ');

                if (tokens.Length == 0)
                {
                    yield return new SearchTerm
                    {
                        ValidSyntax = false,
                        Name = expression
                    };

                    continue;
                }

                if (tokens.Length < 3)
                {
                    yield return new SearchTerm
                    {
                        ValidSyntax = false,
                        Name = tokens[0]
                    };

                    continue;
                }

                yield return new SearchTerm
                {
                    ValidSyntax = true,
                    Name = tokens[0],
                    Operator = tokens[1],
                    Value = string.Join(" ", tokens.Skip(2))
                };
            }
        }

        /// <summary>
        /// Get all valid properties that are searchable.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SearchTerm> GetValidSearchTerms()
        {
            var querySearchTerms = GetSearchTerms()
                .Where(x => x.ValidSyntax)
                .ToArray();

            if (!querySearchTerms.Any())
                yield break;

            var searchableFields = GetSearchablePropertiesFromModel();

            foreach(var term in querySearchTerms)
            {
                var searchTerm = searchableFields
                    .SingleOrDefault(x => x.Name.Equals(term.Name, StringComparison.OrdinalIgnoreCase));

                if (searchTerm == null)
                    continue;

                yield return new SearchTerm
                {
                    ValidSyntax = term.ValidSyntax,
                    Name = searchTerm.Name,
                    EntityName = searchTerm.EntityName,
                    Operator = term.Operator,
                    Value = term.Value,
                    ExpressionProvider = searchTerm.ExpressionProvider
                };
            }
        }

        /// <summary>
        /// Apply the given search.
        /// </summary>
        /// <param name="query">The base query where the search will apply.</param>
        /// <returns></returns>
        public IQueryable<TEntity> Apply(IQueryable<TEntity> query)
        {
            var validSearchTerms = GetValidSearchTerms().ToArray();

            if (!validSearchTerms.Any())
                return query;

            var modifiedQuery = query;

            foreach(var searchTerm in validSearchTerms)
            {
                var propertyInfo = ExpressionHelper
                    .GetPropertyInfo<TEntity>(searchTerm.EntityName ?? searchTerm.Name);

                var obj = ExpressionHelper.Parameter<TEntity>();

                // Build up the LINQ expression backwards:
                // query = query.Where(x => x.Property == "Value");

                // x.Property
                var left = ExpressionHelper.GetPropertyExpression(obj, propertyInfo);

                // "Value"
                var right = searchTerm.ExpressionProvider.GetValue(searchTerm.Value);

                // x.Property == "Value"
                var comparisonExpression = searchTerm.ExpressionProvider
                    .GetComparison(left, searchTerm.Operator, right);

                // x => x.Property == "Value"
                var lambdaExpression = ExpressionHelper
                    .GetLambda<TEntity, bool>(obj, comparisonExpression);

                // query = query.Where...
                modifiedQuery = ExpressionHelper.CallWhere(modifiedQuery, lambdaExpression);
            }

            return modifiedQuery;
        }

        /// <summary>
        /// Get all the property from the model that is searchable by checking if there is a [Searchable] attribute.
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<SearchTerm> GetSearchablePropertiesFromModel()
            => typeof(T).GetTypeInfo()
            .DeclaredProperties
            .Where(p => p.GetCustomAttributes<SearchableAttribute>().Any())
            .Select(p =>
            {
                var attr = p.GetCustomAttribute<SearchableAttribute>();

                return new SearchTerm
                {
                    Name = p.Name,
                    EntityName = attr.EntityProperty,
                    ExpressionProvider = attr.ExpressionProvider
                };
            });
    }
}
