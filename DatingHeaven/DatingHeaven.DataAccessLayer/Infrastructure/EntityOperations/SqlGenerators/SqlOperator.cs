using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators {
    public enum SqlOperator {
        Equals = 0,
        NotEquals,
        GreaterThan,
        LessThan,
        GreaterOrEquals,
        LessOrEquals, 
        Like,
        Is
    }
}
