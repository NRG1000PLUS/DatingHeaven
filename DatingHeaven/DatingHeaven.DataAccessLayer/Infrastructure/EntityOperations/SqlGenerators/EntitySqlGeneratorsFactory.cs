using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatingHeaven.Entities;

namespace DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators {
    public class EntitySqlGeneratorsFactory : IEntitySqlGeneratorsFactory{
        private readonly IEntityInfoResolver _entityInfoResolver;

        public EntitySqlGeneratorsFactory(IEntityInfoResolver tableResolver){
            if (tableResolver == null){
                //
                throw new NullReferenceException("<TableResolver> is not provided!");
            }

            _entityInfoResolver = tableResolver;
        }

        public SelectEntitySqlGenerator<TEntity> CreateSelectSqlGenerator<TEntity>() 
                           where TEntity : BaseEntity{
           return new SelectEntitySqlGenerator<TEntity>(
               config: CreateConfig(), 
               tableResolver: _entityInfoResolver);
        }

        private SqlGeneratorConfig CreateConfig(){
            return new SqlGeneratorConfig();
        }


        public UpdateEntitySqlGenerator<TEntity> CreateUpdateSqlGenerator<TEntity>() where TEntity : BaseBusinessEntity {
           return new UpdateEntitySqlGenerator<TEntity>(
              config: CreateConfig(),
              tableResolver:_entityInfoResolver
            );
        }
    }
}
