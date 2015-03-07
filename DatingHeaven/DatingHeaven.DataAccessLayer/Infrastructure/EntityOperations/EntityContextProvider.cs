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
using System.Reflection;
using DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators;
using DatingHeaven.Entities;
using System.Data.Entity;
using DatingHeaven.Entities.Geo;


namespace DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations {
    public class EntityContextProvider : IEntityContextProvider{
        private readonly IEntityInfoResolver                     _entityInfoResolver;
        private readonly IDbContext                              _dbContext;
        private readonly IEntitySqlGeneratorsFactory              _sqlGeneratorsFactory;

        public EntityContextProvider(              IEntityInfoResolver   entityInfoResolver,                       
                                                            IDbContext   dbContext,
                                            IEntitySqlGeneratorsFactory   sqlGeneratorsFactory){
            _entityInfoResolver = entityInfoResolver;
            _dbContext = dbContext;   // dbContext //
            _sqlGeneratorsFactory = sqlGeneratorsFactory;
        }

        public virtual object GetPropertyValue<T>(object entityKey, string entityProperty) where T : BaseEntity{
            EnsureEntityKeyIsValid(entityKey);
            var sqlGenForSelect = _sqlGeneratorsFactory.CreateSelectSqlGenerator<T>();

            // initialize the generator
            sqlGenForSelect.Key = entityKey;
            sqlGenForSelect.SelectedProperties.Add(entityProperty);
            

            var querySelectProperty = _dbContext.Database.SqlQuery(
                // TODO: Finish with EntityProviderAnalyzer 
                 elementType: _entityInfoResolver.GetPropertyType<T>(entityProperty),
                 sql: sqlGenForSelect.GenerateSql(),
                 parameters: MakeParametersFromEntityKey(entityKey)
            );
           

            //return the value
            var enumerator = querySelectProperty.GetEnumerator();
            return enumerator.MoveNext() ? enumerator.Current : null;
        }

     

        private object[] MakeParametersFromEntityKey(object entityKey){
            object[] result = null;
           
            if (entityKey is int){
                result = new object[]{
                   entityKey
                };  
            } else if (entityKey is EntityKey){
                result = ((EntityKey) entityKey).EntityKeyValues.
                    Select(m => m.Value).
                    ToArray();
            }

            return result ?? new object[0];
        }


      

     
        public T SearchByProperty<T>(Func<T, object> propertySelector, object propertyValue) where T : BaseBusinessEntity{
            var dbSet = _dbContext.GetSet<T>();
             // dbSet.AsNoTracking().
            return dbSet.FirstOrDefault(ent => propertySelector(ent).Equals(propertyValue));
        }

        

        public bool SetPropertyValue<T>(object entityKey, string property, object value) where T: BaseBusinessEntity{
            EnsureEntityKeyIsValid(entityKey);

            var updateEntityGen = _sqlGeneratorsFactory.CreateUpdateSqlGenerator<T>();
            updateEntityGen.Key = entityKey;
            updateEntityGen.Set(property, value);

            var result = (_dbContext.Database.ExecuteSqlCommand(
                              sql: updateEntityGen.GenerateSql(),
                              parameters: MakeParametersFromEntityKey(entityKey)) > 0      
                        );

            if (result){
                  // update the entity in DbContext
                 _dbContext.UpdateEntityInContext<T>(entityKey, property, value);
            }

            return result;
        }


        public object GetPropertyValue<T>(object entityKey, Expression<Func<T, object>> propertySelector) where T: BaseEntity{
            EnsureEntityKeyIsValid(entityKey);
            // ensure the entity property is selected
            EnsureIsPropertyExpression(propertySelector);

            // get the property name
            var propertyName = GetPropertyNameFromExpression(propertySelector);
            return GetPropertyValue<T>(entityKey, propertyName);
        }


        public bool SetPropertyValue<T>(object entityKey, Expression<Func<T, object>> propertySelector, object value) where T : BaseBusinessEntity {
            EnsureIsPropertyExpression(propertySelector);
            var propertyName = GetPropertyNameFromExpression(propertySelector);

            return SetPropertyValue<T>(entityKey, propertyName, value);
        }


        public T GetEntityWithProperties<T>(object entityKey, IEnumerable<string> selectProperties) where T : BaseEntity {
            throw new NotImplementedException();
        }


        private string GetPropertyNameFromExpression<T>(Expression<Func<T, object>> exp){
            var memberExpression = (MemberExpression) exp.Body;
            return memberExpression.Member.Name;
        }

        private void EnsureIsPropertyExpression<T>(Expression<Func<T, object>> expression){
            var memberExp = expression.Body as MemberExpression;
            if ((memberExp == null) || (memberExp.Member.MemberType != MemberTypes.Property)) {
                // No property selected! We need the entity PROPERTY only
                throw new InvalidOperationException("You did not specify a property!");
            }  
        }


        private void EnsureEntityKeyIsValid(object entityKey){
            if (entityKey == null){
                //throw
                throw new NullReferenceException("entityKey");
            }

            var result = (entityKey is EntityKey) ||
                         (entityKey is int) ||
                         (entityKey is string);
            if (!result){
                //
                throw new ArgumentException("Entity Key must be of type: [int, string, EntityKey]");
            }
        }
    }
}
