using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using DatingHeaven.DataAccessLayer.Services;
using DatingHeaven.Entities.Domain;

namespace DatingHeaven.DataAccessLayer {
    public class Repository<T>: IRepository<T> where T: BaseEntity{
        private readonly IEntityInfoService _entityInfoService;

        public Repository(IEntityInfoService entityInfoService){
            _entityInfoService = entityInfoService;
        } 


        public T GetById(int id){
            using (var dbContext = new DbModelContainer()){
                // get the entity table name by its type
                var tableName = _entityInfoService.GetTableNameByEntityType<T>();

                if (tableName == null){
                    // could not evaluate name of the table
                    return null;
                }

               


                var exp = _entityInfoService.GetExpression(tableName);

                if (exp == null){

                    return null;
                }
            }
            return null;
        }



        public IList<T> GetAll(){
            throw new NotImplementedException();
        }

        public void MarkAsDeleted(int id){
            throw new NotImplementedException();
        }

        public void Refresh(T entity){
            throw new NotImplementedException();
        }

        public void Insert(T entity){
            throw new NotImplementedException();
        }
    }
}
