using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Data.Entity;
using DatingHeaven.DataAccessLayer.EntityAdapters;
using DatingHeaven.DataAccessLayer.Infrastructure;
using DatingHeaven.Entities;
using DatingHeaven.Entities.Member;
using DatingHeaven.Entities.Profile;


namespace DatingHeaven.DataAccessLayer {
    public class DatingHeavenDbContext : DbContext, IDbContext{
        private DbSet<Message>                 _messages;
        private DbSet<Member>                  _members;
        private DbSet<Profile>                 _profiles;
        private DbSet<ProfileAttribute>        _profileAttributes;
        private DbSet<ProfileAttributeValue>   _profileAttributeValues;
        private DbSet<ProfileItem>             _profileItems; 

        

        public DatingHeavenDbContext(){

            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Message> Messages{
            get{
                return _messages ?? (_messages = Set<Message>());
            }
        }

        public DbSet<Member> Members{
            get{
                return _members ?? (_members = Set<Member>());
            }
        }

        public DbSet<Profile> Profiles{
            get{
                return _profiles ?? (_profiles = Set<Profile>());
            }
        }

        //public DbSet<ProfileAttribute> ProfileAttributes{
        //    get{
        //        return _profileAttributes ?? (_profileAttributes = Set<ProfileAttribute>());
        //    }
        //}


        //public DbSet<ProfileAttributeValue> ProfileAttributeValues{
        //    get{
        //        return _profileAttributeValues ?? (_profileAttributeValues = Set<ProfileAttributeValue>());
        //    }
        //}

        //public DbSet<ProfileItem> ProfileItems{
        //    get{
        //        return _profileItems ?? (_profileItems = Set<ProfileItem>());
        //    }
        //} 

        protected override void OnModelCreating(DbModelBuilder modelBuilder){
            
            modelBuilder.Entity<Message>().Map(m =>{
                m.MapInheritedProperties();

                // table name in the physical DB
                m.ToTable("Messages");
            });

            modelBuilder.Entity<Member>().Map(m =>{
                m.MapInheritedProperties();

                m.ToTable("Members");
            });

            modelBuilder.Entity<Profile>().Map(m =>{
                m.MapInheritedProperties();

                m.ToTable("Profiles");
            });

      
        }

        public DbSet<T> GetSet<T>() where T : class {
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


        public AdapterDbSet<TEntity> AdapterSet<TEntity>() where TEntity: BaseEntity{
            var dbSet = base.Set<TEntity>();
            return new AdapterDbSet<TEntity>(dbSet);
        } 






    }
}
