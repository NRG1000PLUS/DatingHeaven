using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Data.Entity;
using DatingHeaven.DataAccessLayer.EntityAdapters;
using DatingHeaven.DataAccessLayer.Infrastructure;
using DatingHeaven.DataAccessLayer.Mapping;
using DatingHeaven.DataAccessLayer.Mapping.Geo;
using DatingHeaven.Entities;
using DatingHeaven.Entities.Geo;
using DatingHeaven.Entities.Members;
using DatingHeaven.Entities.Profile;


namespace DatingHeaven.DataAccessLayer {
    public class DatingHeavenDbContext : DbContext, IDbContext{
        public DatingHeavenDbContext(){
           // enable the Lazy Loading
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder){
           // register entity types
           RegisterEntityTypeConfigurations(modelBuilder);

           // fix error with DateTime conversion to the DB type
           HandleDateTimeError(modelBuilder); 
        }

        private void HandleDateTimeError(DbModelBuilder modelBuilder){
            modelBuilder.Properties<DateTime>().Configure(
                config => config.HasColumnType("datetime2").
                                 HasPrecision(0)
            );
        }

        private void RegisterEntityTypeConfigurations(DbModelBuilder modelBuilder){
            var entityConfigTypes = (from type in typeof (BaseMap<>).Assembly.GetTypes()
                where type.IsClass && !type.IsAbstract &&
                      (type.BaseType != null) &&
                      type.BaseType.IsGenericType &&
                      (type.BaseType.GetGenericTypeDefinition() == typeof (BaseMap<>) ||
                       type.BaseType.GetGenericTypeDefinition() == typeof (BaseBusinessEntityWithIdMap<>))
                select type).ToList();

            entityConfigTypes.ForEach(entConfigType => {
                dynamic entityConfigInstance = Activator.CreateInstance(entConfigType);
                modelBuilder.Configurations.Add(entityConfigInstance);
            });
        }

        public DbSet<T> Set<T>() where T : class {
            return base.Set<T>();
        }


        /// <summary>
        /// Get ObjectContext from the current DbContext
        /// </summary>
        public ObjectContext ObjectContext{
            get{
                return ((IObjectContextAdapter) this).ObjectContext;
            }
        }

        public DbSet Set(string entitySetName) {
            throw new NotImplementedException();
        }
    }
}
