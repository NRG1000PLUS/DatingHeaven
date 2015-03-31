using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;

namespace DatingHeaven.Entities.Geo {
    [Table("Countries")]
    public class Country: BaseLookupEntity {

        [Required]
        [MaxLength(10)]
        public string Code{
            get; 
            set; 
        }
        
    }
}
