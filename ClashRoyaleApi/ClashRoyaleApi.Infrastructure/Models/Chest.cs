using ClashRoyaleApi.Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClashRoyaleApi.Infrastructure.Models
{
    public class Chest
    {
        [Sortable]
        public int Id { get; set; }

        public string IdName { get; set; }

        [Sortable]
        public string Name { get; set; }

        public int Version { get; set; }
    }
}
