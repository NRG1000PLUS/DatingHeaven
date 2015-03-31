using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatingHeaven.DataAccessLayer {
    public interface IDbContextProvider{
        /// <summary>
        /// Create a DB context
        /// </summary>
        IDbContext CreateContext();
    }
}
