using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;

namespace DatingHeaven.Entities {
    public abstract class BaseBusinessEntityWithId : BaseBusinessEntity{

        protected BaseBusinessEntityWithId(){
            // ID is always null when being created
            Id = 0;
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id{
            get; 
            set; 
        }



        protected override EntityKey GetEntityKey() {
            var entityKey = new EntityKey();
            var keyIdMember = new EntityKeyMember {
                Key = "Id",
                Value = Id
            };

            entityKey.EntityKeyValues = new[] { keyIdMember /* Only one key*/};
            return entityKey;
        }
    }
}
