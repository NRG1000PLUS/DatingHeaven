using System;
using System.Linq;

namespace DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations {
    public class EntityContextProvider : IEntityOperationsProvider{
        private readonly IEntityInfoResolver _entityInfoResolver;
        private readonly IDbContext _dbContext;

        private const string SQL_SELECT_PROPERTY_VALUE = @"SELECT [@p0]
                                                           FROM [@p1]
                                                           WHERE [Id] = @p2";

        public EntityContextProvider(IEntityInfoResolver entityInfoResolver, 
                                     IDbContext dbContext){
            _entityInfoResolver = entityInfoResolver;
            _dbContext = dbContext;
        }

        public object GetProperty<T>(int entityId, string property) where T : Entities.BaseEntity{
            var q = _dbContext.Database.SqlQuery<object>(SQL_SELECT_PROPERTY_VALUE,
                property,
                _entityInfoResolver.ResolveTableName<T>(),
                entityId);
            return q.FirstOrDefault();
        }

        public bool SetPropery<T>(int entityId, string property, object value) {
            throw new NotImplementedException();
        }


        public T GetEntityByProperty<T>(Func<T, object> propertySelector, object propertyValue) where T : Entities.BaseEntity{
            var dbSet = _dbContext.GetSet<T>();
            return dbSet.FirstOrDefault(ent => propertySelector(ent).Equals(propertyValue));
        }

        public object GetProperty<T>(object entityId, string property) where T : Entities.BaseEntity {
            throw new NotImplementedException();
        }

        public void SetPropery<T>(object entityId, string property, object value) {
            throw new NotImplementedException();
        }
    }
}
