using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using DatingHeaven.DataAccessLayer.EntityAdapters;
using DatingHeaven.DataAccessLayer.Infrastructure;
using DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations;
using DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators;
using DatingHeaven.Entities;

namespace DatingHeaven.DataAccessLayer {
    public class EfRepository<T>: IRepository<T> where T: BaseEntity{
        private readonly IEntityInfoResolver              _entityInfoResolver;
        private readonly IEntitySqlGeneratorsProvider     _sqlGeneratorsProvider;
        private readonly IPropertySelectionAnalyzer      _propertySelectionAnalyzer;
        private readonly IDbContextProvider              _dbContextProvider;

        public EfRepository(IEntitySqlGeneratorsProvider sqlGeneratorsFactory, 
                            IPropertySelectionAnalyzer propertySelectionAnalyzer, 
                            IDbContextProvider dbContextProvider, 
                            IEntityInfoResolver entityInfoResolver){
            _sqlGeneratorsProvider = sqlGeneratorsFactory;
            _propertySelectionAnalyzer = propertySelectionAnalyzer;
            _dbContextProvider = dbContextProvider;
            _entityInfoResolver = entityInfoResolver;
        }


        public T GetById(object entityKey){
            T result = null;

            using (var dbContext = _dbContextProvider.CreateContext()){
                try{
                    result = dbContext.GetSet<T>().Find(entityKey);
                } catch (InvalidOperationException ex){
                        // log error here
                        throw;
                }
            }
            

            return result;
        }
  
        public IList<T> GetAll(){
            using (var dbContext = _dbContextProvider.CreateContext()){
                var query = from ent in dbContext.GetSet<T>().AsNoTracking()
                            select ent;

                IList<T> result;

                try{
                    result = query.ToList();
                } catch (Exception ex){

                    throw;
                }

                return result;
            }
        }

     

        public IList<T> GetWhere(Expression<Func<T, object>> propertySelector, object propertyValue){
            // validate the current selector
            _propertySelectionAnalyzer.ValidateSelector<T>(propertySelector);

            // get the entity property name from the current selector
            string propertyName = _propertySelectionAnalyzer.GetPropertyName<T>(propertySelector);
 
            // call the internal method that invokes calls to SQL source
            return GetWhereInternal(propertyName, SqlOperator.Equals, propertyValue);
        }

        
                          
        public IList<T> GetWhere(Expression<Func<T, object>> propertySelector, 
                                     SqlOperator sqlOperator, 
                                     object propertyValue){
            _propertySelectionAnalyzer.ValidateSelector(propertySelector);
            
            // get property name of entity using a selector expression
            string propertyName = _propertySelectionAnalyzer.GetPropertyName(propertySelector);

            return GetWhereInternal(propertyName, sqlOperator, propertyValue);
        }

        

        private IList<T> GetWhereInternal(string propertyName, 
                                          SqlOperator sqlOperator, 
                                          object propertyValue){
            IList<T> result = null;

            using (var dbContext = _dbContextProvider.CreateContext()) {
                SelectEntitySqlGenerator sqlGenerator = _sqlGeneratorsProvider.CreateSelectGenerator();

                // initialize the query generator
                InitializeSqlGenerator(sqlGenerator, propertyName, sqlOperator, propertyValue);

                try{
                    var query = dbContext.GetSet<T>().SqlQuery(
                        sql: sqlGenerator.GenerateSql(),
                        parameters: _sqlGeneratorsProvider.BuildParametersList(sqlGenerator.WhereConditions.OfType<WhereCondition>()));
                    result = query.ToList();
                } catch (Exception) {

                    throw;
                }
            }

            return result;
        }

        private void InitializeSqlGenerator(SelectEntitySqlGenerator sqlGenerator,
                string propertyName,
                SqlOperator sqlOperator,
                object propertyValue){
            sqlGenerator.SelectAllColumns = true;
            sqlGenerator.TableName = _entityInfoResolver.GetTableName<T>();

            var whereCondition = new WhereCondition{
                Column = propertyName,
                Operator = sqlOperator,
                Value = propertyValue
            };

            sqlGenerator.WhereConditions.Add(whereCondition);
        }

        public IList<T> GetWhere(string property, object propertyValue){
            return GetWhereInternal(property, SqlOperator.Equals, propertyValue);
        }

        public void Refresh(T entity) {
            using (var dbContext = _dbContextProvider.CreateContext()){
                if (dbContext.Entry(entity).State == EntityState.Detached){
                    dbContext.GetSet<T>().Attach(entity);
                    dbContext.Entry(entity).State = EntityState.Modified;
                }
                dbContext.SaveChanges();
            }
        }

        public void Insert(T entity) {
            using (var dbContext = _dbContextProvider.CreateContext()){
                dbContext.GetSet<T>().Add(entity);

                try{
                    dbContext.SaveChanges();
                } catch (Exception ex){
                    
                }
            }
        }




        public void Delete(T entity) {
            using (var dbContext = _dbContextProvider.CreateContext()){

                if (dbContext.Entry(entity).State == EntityState.Detached){
                      // attach entity to the context with UNCHANGED state
                      dbContext.GetSet<T>().Attach(entity);
                      // mark the entity as DELETED in the context
                      dbContext.Entry(entity).State = EntityState.Deleted;
                }

                try{
                    dbContext.SaveChanges();
                } catch (Exception ex){
                    // handle error here
                    throw;
                }
            }
        }


        public void DeleteById(object entityKey){
           //var generator =  _sqlGeneratorsFactory.CreateDeleteGenerator<T>();
           //InitializeGenerator(generator);
        }


        public int GetCount(){
            int entitiesCount = 0;

            using (var db = _dbContextProvider.CreateContext()){
                try{
                    entitiesCount =  db.GetSet<T>().Count();
                } catch (Exception ex){
                    // log error
                    throw;
                }
            }

            return entitiesCount;
        }

        public int GetCountWhere(Expression<Func<T, bool>> predicate){
            int entitiesCount = 0;

            using (var dbContext = _dbContextProvider.CreateContext()){
                try{
                    entitiesCount = dbContext.GetSet<T>().Count(predicate);
                } catch (Exception ex){
                    
                }
            }

            return entitiesCount;
        }


        public IList<T> GetWhere(string property, SqlOperator sqlOperator, object propertyValue){
            return GetWhereInternal(property, sqlOperator, propertyValue);
        }


        public FluentSyntaxForWhereConditions<T> Where() {
            return new FluentSyntaxForWhereConditions<T>(
                              propertySelectionAnalyzer: _propertySelectionAnalyzer,
                              dbContext: _dbContextProvider.CreateContext(),
                              sqlGeneratorsFactory: _sqlGeneratorsProvider,
                              entityInfoResolver: _entityInfoResolver
            );
        }
    }
}
