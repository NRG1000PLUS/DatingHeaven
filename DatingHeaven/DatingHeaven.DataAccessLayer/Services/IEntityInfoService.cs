using System;
using System.Data.Objects;
using DatingHeaven.Entities.Domain;

namespace DatingHeaven.DataAccessLayer.Services {
    public interface IEntityInfoService{
        /// <summary>
        /// Get name of the table by its entity type
        /// </summary>
        string GetTableNameByEntityType<T>() where T : BaseEntity;


        Func<ObjectContext, object> GetExpression(string tableName);
    }
}
