using System;
using System.Collections.Generic;
using DatingHeaven.Entities;

namespace DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators{
    public interface IEntitySqlGeneratorsProvider{
        /// <summary>
        /// 
        /// </summary>
        SelectEntitySqlGenerator CreateSelectGenerator();

        UpdateEntitySqlGenerator CreateUpdateGenerator();

        DeleteEntitySqlGenerator CreateDeleteGenerator();

        object[] BuildParametersList(IEnumerable<IWhereConditionRoot> whereConditions);
    }
}