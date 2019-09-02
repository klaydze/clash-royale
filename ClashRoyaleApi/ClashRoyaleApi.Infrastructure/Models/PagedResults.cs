using System;
using System.Collections.Generic;
using System.Text;

namespace ClashRoyaleApi.Infrastructure.Models
{
    /// <summary>
    /// This will be used when returning a data that are paginated.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResults<T>
    {
        /// <summary>
        /// List of items return after pagination.
        /// </summary>
        public IEnumerable<T> Items { get; set; }

        /// <summary>
        /// Total count of records from the collection.
        /// </summary>
        public int TotalSize { get; set; }
    }
}
