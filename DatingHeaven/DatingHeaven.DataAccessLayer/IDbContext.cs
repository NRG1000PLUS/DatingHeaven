using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using DatingHeaven.Entities;

namespace DatingHeaven.DataAccessLayer {
    public interface IDbContext{
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        DbSet<T> GetSet<T>() where T : BaseBusinessEntity;

        /// <summary>
        /// 
        /// </summary>
        Database Database { get; }

        /// <summary>
        /// Get 
        /// </summary>
        ObjectContext ObjectContext { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

    }
}
