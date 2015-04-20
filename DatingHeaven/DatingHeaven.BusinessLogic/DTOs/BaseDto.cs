using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatingHeaven.BusinessLogic.DTOs {
    public abstract class BaseDto {

        public bool IsSuccess{
            get; 
            set;
        }

        public string Error{
            get; 
            set; 
        }
    }
}
