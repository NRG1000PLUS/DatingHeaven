using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using DatingHeaven.Entities;

namespace DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators {
    public abstract class EntityCrudSqlGenerator<T> : SqlGenerator where T: BaseBusinessEntity{
        protected IEntityTableInfoResolver TableInfoResolver;
        private readonly Type _typeBaseBusinessEntityWithId;

        protected EntityCrudSqlGenerator(IEntityTableInfoResolver tableResolver){
            if (tableResolver == null){
                // we cannot work futher without a TableNameResolver
                throw new NullReferenceException("<TableNameResolver> is not defined");
            }

            TableInfoResolver = tableResolver;
            
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
               sb.AppendFormat("[{0}]", TableInfoResolver.GetTableName<T>());

               if (Key != null){
                   // add some space after the [{TableName}] 
                   // cause next is 'WHERE' clause coming
                   sb.Append(" ");
               }
        }

        private void GenerateWhereClause(StringBuilder sb){
            // sql: WHERE 
            sb.Append("WHERE ");

            if (IsBaseEntityWithId() && (Key is int)){
                // single key
                int entityId = (int) Key;
                sb.AppendFormat("[Id] = {0}", (ParameterPlaceholdersEnabled ? "@p0" : entityId.ToString()));
            } else{
                // composite key
                var keyMembers = ((EntityKey) Key).EntityKeyValues.ToList();
                keyMembers.ForEach(key =>{
                    sb.AppendFormat("([{0}] = {1})",
                                    key.Key, 
                     !ParameterPlaceholdersEnabled ? 
                               SqlInjectedValueFormatter.ObjectToString(key.Value) :
                               string.Format("@param{0}", keyMembers.IndexOf(key))
                     );
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
