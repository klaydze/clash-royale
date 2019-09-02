using ClashRoyaleApi.Infrastructure.Attributes;
using ClashRoyaleApi.Infrastructure.Helpers;
using ClashRoyaleApi.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ClashRoyaleApi.Infrastructure.Processors
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public class SortOptionsProcessor<T, TEntity>
    {
        private readonly string[] _orderBy;

        public SortOptionsProcessor(string[] orderBy)
        {
            _orderBy = orderBy;
        }

        /// <summary>
        /// Get all the sort terms pass from the endpoint as query string and return as deserialized object.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SortTerm> GetSortTerms()
        {
            // If the client didn't give us an orderby term then yield break
            if (_orderBy == null)
                yield break;

            foreach(var term in _orderBy)
            {
                if (string.IsNullOrEmpty(term))
                    continue;

                // Get the field and sort type to sort
                // E.g "[orderBy]=[name] [desc]"
                var tokens = term.Split(' ');

                if (tokens.Length == 0)
                {
                    yield return new SortTerm { Name = term };
                    continue;
                }

                var isDescending = tokens.Length > 1 && 
                                        tokens[1].Equals("desc", StringComparison.OrdinalIgnoreCase);

                yield return new SortTerm
                {
                    Name = tokens[0],
                    IsDescending = isDescending
                };
            }
        }

        /// <summary>
        /// Get all the valid properties that are sortable.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SortTerm> GetValidSortTerms()
        {
            var sortTerms = GetSortTerms().ToArray();

            if (!sortTerms.Any())
                yield break;

            var sortableFieldNames = GetSortablePropertiesFromModel();

            foreach(var term in sortTerms)
            {
                var sortableField = sortableFieldNames
                    .SingleOrDefault(x => x.Name.Equals(term.Name, StringComparison.OrdinalIgnoreCase));

                if (sortableField == null)
                    continue;

                yield return new SortTerm
                {
                    Name = sortableField.Name,
                    EntityName = sortableField.EntityName,
                    IsDescending = term.IsDescending,
                    IsDefault = sortableField.IsDefault
                };
            }
        }

        /// <summary>
        /// Apply the given sorting.
        /// </summary>
        /// <param name="query">The base query where the sorting will apply.</param>
        /// <returns></returns>
        public IQueryable<TEntity> Apply(IQueryable<TEntity> query)
        {
            var validSolidTerms = GetValidSortTerms().ToArray();

            if (!validSolidTerms.Any())
            {
                validSolidTerms = GetSortablePropertiesFromModel()
                    .Where(t => t.IsDefault).ToArray();
            }

            if (!validSolidTerms.Any())
                return query;

            var modifiedQuery = query;
            var useThenBy = false;

            foreach(var sortTerm in validSolidTerms)
            {
                var propertyInfo = 
                    ExpressionHelper.GetPropertyInfo<TEntity>(sortTerm.EntityName ?? sortTerm.Name);

                var obj = ExpressionHelper.Parameter<TEntity>();

                // Build the LINQ expression backwards:
                // query = query.OrderBy(x => x.Property);

                // x => x.Property
                var key = ExpressionHelper
                        .GetPropertyExpression(obj, propertyInfo);
                var keySelector = ExpressionHelper
                        .GetLambda(typeof(TEntity), propertyInfo.PropertyType, obj, key);

                // query.OrderBy/ThenBy[Descending](x => x.Property)
                modifiedQuery = ExpressionHelper
                        .CallOrderByOrThenBy(modifiedQuery, useThenBy, sortTerm.IsDescending, propertyInfo.PropertyType, keySelector);

                useThenBy = true;
            }

            return modifiedQuery;
        }

        /// <summary>
        /// Get all the property from the model that is sortable by checking if there is a [Sortable] attribute.
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<SortTerm> GetSortablePropertiesFromModel()
            => typeof(T).GetTypeInfo()
            .DeclaredProperties
            .Where(p => p.GetCustomAttributes<SortableAttribute>().Any())
            .Select(prop => new SortTerm
            {
                Name = prop.Name,
                EntityName = prop.GetCustomAttribute<SortableAttribute>().EntityProperty,
                IsDefault = prop.GetCustomAttribute<SortableAttribute>().IsDefault
            });
    }
}
