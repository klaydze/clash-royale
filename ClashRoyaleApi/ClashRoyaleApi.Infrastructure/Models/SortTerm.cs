using System;
using System.Collections.Generic;
using System.Text;

namespace ClashRoyaleApi.Infrastructure.Models
{
    public class SortTerm
    {
        public string Name { get; set; }
        public string EntityName { get; set; }
        public bool IsDescending { get; set; }
        public bool IsDefault { get; set; }
    }
}
