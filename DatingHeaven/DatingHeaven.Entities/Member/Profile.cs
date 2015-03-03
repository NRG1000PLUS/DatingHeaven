﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;

namespace DatingHeaven.Entities.Member {
    [Table("Profiles")]
    public class Profile : BaseEntity{
        private EntityKey _entityKey;

        [Key]
        public int MemberId{
            get; 
            set; 
        }

        [Required]
        public int CountryId{
            get; 
            set; 
        }

        [Required]
        public int CityId{
            get; 
            set; 
        }


        public string CurrentStatus{
            get; 
            set; 
        }


        public override EntityKey Key{
            get{
                if (_entityKey == null){
                    var entityKey = new EntityKey(
                        qualifiedEntitySetName: "DatingHeavenDbContext.Profiles",
                                       keyName: "MemberId", 
                                      keyValue: MemberId);
                    _entityKey = entityKey;
                }

                return _entityKey;
            }
        }
    }
}