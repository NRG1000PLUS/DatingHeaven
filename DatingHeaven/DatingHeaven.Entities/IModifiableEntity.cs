using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatingHeaven.Entities {
    public interface IModifiableEntity {
        /// <summary>
        /// 
        /// </summary>
        DateTime? ModifiedOn{
            get; 
            set; 
        }
    }
}
