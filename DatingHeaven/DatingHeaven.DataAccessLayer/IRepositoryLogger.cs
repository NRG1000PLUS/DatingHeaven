﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Server;

namespace DatingHeaven.DataAccessLayer {
    public interface IRepositoryLogger{
        /// <summary>
        /// 
        /// </summary>
        void LogFailedSelect(string msg);
    }
}
