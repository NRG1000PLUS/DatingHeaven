using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatingHeaven.DataAccessLayer.Infrastructure {
    public class WhereConditionsGroup : IWhereCondition{

        private readonly List<IWhereCondition> _conditions;
        private readonly List<LogicalOperation> _logicalOperations; 

        public WhereConditionsGroup(){
            _conditions = new List<IWhereCondition>();
            _logicalOperations = new List<LogicalOperation>();
        }

        public List<IWhereCondition> Conditions{
            get{
                return _conditions;
            }
        }

        public List<LogicalOperation> LogicalOperations{
            get{
                return _logicalOperations;
            }
        } 
    }
}
