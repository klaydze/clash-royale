using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClashRoyaleApi.Core.Entities
{
    public class ChestEntity : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "IdName is required")]
        public string IdName { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public int Version { get; set; }
    }
}
