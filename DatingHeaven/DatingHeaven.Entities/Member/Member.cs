using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatingHeaven.Entities.Member {
    [Table("Members")]
    public class Member: BaseEntityWithId{
        private Profile _profile;

        public Member(){

            // gender is not defined at start
            this.Gender = '\0';
        }

        [Required]
        [MaxLength(100)]
        public string NickName{
            get; 
            set; 
        }


        [Required]
        public string FirstName{
            get; 
            set; 
        }


        [Required]
        public string LastName{
            get; 
            set;
        }

        [Required]
        [DefaultValue(null)]
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



        [Required]
        public DateTime LastVisit{
            get; 
            set; 
        }


        public Profile Profile{
            get{
                return _profile ?? (_profile = new Profile());
            }
            set{
                _profile = value;
            }
        }



    }
}
