using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace DatingHeaven.DataAccessLayer.Services {
    class EntityInfoService: IEntityInfoService{
        private static Dictionary<Type, string> _dictionary; 

        public string GetTableNameByEntityType<T>() where T : Entities.Domain.BaseEntity {
            if (_dictionary.ContainsKey(typeof (T))){
                return _dictionary[typeof (T)];
            }
            return null;
        }


        public Func<System.Data.Objects.ObjectContext, object> GetExpression(string tableName) {
            throw new NotImplementedException();
        }
    }
}
