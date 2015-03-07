using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using DatingHeaven.Entities;

namespace DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators {
    public class SelectEntitySqlGenerator<TEntity>: EntitySqlGenerator<TEntity> 
               where TEntity : BaseEntity{

        private List<string> _entityProperties; 

        public SelectEntitySqlGenerator(SqlGeneratorConfig config, 
                                        IEntityInfoResolver tableResolver) : 
            base(config, tableResolver){
            // empty constructor
        }

        public SelectEntitySqlGenerator (string property,
                                         object key, 
                                         SqlGeneratorConfig config,
                                         IEntityInfoResolver tableResolver): 
            base(config, tableResolver){
               SelectedProperties.Add(property);
               Key = key;
        }

        /// <summary>
        /// List of properties to select in the 'SELECT' clause
        /// </summary>
        public List<string> SelectedProperties{
            get{
                return _entityProperties ?? (_entityProperties = new List<string>());
            }
        }

        protected override void GenerateSqlClauseInternal(StringBuilder sb){
            // append the 'SELECT' clause
            sb.Append("SELECT ");

            var noPropertiesSelected = (_entityProperties == null) || 
                                       (SelectedProperties.Count == 0);




            if (noPropertiesSelected){
                // we need all the members of EntityType
                sb.Append(" * ");
            } else{
           

                SelectedProperties.ForEach(prop =>{
                    sb.AppendFormat("[{0}]", prop);

                    if (SelectedProperties.IndexOf(prop) < (SelectedProperties.Count - 1)){
                         // add some space between different column names and a comma
                        sb.Append(", ");
                    }
                });   
            }

            // add some space before the 'FROM' clause
            sb.Append(" ");

            // GENERATE THE 'FROM' CLAUSE PART
            GenerateFromClause(sb);
        }

       
    }
}
