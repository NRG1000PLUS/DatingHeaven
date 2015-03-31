using DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators;

namespace DatingHeaven.DataAccessLayer.Infrastructure{
    public class WhereCondition : IWhereCondition{
        public string Column{
            get; 
            set;
        }

        public SqlOperator Operator{
            get; 
            set;
        }

        public object Value{
            get; 
            set; 
        }

        public int ParameterIndex{
            get; 
            set; 
        }
    }
}
    