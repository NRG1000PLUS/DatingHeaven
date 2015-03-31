using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DatingHeaven.Entities.Profile {
    [Table("Profiles")]
    public class Profile : BaseBusinessEntity{
        private List<ProfileItem> _profileItems;
        
            
        [Key, ForeignKey("Member")]
        public int MemberId{
            get; 
            set; 
        }


        public DateTime? DateOfBirth{
            get; 
            set; 
        }

        #region === GEO PROPERTIES ===

        public int? CountryId{
            get; 
            set; 
        }

        public int? RegionId{
            get; 
            set; 
        }

        public int? CityId{
            get; 
            set; 
        }

        #endregion

        public Member.Member Member{
            get; 
            set; 
        }
    }
}
