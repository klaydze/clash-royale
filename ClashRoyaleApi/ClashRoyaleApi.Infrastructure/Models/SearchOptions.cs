using ClashRoyaleApi.Infrastructure.Processors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ClashRoyaleApi.Infrastructure.Models
{
    public class SearchOptions<T, TEntity> : IValidatableObject
    {
        public string[] Search { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var processor = new SearchOptionsProcessor<T, TEntity>(Search);

            var validSearchTerms = processor.GetValidSearchTerms()
                .Select(x => x.Name);

            var invalidSearchTerms = processor.GetSearchTerms()
                .Select(x => x.Name)
                .Except(validSearchTerms, StringComparer.OrdinalIgnoreCase);

            foreach(var invalidSearchTerm in invalidSearchTerms)
            {
                yield return new ValidationResult(
                    $"Invalid search term '{invalidSearchTerm}'.",
                    new[] { nameof(Search) });
            }
        }

        public IQueryable<TEntity> Apply(IQueryable<TEntity> query)
        {
            var processor = new SearchOptionsProcessor<T, TEntity>(Search);

            return processor.Apply(query);
        }
    }
}
