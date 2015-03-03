using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;


namespace DatingHeaven.Entities {
    public abstract class BaseEntity {

        protected BaseEntity(){
            // each entity is hidden from the start
            IsHidden = false;
        }

        /// <summary>
        /// Date when this entity was created
        /// </summary>
        [Required]
        public DateTime CreatedOn{
            get; 
            set; 
        }

        [Required]
        public DateTime ModifiedOn{
            get; 
            set; 
        }

        /// <summary>
        /// Is this entity hidden from the user?
        /// </summary>
        [Required]
        public bool IsHidden {
            get; 
            set; 
        }


        public abstract EntityKey Key {get;}


        public override bool Equals(object obj){
            if (obj == null){
                return false;
            }

            if (ReferenceEquals(this, obj)){
                // the same reference, it's TRUE
                return true;
            }

            if (!(obj is BaseEntity) || GetType() != obj.GetType()){
                // types do not match, then FALSE
                // typeof(Message) != typeof(LogRecord)
                return false;
            }

            var anotherObj = obj as BaseEntity;
            return Key.Equals(anotherObj.Key);
        }
    }
}
