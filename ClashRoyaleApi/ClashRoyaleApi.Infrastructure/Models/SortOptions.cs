using ClashRoyaleApi.Infrastructure.Processors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ClashRoyaleApi.Infrastructure.Models
{
    public class SortOptions<T, TEntity> : IValidatableObject
    {
        public string[] OrderBy { get; set; }

        // Asp.net core calls this to validate incoming parameters
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var processor = new SortOptionsProcessor<T, TEntity>(OrderBy);

            var validTerms = processor.GetValidSortTerms().Select(x => x.Name);
            var invalidTerms = processor.GetSortTerms().Select(x => x.Name)
                .Except(validTerms, StringComparer.OrdinalIgnoreCase);

            foreach(var term in invalidTerms)
            {
                yield return new ValidationResult(
                    $"Invalid sort term '{term}'", new[] {
                        nameof(OrderBy) });
            }
        }

        /// <summary>
        /// Apply sort options to a database query.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Apply(IQueryable<TEntity> query)
        {
            var processor = new SortOptionsProcessor<T, TEntity>(OrderBy);

            return processor.Apply(query);
        }
    }
}
