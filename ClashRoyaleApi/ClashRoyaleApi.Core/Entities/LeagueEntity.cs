using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClashRoyaleApi.Core.Entities
{
    public class LeagueEntity : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "IdName is required")]
        [StringLength(30)]
        public string IdName { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(30)]
        public string Name { get; set; }

        public int VictoryGold { get; set; }

        public int MinimumTrophies { get; set; }

        public int Version { get; set; }
    }
}
