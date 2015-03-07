using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using DatingHeaven.Entities;

namespace DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators {
    public abstract class EntitySqlGenerator<T> : SqlGenerator where T: BaseEntity{
        protected IEntityInfoResolver EntityInfoResolver;
        private readonly Type _typeBaseBusinessEntityWithId;

        protected EntitySqlGenerator(SqlGeneratorConfig config,
                                     IEntityInfoResolver tableResolver) : base(config){
            if (tableResolver == null){
                // we cannot work futher without a TableNameResolver
                throw new NullReferenceException("<TableNameResolver> is not defined");
            }

            EntityInfoResolver = tableResolver;
            
            // save the type of [BaseBusinessEntityWithId] to the local variable
            _typeBaseBusinessEntityWithId = typeof (BaseBusinessEntityWithId);
        }

        

        /// <summary>
        /// Keys used as filters in the 'WHERE' clause
        /// </summary>
        public object Key{
            get; 
            set;
        }


        /// <summary>
        ///  Generate the major part of an SQL query 
        /// </summary>
        protected abstract void GenerateSqlClauseInternal(StringBuilder sb);

        public override string GenerateSql(){
            var sb = new StringBuilder();

            GenerateSqlClauseInternal(sb);

            // GENERATE THE MAJOR PART OF THE SQL QUERY
            //GenerateFromClause(sb);

            if (Key != null){
                // GENERATE THE 'WHERE' CLAUSE
                GenerateWhereClause(sb);
            }

            return sb.ToString();
        }


        protected void GenerateFromClause(StringBuilder sb){
               // sql: FROM
               sb.Append("FROM ");

               // sql: FROM [{TableName}]
               sb.AppendFormat("[{0}]", EntityInfoResolver.GetTableName<T>());

               if (Key != null){
                   // add some space after the [{TableName}] 
                   // cause next is 'WHERE' clause coming
                   sb.Append(" ");
               }
        }

        private void GenerateWhereClause(StringBuilder sb){
            // sql: WHERE 
            sb.Append("WHERE ");

            if (Key is int){
                // single key
                if (IsBaseEntityWithId()){
                    // The current entity type is {BaseEntityWithId}
                    sb.AppendFormat("[Id] = {0}", Config.ParameterSymbol + "0");
                } else{
                    var keyPropertyName = EntityInfoResolver.GetEntityKeyProperty<T>();
                    sb.AppendFormat("[{0}] = {1}", keyPropertyName, Config.ParameterSymbol + "0");
                }
            } else{
                // composite key
                var keyMembers = ((EntityKey) Key).EntityKeyValues.ToList();
                keyMembers.ForEach(key =>{
                    sb.AppendFormat("([{0}] = {1})",
                                    key.Key,
                                    Config.ParameterSymbol + keyMembers.IndexOf(key));
                    if (keyMembers.IndexOf(key) < (keyMembers.Count - 1)){
                        // add some space and a AND operator
                        sb.Append(" AND ");
                    }
                });
            }
        }

        private bool IsBaseEntityWithId(){
            // check if possible to assign 
            return _typeBaseBusinessEntityWithId.IsAssignableFrom(typeof (T));
        }
    }
}
