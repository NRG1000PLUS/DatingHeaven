using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using DatingHeaven.DataAccessLayer.Infrastructure;
using DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators;

namespace DatingHeaven.DataAccessLayer {
    public class FluentSyntaxForEntityProperty<T> where T: class{
        private readonly FluentSyntaxForWhereConditions<T> _whereParent; 
        
        public FluentSyntaxForEntityProperty(string propertyName, 
                                            FluentSyntaxForWhereConditions<T> whereParent, 
                                            int paramIndex){
            PropertyName = propertyName;
            ParameterIndex = paramIndex;
            _whereParent = whereParent;
        }

        public string PropertyName{
            get; 
            private set;
        }

        public int ParameterIndex{
            get; 
            private set;
        }

        public FluentSyntaxForWhereConditions<T> Is(object value){
            var condition = CreateCondition(SqlOperator.Equals, value);
            _whereParent.AppendWhereCondition(condition);
            return _whereParent;
        }

        private WhereCondition CreateCondition(SqlOperator sqlOperator, object value){
            return new WhereCondition{
                Column = PropertyName,
                ParameterIndex = ParameterIndex,
                Operator = sqlOperator,
                Value = value
            };
        }


        public FluentSyntaxForWhereConditions<T> IsNot(object propertyValue){
            var condition = CreateCondition(SqlOperator.NotEquals, propertyValue);
            // Where().Property(m => m.Name).NotEquals("Limo");
            _whereParent.AppendWhereCondition(condition);
            return _whereParent;
        }

        public FluentSyntaxForWhereConditions<T> LessThan(object propertyValue){
            var condition = CreateCondition(SqlOperator.LessThan, propertyValue);
             // Where().Property(m => m.Age).LessThan(23);
            _whereParent.AppendWhereCondition(condition);
            return _whereParent;
        }

        public FluentSyntaxForWhereConditions<T> GreaterThan(object propertyValue){
            var condition = CreateCondition(SqlOperator.GreaterThan, propertyValue);
            _whereParent.AppendWhereCondition(condition);
            return _whereParent;
        }


        public FluentSyntaxForWhereConditions<T> IsNull(){
            var condition = CreateCondition(SqlOperator.Is, "NULL");
            _whereParent.AppendWhereCondition(condition);
            return _whereParent;
        }

        public FluentSyntaxForWhereConditions<T> IsNotNull(){
            var condition = CreateCondition(SqlOperator.Is, "NOT NULL");
            _whereParent.AppendWhereCondition(condition);
            return _whereParent;
        } 


    }
}
