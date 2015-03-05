using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;

namespace DatingHeaven.Entities {
    public abstract class BaseLookupEntity: BaseEntity {

        [Key]
        public int Id{
            get; 
            set; 
        }




        protected override EntityKey GetEntityKey() {
            var entityKey = new EntityKey();

            // create a single key [Id]
            var keyIdMember = new EntityKeyMember {
                Key = "Id",
                Value = Id
            };

            entityKey.EntityKeyValues = new[] { keyIdMember /* Only one key*/};
            return entityKey;
        }
    }
}
