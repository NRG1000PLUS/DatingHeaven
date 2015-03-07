using DatingHeaven.Entities;

namespace DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators{
    public interface IEntitySqlGeneratorsFactory {
        /// <summary>
        /// 
        /// </summary>
        SelectEntitySqlGenerator<TEntity> CreateSelectSqlGenerator<TEntity>() 
            where TEntity : BaseEntity;

        UpdateEntitySqlGenerator<TEntity> CreateUpdateSqlGenerator<TEntity>()
            where TEntity : BaseBusinessEntity;
    }
}