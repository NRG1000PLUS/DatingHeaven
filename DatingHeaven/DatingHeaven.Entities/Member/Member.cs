using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.Xml;

namespace DatingHeaven.Entities.Member {
    [Table("Members")]
    public class Member: BaseBusinessEntityWithId{
        private Profile.Profile _profile;
        private List<Message> _messages; 

        public Member(){

          
        }

        [Required]
        [MaxLength(100)]
        public string NickName{
            get; 
            set; 
        }


        [Required]
        [MaxLength(100)]
        public string FirstName{
            get; 
            set; 
        }


        [Required]
        [MaxLength(100)]
        public string LastName{
            get; 
            set;
        }

        [Required]
        public char Gender{
            get; 
            set; 
        }


        [NotMapped]
        public Gender GenderValue{
            get{
                if (Gender == 'F'){
                     // FEMALE 
                    return Entities.Member.Gender.Female;
                }  else if (Gender == 'M'){
                    // MALE 
                    return Entities.Member.Gender.Male;
                }

                return Entities.Member.Gender.NotDefined;
            }
        }


        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email{
            get; 
            set; 
        }


        public DateTime? LastVisit{
            get; 
            set; 
        }


        public virtual Profile.Profile Profile{
            get{
                return _profile ?? (_profile = new Profile.Profile());
            }
            set{
                _profile = value;
            }
        }

        public IList<Message> Messages{
            get{
                return _messages ?? (_messages = new List<Message>());
            }
            set{
                _messages = value.ToList();
            }
        } 
    }
}
