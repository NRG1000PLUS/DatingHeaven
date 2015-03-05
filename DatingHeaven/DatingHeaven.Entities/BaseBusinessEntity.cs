﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;


namespace DatingHeaven.Entities {
    public abstract class BaseBusinessEntity: BaseEntity {

        protected BaseBusinessEntity(){
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

    }
}