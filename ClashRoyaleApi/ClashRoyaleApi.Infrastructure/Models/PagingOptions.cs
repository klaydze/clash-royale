﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClashRoyaleApi.Infrastructure.Models
{
    /// <summary>
    /// Options when working with pagination.
    /// </summary>
    public class PagingOptions
    {
        [Range(0, 99999, ErrorMessage = "Offset must be greater than 0.")]
        public int? Offset { get; set; }

        [Range(1, 100, ErrorMessage = "Limit must be greater than 0 and less than 100.")]
        public int? Limit { get; set; }

        public PagingOptions Replace(PagingOptions newer)
        {
            return new PagingOptions
            {
                Offset = newer.Offset ?? this.Offset,
                Limit = newer.Limit ?? this.Limit
            };
        }
    }
}
