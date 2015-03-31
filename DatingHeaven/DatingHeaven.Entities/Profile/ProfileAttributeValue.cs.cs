using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DatingHeaven.Entities.Profile {
    [Table("ProfileAttributeValues")]
    public class ProfileAttributeValue : BaseBusinessEntityWithId {

        /// <summary>
        /// Foreign key to the 'ProfileAttributes' table
        /// </summary>
        [Required]
        public int ProfileAttributeId{
            get; 
            set; 
        }

        [Required]
        public int TypeId{
            get; 
            set; 
        }


        public int? IntValue{
            get; 
            set; 
        }

        public char? CharValue{
            get; 
            set; 
        }

        [MaxLength(50)]
        public string StringValue{
            get; 
            set; 
        }

        [MaxLength(100)]
        public string String100Value{
            get; 
            set; 
        }

        public byte[] BinaryValue{
            get; 
            set; 
        }

        [NotMapped]
        public ProfileAttributeValueType Type{
            get{
                return (ProfileAttributeValueType) TypeId;
            }
        }
    }
}
                                         