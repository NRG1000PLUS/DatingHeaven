using DatingHeaven.Entities;

namespace DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations {
    public interface IEntityTableInfoResolver{
        /// <summary>
        /// Get name of the table by its entity type
        /// </summary>
        string GetTableName<T>() where T : BaseEntity;

        /// <summary>
        /// Get schema of the table by its entity type
        /// </summary>
        string GetTableSchema<T>() where T: BaseEntity;
    }
}
