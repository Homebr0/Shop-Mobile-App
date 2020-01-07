using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Data.Entities
{
    public class Country : IEntity
    {
        public int Id { get ; set ; }

        [MaxLength(50, ErrorMessage ="The field {0} may only contain {1} characters.")]
        [Required]
        [Display(Name = "Country")]
        public string Name { get; set; }
    }
}
