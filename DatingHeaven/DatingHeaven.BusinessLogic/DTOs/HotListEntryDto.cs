﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatingHeaven.BusinessLogic.DTOs {
    public class HotListEntryDto : BaseDto {

        public int MemberId{
            get; 
            set; 
        }

        public int TargetId{
            get; 
            set; 
        }
    }
}
