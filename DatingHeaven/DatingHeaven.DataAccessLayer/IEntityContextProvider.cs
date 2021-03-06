﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq.Expressions;
using DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations;
using DatingHeaven.Entities;

namespace DatingHeaven.DataAccessLayer {
    /// <summary>
    /// Provider that provides the API to process low-level entity data.
    /// EntityContext allows a client to query low level entity data, get entity properties, values 
    /// </summary>
    public interface IEntityContextProvider{

        /// <summary>
        /// Get resolver for entity-specific properties
        /// </summary>
        IEntityInfoResolver EntityInfoResolver { get; }

        /// <summary>
        ///  Get the entity property value using a property name
        /// </summary>
        object GetPropertyValue<T>(object entityKey, string property) where T: BaseEntity;

        /// <summary>
        /// Get the entity property value using a Lambda property selector
        /// </summary>
        object GetPropertyValue<T>(object entityKey, Expression<Func<T, object>> propertySelector) where  T: BaseEntity;

        /// <summary>
        /// Set the entity property value using a property name
        /// </summary>
        bool SetPropertyValue<T>(object entityKey, string property, object value) where T: BaseBusinessEntity;

        /// <summary>
        /// Set the entity property value using a Lambda property selector
        /// </summary>
        bool SetPropertyValue<T>(object entityKey, Expression<Func<T, object>> propertySelector, object value) where T: BaseBusinessEntity;
    }
}
