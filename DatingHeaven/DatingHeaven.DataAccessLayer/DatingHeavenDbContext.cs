using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Data.Entity;
using DatingHeaven.DataAccessLayer.Infrastructure;
using DatingHeaven.Entities;
using DatingHeaven.Entities.Member;

namespace DatingHeaven.DataAccessLayer {
    public class DatingHeavenDbContext: DbContext, IDbContext{
        private DbSet<Message> _messages;
        private DbSet<Member> _members;

        public DatingHeavenDbContext() {

           Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Message> Messages{
            get{
                return _messages ?? (_messages = base.Set<Message>());
            }
        }

        public DbSet<Member> Members{
            get{
                return _members ?? (_members = base.Set<Member>());
            }
        } 

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

        public DbSet<T> GetSet<T>() where T : BaseEntity{
            return base.Set<T>();
        }


        /// <summary>
        /// Get ObjectContext from the current DbContext
        /// </summary>
        public ObjectContext ObjectContext {
            get{
                return ((IObjectContextAdapter) this).ObjectContext;
            }
        }
    }             
}
