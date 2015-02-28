using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatingHeaven.Entities.Domain;

namespace DatingHeaven.DataAccessLayer {
    public interface IRepository<T> where T: BaseEntity{
        /// <summary>
        /// Get entity by Id
        /// </summary>
        T GetById(int id);

        /// <summary>
        /// Get all entities
        /// </summary>
        IList<T> GetAll();

        /// <summary>
        /// Mark the current entity as deleted
        /// </summary>
        void MarkAsDeleted(int id);

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
