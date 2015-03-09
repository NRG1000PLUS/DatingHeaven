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
    public class AdapterDbSet<TEntity, TAdapter>: DbSet<TAdapter>, IQueryable<TAdapter>, IEnumerable<TAdapter> where TEntity: BaseEntity where TAdapter: class, IEntityAdapter, new(){
        private readonly DbSet<TEntity> _dbSet;
        private readonly EntityEnumerator<TAdapter, TEntity> _enumerator; 

        

        public AdapterDbSet(DbSet<TEntity> dbSet){
            _dbSet = dbSet;
            _enumerator = new EntityEnumerator<TAdapter, TEntity>(dbSet);
        }

        public AdapterDbSet(IQueryProvider queryProvider,
                            Expression expression){
            Expression = expression;
            Provider = queryProvider;
        } 







        public Type ElementType {
            get{
                // Adapter type
                return typeof (TAdapter);
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
    }
}
