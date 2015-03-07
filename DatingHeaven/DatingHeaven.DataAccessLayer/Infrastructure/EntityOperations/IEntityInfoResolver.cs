using System;
using DatingHeaven.Entities;

namespace DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations {
    public interface IEntityInfoResolver{
        /// <summary>
        /// Get name of the table by its entity type
        /// </summary>
        string GetTableName<T>() where T : BaseEntity;

        /// <summary>
        /// Get schema of the table by its entity type
        /// </summary>
        string GetTableSchema<T>() where T: BaseEntity;

        /// <summary>
        /// Get type of the entity property using its name
        /// </summary>
        Type GetPropertyType<T>(string property) where T : BaseEntity;


        /// <summary>
        /// Get the Key property of entity, having [KeyAttribute] 
        /// </summary>
        string GetEntityKeyProperty<T>() where T : BaseEntity;
    }
}
