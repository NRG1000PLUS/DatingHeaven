using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using DatingHeaven.DataAccessLayer.Infrastructure;
using DatingHeaven.Entities;

namespace DatingHeaven.DataAccessLayer {
    public class EfRepository<T>: IRepository<T> where T: BaseBusinessEntity{
        private readonly IDbContext          _dbContext;
        private          DbSet<T>            _set; 
   

        public EfRepository(IDbContext dbContext){
             // save the DB context
            _dbContext = dbContext;
        } 


        public virtual T GetById(object id){
            var dbSet = GetSet();
            var entityKey = id as EntityKey;

            if (entityKey != null){
                // composite key (EntityKey)
                object entityObjectResult = null;
                bool result = _dbContext.ObjectContext.TryGetObjectByKey(entityKey,
                    out entityObjectResult);
                if (result && !((BaseBusinessEntity)entityObjectResult).IsHidden){
                    // return 
                    return (T) entityObjectResult;
                }

                // could not find enityt
                return null;
            } else{
                // Find 
                var entity = dbSet.Find(id);
                return ((entity != null) && !entity.IsHidden)
                    ? entity
                    : null;
            }
        }

        public T GetByIdWithHidden(object id){
            var dbSet = GetSet();
            var result = dbSet.Find(id);
            return result;
        }

        public IList<T> GetAll(){
            var dbSet = GetSet();
            return dbSet.Where(ent => !ent.IsHidden).ToList();
        }

        public IList<T> GetWhere(Func<T, bool> predicate){
            throw new NotImplementedException();
        }

        public IList<T> GetWhereWithHidden(Func<T, bool> predicate){
            throw new NotImplementedException();
        }

        public IList<T> GetAllWithHidden(){
            var dbSet = GetSet();
            return dbSet.ToList();
        }


        public void Hide(object id) {
            throw new NotImplementedException();
        }

        public void Refresh(T entity) {
            throw new NotImplementedException();
        }

        public void Insert(T entity) {
            throw new NotImplementedException();
        }

        protected DbSet<T> GetSet(){
            // save a DbSet to the local reference if needed
            return _set ?? (_set = _dbContext.GetSet<T>());
        }
    }
}
