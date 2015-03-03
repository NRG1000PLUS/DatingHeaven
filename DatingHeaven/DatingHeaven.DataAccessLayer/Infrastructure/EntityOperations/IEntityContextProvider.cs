﻿using System;
using DatingHeaven.Entities;

namespace DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations {
    /// <summary>
    /// Provider that provides the API to process low-level entity data.
    /// EntityContext allows a client to query low level entity data, get entity properties, values 
    /// </summary>
    public interface IEntityOperationsProvider{
        /// <summary>
        /// 
        /// </summary>>
        /// <returns></returns>
        object GetProperty<T>(object entityId, string property ) where T: BaseEntity;

        /// <summary>
        /// 
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityId"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        void SetPropery<T>(object entityId, string property, object value);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertySelector"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        T GetEntityByProperty<T>(Func<T, object> propertySelector, object propertyValue) where T : BaseEntity;
    }
}