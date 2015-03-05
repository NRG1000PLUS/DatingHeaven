using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using DatingHeaven.Entities;

namespace DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators {
    class UpdatePropertiesCrudSqlGenerator<T>: EntityCrudSqlGenerator<T> where T: BaseBusinessEntity {


        public UpdatePropertiesCrudSqlGenerator(IEntityTableInfoResolver tableResolver) : base(tableResolver){
            
        }

        protected override void GenerateSqlClauseInternal(StringBuilder sb ){
            sb.Append("UPDATE ");
            sb.Append(base.TableInfoResolver.GetTableName<T>());
        }
    }
}
