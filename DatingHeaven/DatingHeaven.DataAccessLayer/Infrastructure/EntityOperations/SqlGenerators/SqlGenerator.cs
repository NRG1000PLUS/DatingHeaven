using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators {
    public abstract class SqlGenerator{

        public bool ParameterPlaceholdersEnabled{
            get; 
            set; 
        }
        

        public abstract string GenerateSql();
    }
}
