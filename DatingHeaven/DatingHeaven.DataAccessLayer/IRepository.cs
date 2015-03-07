using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatingHeaven.Entities;

namespace DatingHeaven.DataAccessLayer {
    public interface IRepository<T> where T: BaseEntity{
        /// <summary>
        /// Get entity by Id
        /// </summary>
        T GetById(object entityKey);

        /// <summary>
        /// 
        /// </summary>
        T GetByIdWithHidden(object entityKey);

        /// <summary>
        /// Get all entities
        /// </summary>
        IList<T> GetAll();

        /// <summary>
        /// Get entities filtered by the WHERE predicate
        /// </summary>
        IList<T> GetWhere(Func<T, bool> predicate);

        /// <summary>
        /// 
        /// </summary>
        IList<T> GetWhereWithHidden(Func<T, bool> predicate);

        /// <summary>
        /// Admin method to get all entities, including the HIDDEN/DELETED ones
        /// </summary>
        /// <returns></returns>
        IList<T> GetAllWithHidden(); 

        /// <summary>
        /// Mark the current entity as DELETED
        /// </summary>
        void Hide(object id);

        /// <summary>
        /// Update the current entity
        /// </summary>
        void Refresh(T entity);

        /// <summary>
        /// Insert entity to the table 
        /// </summary>
        void Insert(T entity);
    }
}
