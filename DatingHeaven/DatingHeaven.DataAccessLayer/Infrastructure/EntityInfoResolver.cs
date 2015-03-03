using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using DatingHeaven.Entities;

namespace DatingHeaven.DataAccessLayer.Infrastructure {
    public class EntityInfoResolver: IEntityInfoResolver{
        private static readonly Dictionary<Type, string> TableNames = new Dictionary<Type, string>(); 

        public string ResolveTableName<T>() where T : BaseEntity {
            if (HasTableName<T>()){
                // table name for type 'T> is known, get it
                return GetTableName<T>();
            } else{
                lock (TableNames){
                    // solve table name
                    SolveTableName<T>();
                    return GetTableName<T>();
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

            TableNames.Add( type, tableAttribute.Name);
        }

        private bool HasTableName<TEntity>() where TEntity : BaseEntity{

            return TableNames.ContainsKey(typeof (TEntity));
        }

        private string GetTableName<TEntity>() where TEntity : BaseEntity{

            return TableNames[typeof (TEntity)];
        }





        public Func<System.Data.Entity.Core.Objects.ObjectContext, object> GetExpression(string tableName) {
            throw new NotImplementedException();
        }
    }
}
