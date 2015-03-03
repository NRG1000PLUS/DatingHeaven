using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;

namespace DatingHeaven.Entities {
    public abstract class BaseEntityWithId : BaseEntity{

        private EntityKey _entityKey;

        protected BaseEntityWithId(){
            // ID is always null when being created
            Id = 0;
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id{
            get; 
            set; 
        }

        [NotMapped]
        public override EntityKey Key {
            get{

                if (_entityKey == null){
                    var entityKey = new EntityKey();
                    var keyIdMember = new EntityKeyMember{
                        Key = "Id",
                        Value = Id
                    };

                    entityKey.EntityKeyValues = new[]{ keyIdMember /* Only one key*/};
                    _entityKey = entityKey;
                }

                return _entityKey;
            }
        }
    }
}
