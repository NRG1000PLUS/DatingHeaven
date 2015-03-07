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
    public class EfRepository<T>: IRepository<T> where T: BaseEntity{
        private readonly IDbContext                _dbContext;
        private readonly IEntityContextProvider    _entityContext;
        private          DbSet<T>                  _set; 
   

        public EfRepository(IDbContext dbContext,
                            IEntityContextProvider entityContextProvider){
             // save the DB context
            _dbContext = dbContext;
            _entityContext = entityContextProvider;
        } 


        public virtual T GetById(object entityKey){
            var dbSet = GetSet();

            

            return default(T);
        }

        public T GetByIdWithHidden(object id){
            var dbSet = GetSet();
            var result = dbSet.Find(id);
            return result;
        }

        public IList<T> GetAll(){
            var dbSet = GetSet();
            return null;
        }

        public IList<T> GetWhere(Func<T, bool> predicate){
            var dbSet = GetSet();
            return dbSet.Where(predicate).ToList();
        }

        public IList<T> GetWhereWithHidden(Func<T, bool> predicate){
            throw new NotImplementedException();
        }

        public IList<T> GetAllWithHidden(){
            var dbSet = GetSet();
            return dbSet.ToList();
        }


        public void Hide(object entityKey){
             /*
              * User has decided to delete an entity, but the current DB does not delete anuthing,
              * it simply marks entities as HIDDEN
              */ 
           // _entityContext.SetPropertyValue<T>(entityKey: entityKey,
           //                                    propertySelector: ent => ent.IsHidden,
           //                                    value: true);

        }

        public void Refresh(T entity){
            var dbSet = GetSet();
            dbSet.Attach(entity);

            _dbContext.SaveChanges();
        }

        public void Insert(T entity){
            var dbSet = GetSet();
            dbSet.Add(entity);
            _dbContext.SaveChanges();
        }

        protected DbSet<T> GetSet(){
            // save a DbSet to the local reference if needed
            return _set ?? (_set = _dbContext.GetSet<T>());
        }
    }
}
