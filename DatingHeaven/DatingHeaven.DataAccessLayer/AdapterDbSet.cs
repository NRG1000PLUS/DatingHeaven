using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DatingHeaven.DataAccessLayer.EntityAdapters;
using DatingHeaven.Entities;
using Ninject.Infrastructure.Language;

namespace DatingHeaven.DataAccessLayer {
    public class AdapterDbSet<TEntity>: IQueryable<EntityAdapter<TEntity>> where TEntity: BaseEntity{
        private readonly DbSet<TEntity> _dbSet;       

        public AdapterDbSet(DbSet<TEntity> dbSet){
            _dbSet = dbSet;
        }

        public AdapterDbSet(IQueryProvider queryProvider,
                            Expression expression){
            Expression = expression;
            Provider = queryProvider;
        } 







        public Type ElementType {
            get{
                // Adapter type
                return typeof (BaseEntity);
            }
        }

        /// <summary>
        /// Save LINQ expression and assign the value from constructor
        /// </summary>
        public Expression Expression{
            get; 
            private set; 
        }

        /// <summary>
        /// Save LINQ provider and assign the value from constructor
        /// </summary>
        public IQueryProvider Provider{
            get; 
            private set; 
        }

        public IEnumerator<EntityAdapter<TEntity>> GetEnumerator(){
            return null;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            throw new NotImplementedException();
        }
    }
}
