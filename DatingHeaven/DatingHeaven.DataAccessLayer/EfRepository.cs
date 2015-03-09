using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using DatingHeaven.DataAccessLayer.EntityAdapters;
using DatingHeaven.DataAccessLayer.Infrastructure;
using DatingHeaven.Entities;

namespace DatingHeaven.DataAccessLayer {
    public class EfRepository<T>: IRepository<T> where T: BaseEntity{
        private readonly IEntityContextProvider    _entityContext;
   

        public EfRepository(IEntityContextProvider entityContextProvider){
            _entityContext = entityContextProvider;
        } 

        public virtual T GetById(object entityKey){
            T result = null;


            using (var dbContext = new DatingHeavenDbContext()){
                if (TypesHelper.IsBaseBusinessEntityWithId<T>()){
                     //is [BaseBusinessEntityWithId] entity 
                    //int entityId = (int) entityKey;
                    //var query = from ent in dbContext.AdapterSet<T, BaseBusinessEntityWidthIdAdapter<T>>()
                    //            where (ent.IsHidden == false) && (ent.Id == entityId)
                    //            select ent;

                    //var entityAdapter = query.FirstOrDefault();

                    //if (entityAdapter != null){
                    //    // return the Entity instance in the current adapter
                    //    result = (T) entityAdapter.Entity;
                    //}
                }  else if (TypesHelper.IsBaseBusinessEntity<T>()){
                    //todo: CHECK IF THE KEY IS COMPOSITE HERE
                    if (entityKey is EntityKey){
                        // THE KEY IS COMPOSITE
                        // bbbdff
                        //ffffff
                        //ffffff
                        object entity;
                        var isEntityFound = false;
                        isEntityFound = dbContext.ObjectContext.TryGetObjectByKey(
                              (EntityKey)entityKey,
                              out entity
                            );

                        if (isEntityFound){
                            result = (T) entity;
                        }
                    } else{
                        
                    }


                } else if (TypesHelper.IsBaseLookupEntity<T>()){
                    // Is BaseLookupEntity
                    var lookupQuery = from ent in dbContext.AdapterSet<T, BaseLookupEntityAdapter>()
                                      where ent.Id == (int)entityKey
                                      select ent;
                }
            } // end/dispose DbContext

            return result;
        }

        private int GetEntityId(T entity){
            if (!(entity is BaseBusinessEntityWithId)){
                throw new ArgumentException("must have an ID~");
            }

            return ((BaseBusinessEntityWithId) ((object) entity)).Id;
        }

        private IQueryable GetSelectQuery<T>(object entityKey, DatingHeavenDbContext dbContext) where T : BaseEntity{
            if (TypesHelper.IsBaseBusinessEntityWithId(typeof (T))){
                return    from ent in dbContext.Set<T>().AsNoTracking().Cast<BaseBusinessEntityWithId>()
                          where (ent.Id == (int) entityKey) && (ent.IsHidden == false)
                          select ent;
            } else if (TypesHelper.IsBaseBusinessEntity(typeof (T))){
                return from ent in dbContext.Set<T>().AsNoTracking().Cast<BaseBusinessEntity>()
                    where ent.IsHidden == false && ent.Equals(entityKey)
                    select ent;
            }
            return null;
        }


        
        public IList<T> GetAll(){
            using (var dbContext = new DatingHeavenDbContext()){
                var q = from ent in dbContext.AdapterSet<T, BaseEntityAdapter>()
                    where ent.IsHidden == false
                    select ent;
            }

            return null;
        }

        /*
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
        */
    }
}
