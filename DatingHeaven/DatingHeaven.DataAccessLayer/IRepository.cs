using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatingHeaven.Entities;

namespace DatingHeaven.DataAccessLayer {
    public interface IRepository<T> where T: BaseBusinessEntity{
        /// <summary>
        /// Get entity by Id
        /// </summary>
        T GetById(object id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetByIdWithHidden(object id);

        /// <summary>
        /// Get all entities
        /// </summary>
        IList<T> GetAll();


        IList<T> GetWhere(Func<T, bool> predicate);


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
