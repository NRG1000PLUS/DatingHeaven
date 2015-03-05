using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DatingHeaven.Entities;

namespace DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations {
    public class EntityTableInfoResolver: IEntityTableInfoResolver{
        private static readonly Dictionary<Type, TableAttribute> TableNames = new Dictionary<Type, TableAttribute>(); 

        public string GetTableName<T>() where T : BaseEntity {
            if (HasTableName<T>()){
                // table name for type 'T> is known, get it
                return TableNames[typeof (T)].Name;
            } else{
                lock (TableNames){
                    if (!HasTableName<T>()){
                        // solve table name
                        SolveTableName<T>();
                    }

                    return TableNames[typeof (T)].Name;
                }
            }
        }

        private void SolveTableName<T>(){
            var type = typeof (T);
            var attributes = type.GetCustomAttributes(attributeType: typeof(TableAttribute),
                                                            inherit: false);

            if (attributes.Length < 1){
                // no TableAttribute was found in the current entity type
                throw new Exception("<TableAttribute> is not defined for type: " + type);
            }

            var tableAttribute = (attributes[0] as TableAttribute);

            TableNames.Add( type, tableAttribute);
        }

        private bool HasTableName<TEntity>(){

            return TableNames.ContainsKey(typeof (TEntity));
        }



        public string GetTableSchema<T>() where T : BaseEntity {
            throw new NotImplementedException();
        }
    }
}
