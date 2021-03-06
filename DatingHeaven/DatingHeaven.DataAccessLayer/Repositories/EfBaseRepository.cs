﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DatingHeaven.DataAccessLayer.FluentSyntax;
using DatingHeaven.DataAccessLayer.Infrastructure;
using DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations;
using DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators;
using DatingHeaven.Entities;

namespace DatingHeaven.DataAccessLayer.Repositories {
    public abstract class EfBaseRepository<T>: IRepository<T> where T: BaseEntity{
        private readonly IEntityInfoResolver              _entityInfoResolver;
        private readonly IEntitySqlGeneratorsProvider     _sqlGeneratorsProvider;
        private readonly IEntityPropertySelectionAnalyzer _propertySelectionAnalyzer;
        protected readonly IDbContextProvider             dbContextProvider;
        protected        IDbContext                       externalDbContext;

        protected EfBaseRepository(IEntitySqlGeneratorsProvider sqlGeneratorsFactory, 
                            IEntityPropertySelectionAnalyzer propertySelectionAnalyzer, 
                            IDbContextProvider dbContextProviderParameter, 
                            IEntityInfoResolver entityInfoResolver){
            _sqlGeneratorsProvider = sqlGeneratorsFactory;
            _propertySelectionAnalyzer = propertySelectionAnalyzer;
            dbContextProvider = dbContextProviderParameter;
            _entityInfoResolver = entityInfoResolver;
        }

        public T GetById(object entityKey){
            T result = null;

            if (externalDbContext != null){
                result = Invoke_GetById(entityKey, externalDbContext);
            } else{
                using (var dbContext = dbContextProvider.CreateContext()){
                    result = Invoke_GetById(entityKey, dbContext);
                }
            }
           

            return result;
        }
  
        public IList<T> GetAll(){
            IList<T> result = null;

            if (externalDbContext != null){
                // get all entities using the external DbContext 
                 result = Invoke_GetAll(externalDbContext);
            } else{
               using (var dbContext = dbContextProvider.CreateContext()){
                 // get all entities using a new DbContext
                 result = Invoke_GetAll(dbContext);
               }
            }

            return result;
        }

        private IList<T> Invoke_GetAll(IDbContext dbContext){
            IList<T> entitiesList = null;

            var query = from entity in dbContext.Set<T>().AsNoTracking()
                        select entity;
            try{
                entitiesList = query.ToList();
            } catch (Exception ex){
                throw;
            }

            return entitiesList;
        }

        private T Invoke_GetById(object entityKey, IDbContext dbContext){
            T entity = null;
            object[] keys = (entityKey is object[])
                ? (object[]) entityKey: 
                   new[]{ entityKey };

            try{
                entity = dbContext.Set<T>().Find(keys);
            } catch (Exception ex){

                throw;
            }

            return entity;
        }

     

        public IList<T> GetWhere(Expression<Func<T, object>> propertySelector, object propertyValue){
            // validate the current selector
            _propertySelectionAnalyzer.ValidateSelector<T>(propertySelector);

            // get the entity property name from the current selector
            string propertyName = _propertySelectionAnalyzer.GetPropertyName<T>(propertySelector);
 
            // call the internal method that invokes calls to SQL source
            return GetWhereInternal(propertyName, Comparison.Equals, propertyValue);
        }

        
                          
        public IList<T> GetWhere(Expression<Func<T, object>> propertySelector, 
                                     Comparison comparison, 
                                     object propertyValue){
            _propertySelectionAnalyzer.ValidateSelector(propertySelector);
            
            // get property name of entity using a selector expression
            string propertyName = _propertySelectionAnalyzer.GetPropertyName(propertySelector);

            return GetWhereInternal(propertyName, comparison, propertyValue);
        }

        

        private IList<T> GetWhereInternal(string propertyName, 
                                          Comparison comparison, 
                                          object propertyValue){
            IList<T> result = null;

            
                SelectEntitySqlGenerator sqlGenerator = _sqlGeneratorsProvider.CreateSelectGenerator();

                // initialize the query generator
                InitializeSqlGenerator(sqlGenerator, propertyName, comparison, propertyValue);

            using (var dbContext = dbContextProvider.CreateContext()){
                try{
                    var query = dbContext.Set<T>().SqlQuery(
                        sql: sqlGenerator.GenerateSql(),
                        parameters: _sqlGeneratorsProvider.BuildParametersList(sqlGenerator.WhereConditions));
                    result = query.ToList();
                } catch (Exception){

                    throw;
                }
            }


            return result;
        }

        private void InitializeSqlGenerator(SelectEntitySqlGenerator sqlGenerator,
                string propertyName,
                Comparison comparison,
                object propertyValue){
            sqlGenerator.SelectAllColumns = true;
            sqlGenerator.TableName = _entityInfoResolver.GetTableName<T>();

            var whereCondition = new WhereCondition{
                Column = propertyName,
                Operator = comparison,
                Value = propertyValue
            };

            sqlGenerator.WhereConditions.Add(whereCondition);
        }

        public IList<T> GetWhere(string property, object propertyValue){
            return GetWhereInternal(property, Comparison.Equals, propertyValue);
        }

        public void Refresh(T entity) {
            using (var dbContext = dbContextProvider.CreateContext()){
                dbContext.Set<T>().Attach(entity);
                dbContext.Entry(entity).State = EntityState.Modified;
              
                try{
                    dbContext.SaveChanges();
                } catch (Exception ex){

                    throw;
                }
            }
        }

        public void Insert(T entity) {
            if (externalDbContext != null){
                Invoke_Insert(entity, externalDbContext);
            } else{
                using (var dbContext = dbContextProvider.CreateContext()){
                    Invoke_Insert(entity, dbContext);
                    try{
                        dbContext.SaveChanges();
                    } catch (Exception ex){

                        throw;
                    }
                }
            }
        }

        private void Invoke_Insert(T entity, IDbContext dbContext){
            if (dbContext.Entry(entity).State == EntityState.Detached){
                dbContext.Set<T>().Add(entity);
            } else{
                dbContext.Entry(entity).State = EntityState.Added;
            }
        }




        public void Delete(T entity) {
            using (var dbContext = dbContextProvider.CreateContext()){

                // attach entity to the context with UNCHANGED state
                dbContext.Set<T>().Attach(entity);
                // mark the entity as DELETED in the context
                dbContext.Entry(entity).State = EntityState.Deleted;

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

            using (var dbContext = dbContextProvider.CreateContext()){
                try{
                    entitiesCount = dbContext.Set<T>().Count();
                } catch (Exception ex){

                    throw;
                }
            }

            return entitiesCount;
        }

        public int GetCountWhere(Expression<Func<T, bool>> predicate){
            int entitiesCount = 0;

            if (externalDbContext != null){
                entitiesCount = externalDbContext.Set<T>().Count(predicate);
            } else{
                using (var dbContext = dbContextProvider.CreateContext()){
                    try{
                        // get the count of entities that match the current predicate
                        entitiesCount = dbContext.Set<T>().Count(predicate);
                    } catch (Exception ex){

                        throw;
                    }
                }
            }


            return entitiesCount;
        }


        public IList<T> GetWhere(string property, Comparison comparison, object propertyValue){
            return GetWhereInternal(property, comparison, propertyValue);
        }


        public FluentSyntaxForWhereConditions<T> Where() {
            return new FluentSyntaxForWhereConditions<T>(
                              propertySelectionAnalyzer: _propertySelectionAnalyzer,
                              dbContext: dbContextProvider.CreateContext(),
                              sqlGeneratorsFactory: _sqlGeneratorsProvider,
                              entityInfoResolver: _entityInfoResolver
            );
        }


        public T GetByIdInclude(object entityKey, Expression<Func<T, object>> propertySelector){
            T entity = null;
           
            
            
            
            return entity;
        }

      

        public IQueryable<T> Table {
            get{
                if (externalDbContext == null){
                    const string errorMsg = "Context is not defined for this repostory.";
                    throw new InvalidOperationException(errorMsg);
                }

                return externalDbContext.Set<T>();
            }
        }




        public abstract T GetByIdInclude(object entityKey, params Expression<Func<T, object>>[] includedProperties);

        public void SetContext(IDbContext dbContext) {
            if (dbContext == null){
                throw new NullReferenceException("dbContext");
            }

            externalDbContext = dbContext;
        }


        protected bool HasAnyIncludedProperty(params Expression<Func<T, object>>[] includedProperties){
            return (includedProperties != null) && 
                   (includedProperties.Length > 0);
        }


        public abstract IList<T> GetWhereInclude
            (Expression<Func<T, object>> propertySelector, object propertyValue,
                params Expression<Func<T, object>>[] includeProperties);
    }
}
