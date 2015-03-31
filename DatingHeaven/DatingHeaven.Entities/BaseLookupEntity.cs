using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;

namespace DatingHeaven.Entities {
    public abstract class BaseLookupEntity: BaseEntity {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0, TypeName = "integer")]
        public int Id{
            get; 
            set; 
        }


        [Required]
        [MaxLength(500)]
        [Index(IsClustered = false, IsUnique = true)]
        public string Name{
            get; 
            set; 
        }
    }
}
