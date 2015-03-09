using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;

namespace DatingHeaven.Entities {
    public abstract class BaseEntity{
        private EntityKey _entityKey;


        /// <summary>
        /// Get the key for this entity.
        /// Entity key could be of type 'int' for BaseEntity
        /// </summary>
        [NotMapped]
        public EntityKey Key{
            get{
                return _entityKey ?? (_entityKey = GetEntityKey());
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected abstract EntityKey GetEntityKey();

        public override bool Equals(object anotherObject){
            if (anotherObject == null){
                // FALSE since an object cannot equal to NULL
                return false;
            }

            if (ReferenceEquals(this, anotherObject)){
                // the same reference, it's TRUE
                return true;
            }

            if (!(anotherObject is BaseEntity) || (GetType() != anotherObject.GetType())){
                // types do not match, then FALSE
                // typeof(Message) != typeof(LogRecord)
                return false;
            }

            // cast object to BaseEntity
            var baseEntity = (BaseEntity)anotherObject;

            // check keys
            return Key.Equals(baseEntity.Key);
        }
    }
}
