using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatingHeaven.Entities;

namespace DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators {
    public class SqlGeneratorsFactory {
        private readonly IEntityTableInfoResolver _entityTableInfoResolver;

        public SqlGeneratorsFactory(IEntityTableInfoResolver tableResolver){
            if (tableResolver == null){
                //
                throw new NullReferenceException("<TableResolver> is not provided!");
            }

            _entityTableInfoResolver = tableResolver;
        }

        public bool GeneratorsShouldHaveParameterPlaceholdersEnabled{
            get; 
            set; 
        }


        public SelectPropertiesCrudSqlGenerator<TEntity> CreateSelectSqlGenerator<TEntity>() 
                           where TEntity : BaseBusinessEntity{
            var generator = new SelectPropertiesCrudSqlGenerator<TEntity>(_entityTableInfoResolver){
                 ParameterPlaceholdersEnabled = GeneratorsShouldHaveParameterPlaceholdersEnabled
            };

            return generator;
        }
    }
}
