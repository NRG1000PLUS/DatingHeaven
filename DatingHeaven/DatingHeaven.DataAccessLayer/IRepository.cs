﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DatingHeaven.DataAccessLayer.FluentSyntax;
using DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators;
using DatingHeaven.Entities;

namespace DatingHeaven.DataAccessLayer {
    public interface IRepository<T> where T: BaseEntity{
        /// <summary>
        /// Set an existing context
        /// </summary>
        void SetContext(IDbContext dbContext);

        /// <summary>
        /// Get entity by keys
        /// </summary>
        T GetById(object entityKey);

        /// <summary>
        ///  Get entity along with included properties
        /// </summary>
        T GetByIdInclude(object entityKey, params Expression<Func<T, object>>[] includedProperties);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertySelector"></param>
        /// <param name="propertyValue"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        IList<T> GetWhereInclude(Expression<Func<T, object>> propertySelector, object propertyValue,
                                  params Expression<Func<T, object>>[] includeProperties); 
            
        /// <summary>
        /// Get all entities in the table
        /// </summary>
        IList<T> GetAll();

        /// <summary>
        /// Get total amount of entities stored in the table
        /// </summary>
        int GetCount();

        /// <summary>
        /// Get count of entities that fit the current filter
        /// </summary>
        int GetCountWhere(Expression<Func<T, bool>> propertySelector);

        /// <summary>
        /// Get entities using a filter
        /// </summary>
        IList<T> GetWhere(Expression<Func<T, object>> propertySelector, object propertyValue);

        /// <summary>
        /// 
        /// </summary>
        IList<T> GetWhere(Expression<Func<T, object>> propertySelector, Comparison comparison, object propertyValue); 

        /// <summary>
        /// Get entities using a filter with EQUALS operator
        /// </summary>
        IList<T> GetWhere(string propertyName, object propertyValue);

        /// <summary>
        /// Fluent syntax for filtering
        ///  _repo.Where().Property(m => m.SenderId).Equals(32).And().
        ///                Property(m => m.IsRead).Equals(false).And().
        ///                Property(m => m.ReadOn).IsNull().Select()
        /// 
        ///  _repo.Where().Property(m => m.Login).Equals("KinkyLover007").And().
        ///                Property(m => m.IsTrial).Equals(false).Delete();
        /// 
        ///  _repo.Where().Property( m => m.Login).Equals("AwesomeBoy").Update( m => m.FirstName, "James");
        /// </summary>
        FluentSyntaxForWhereConditions<T> Where();

        /// <summary>
        /// Get entities using a filter
        /// </summary>
        IList<T> GetWhere(string propertyName, Comparison comparison, object propertyValue); 

        /// <summary>
        /// Update the current entity
        /// </summary>
        void Refresh(T entity);

        /// <summary>
        /// Insert a new entity to the table 
        /// </summary>
        void Insert(T entity);

        /// <summary>
        /// Delete entity
        /// </summary>
        void Delete(T entity);

        /// <summary>
        /// Delete entity by its key
        /// </summary>
        void DeleteById(object entityKey);

        /// <summary>
        /// Get the entity table 
        /// </summary>
        IQueryable<T> Table { get; } 
    }
}
