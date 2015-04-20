using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations;
using DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators;
using DatingHeaven.DataAccessLayer.Repositories;
using Ninject.Modules;

namespace DatingHeaven.DataAccessLayer {
    public class DalNInjectModule: NinjectModule {
        public override void Load(){
            // always create a new repository
            Bind(typeof (IRepository<>)).To(typeof(EfSingleIdRepository<>));
            Bind<IEntitySqlGeneratorsProvider>().To<EntitySqlGeneratorsProvider>();
            Bind<IDbContextProvider>().To<DbContextProvider>();
            Bind<IEntityPropertySelectionAnalyzer>().To<EntityPropertySelectionAnalyzer>();
            Bind<IEntityInfoResolver>().To<EntityInfoResolver>();
        }
    }
}
