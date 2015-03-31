using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DatingHeaven.DataAccessLayer.Infrastructure;
using DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations;
using DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators;
using Ninject.Parameters;

namespace DatingHeaven.DataAccessLayer {
    public class FluentSyntaxForWhereConditions<T> where T: class{
        private readonly IPropertySelectionAnalyzer _propertySelectionAnalyzer;
        private readonly List<IWhereCondition> _conditionsList = new List<IWhereCondition>();
        private readonly IDbContext _dbContext;
        private readonly IEntitySqlGeneratorsProvider _sqlGeneratorsProvider;
        private readonly List<LogicalOperation> _logicalOperations = new List<LogicalOperation>();
        private readonly IEntityInfoResolver _entityInfoResolver;
        private readonly Stack<WhereConditionsGroup> _stackConditionGroups = new Stack<WhereConditionsGroup>();
        private          int     _sqlParameterIndex = 0;
        private          bool    _isInsideBlock = false;

        public FluentSyntaxForWhereConditions(IPropertySelectionAnalyzer propertySelectionAnalyzer, 
                                              IDbContext dbContext, 
                                              IEntitySqlGeneratorsProvider sqlGeneratorsFactory, 
                                              IEntityInfoResolver entityInfoResolver){
            _propertySelectionAnalyzer = propertySelectionAnalyzer;
            _dbContext = dbContext;
            _sqlGeneratorsProvider = sqlGeneratorsFactory;
            _entityInfoResolver = entityInfoResolver;
        }



        public FluentSyntaxForEntityProperty<T> Property(Expression<Func<T, object>> propertySelector){
            _propertySelectionAnalyzer.ValidateSelector(propertySelector);
            string propertyName = _propertySelectionAnalyzer.GetPropertyName(propertySelector);
            return new FluentSyntaxForEntityProperty<T>(propertyName, this, _sqlParameterIndex++);
        }

        public FluentSyntaxForEntityProperty<T> Property(string propertyName){
            return new FluentSyntaxForEntityProperty<T>(propertyName, this, _sqlParameterIndex++);
        }


        internal void AppendWhereCondition(WhereCondition whereCondition){
            if (_isInsideBlock){
                var conditionGroup = _stackConditionGroups.Peek();
                conditionGroup.Conditions.Add(whereCondition);
            } else{
                _conditionsList.Add(whereCondition);
            }
        }

        public FluentSyntaxForWhereConditions<T> And(){
            EnsureAnyConditionExists();
            AppendLogicalOperation(LogicalOperation.AND);
            return this;
        }


        private void AppendLogicalOperation(LogicalOperation logicalOperation){
            if (_isInsideBlock){
                var conditionGroup = _stackConditionGroups.Peek();
                conditionGroup.LogicalOperations.Add(logicalOperation);
            } else{
                _logicalOperations.Add(logicalOperation);
            }
        }

        public FluentSyntaxForWhereConditions<T> OpenBlock(){
            var conditionsGroup = new WhereConditionsGroup();

            if (_isInsideBlock && (_stackConditionGroups.Peek() != null)){
                var parentCondition = _stackConditionGroups.Peek();
                parentCondition.Conditions.Add(conditionsGroup);
            }

            _stackConditionGroups.Push(conditionsGroup);
            _isInsideBlock = true;
            return this;
        }

        public FluentSyntaxForWhereConditions<T> CloseBlock(){
            // get the top condition from the stack 
            var conditionGroup = _stackConditionGroups.Pop();
            // add it to the conditions list
            _conditionsList.Add(conditionGroup);

            // change mode
            if (!_stackConditionGroups.Any()){
                // stack is empty
                _isInsideBlock = false;
            }
            return this;
        } 

        public FluentSyntaxForWhereConditions<T> Or(){
            EnsureAnyConditionExists();
            AppendLogicalOperation(LogicalOperation.OR);
            return this;
        } 

        public IList<T> Select(){
            List<T> result = null;
            using (_dbContext){
                var generator = _sqlGeneratorsProvider.CreateSelectGenerator();
                InitializeSelectGenerator(generator);

                try{
                    var query = _dbContext.GetSet<T>().SqlQuery(
                               sql: generator.GenerateSql(),
                               parameters: _sqlGeneratorsProvider.BuildParametersList(generator.WhereConditions));
                    // return the list
                    result = query.ToList();
                } catch (Exception ex){
                    
                }
            }

            return result;
        }

       

        private void InitializeSelectGenerator(SelectEntitySqlGenerator sqlGenerator){
            sqlGenerator.TableName = _entityInfoResolver.GetTableName<T>();
            sqlGenerator.SelectAllColumns = true;
            if (_conditionsList.Any()){
                sqlGenerator.WhereConditions = _conditionsList;
            }

            if (_logicalOperations.Any()){
                sqlGenerator.LogicalOperations = _logicalOperations;
            }
        }

        private void EnsureAnyConditionExists(){
            if (_conditionsList.Count == 0){
                throw new InvalidOperationException(
                    "You cannot use this logical operator without any condition");
            }
        }

    }
}
