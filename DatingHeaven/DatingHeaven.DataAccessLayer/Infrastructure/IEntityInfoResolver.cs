using System;
using System.Data.Entity.Core.Objects;
using DatingHeaven.Entities;

namespace DatingHeaven.DataAccessLayer.Infrastructure {
    public interface IEntityInfoResolver{
        /// <summary>
        /// Get name of the table by its entity type
        /// </summary>
        string ResolveTableName<T>() where T : BaseEntity;
    }
}
