using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Ninject.Activation;

namespace DatingHeaven.Entities.Profile {
    [Table("ProfileAttributes")]
    public class ProfileAttribute : BaseLookupEntity {

        public ProfileAttribute(){
            HasMultipleValues = false;
        }

        [Required]
        public bool HasMultipleValues{
            get; 
            set; 
        }

        [ForeignKey("ProfileAttributeId")]
        public IList<ProfileAttributeValue> Values{
            get; 
            set; 
        } 
    }
}
