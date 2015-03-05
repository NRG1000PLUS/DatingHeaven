using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Data.Entity.Core;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators;
using DatingHeaven.Entities;
using System.Data.Entity;
using DatingHeaven.Entities.Geo;


namespace DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations {
    public class EntityOperationsProvider : IEntityOperationsProvider{
        private readonly IEntityTableInfoResolver          _entityTableResolver;
        private readonly IDbContext                        _dbContext;
        private readonly SqlGeneratorsFactory              _sqlGeneratorsFactory;
        private readonly EntityOperationsProviderConfig    _config;
        private readonly IEntityPropertiesAnalyzer         _entityPropertiesAnalyzer;
   

        public EntityOperationsProvider(IEntityTableInfoResolver   entityTableNameResolver, 
              //                         IEntityPropertiesAnalyzer   entityPropertiesAnalyzer,
                                  EntityOperationsProviderConfig   config,
                                                      IDbContext   dbContext,
                                            SqlGeneratorsFactory   sqlGeneratorsFactory){
            _entityTableResolver = entityTableNameResolver;
            _dbContext = dbContext;   // dbContext //
            _sqlGeneratorsFactory = sqlGeneratorsFactory;
            _config = config;
            //_entityPropertiesAnalyzer = entityPropertiesAnalyzer;

            _sqlGeneratorsFactory.GeneratorsShouldHaveParameterPlaceholdersEnabled = _config.SqlParameterizationEnabled;
        }

        public virtual object GetProperty<T>(object entityKey, string entityProperty) where T : BaseBusinessEntity{
            var sqlGenForSelect = _sqlGeneratorsFactory.CreateSelectSqlGenerator<T>();

            // initialize the generator
            sqlGenForSelect.Key = entityKey;
            sqlGenForSelect.SingleProperty = entityProperty;
            sqlGenForSelect.ParameterPlaceholdersEnabled = false;

          

            //ObjectParameter parameter = new ObjectParameter("p0", entityId);
            var querySelectProperty = _dbContext.Database.SqlQuery(
                 // TODO: Finish with EntityProviderAnalyzer 
                 elementType: _entityPropertiesAnalyzer.GetPropertyType<T>(entityProperty),
                 sql: sqlGenForSelect.GenerateSql(),
                 parameters: new object[] { /* empty */ }
            );

            //return the value
            var enumerator = querySelectProperty.GetEnumerator();
           
            if (enumerator.MoveNext()){
                // we've got the value
                return enumerator.Current;
            }

            return null;
        }

        private object[] GetParametersFromEntityKey(object entityKey){
            object[] result = null;
           
            if (entityKey is int){
                result = new object[]{
                    new SqlParameter("@p0", (int)entityKey)
                };  
            } else if (entityKey is EntityKey){
                result = ((EntityKey) entityKey).EntityKeyValues.
                    Select(m => m.Value).
                    ToArray();
            }

            return result;
        }


      

     
        public T SearchByProperty<T>(Func<T, object> propertySelector, object propertyValue) where T : Entities.BaseBusinessEntity{
            var dbSet = _dbContext.GetSet<T>();
             // dbSet.AsNoTracking().
            return dbSet.FirstOrDefault(ent => propertySelector(ent).Equals(propertyValue));
        }

        

        public void SetProperty<T>(object entityId, string property, object value) where T: BaseBusinessEntity{
            var updateSqlQuery = new UpdatePropertySqlQueryGenerator{
                Key = entityId,
                UpdatedProperty = property,
                PropertyValue = value,
                TableName = _entityTableResolver.GetTableName<T>()
            };

            _dbContext.GetSet<T>().SqlQuery(updateSqlQuery.GenerateSql(), entityId);
        }
    }
}
