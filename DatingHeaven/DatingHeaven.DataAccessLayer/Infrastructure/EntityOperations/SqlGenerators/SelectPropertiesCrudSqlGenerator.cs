using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using DatingHeaven.Entities;

namespace DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators {
    public class SelectPropertiesCrudSqlGenerator<TEntity>: EntityCrudSqlGenerator<TEntity> 
               where TEntity : BaseBusinessEntity{

        private List<string> _entityProperties; 

        public SelectPropertiesCrudSqlGenerator(IEntityTableInfoResolver tableResolver) : 
            base(tableResolver){
            // empty constructor
        }

        public SelectPropertiesCrudSqlGenerator (string property,
                                             object key, 
                                             IEntityTableInfoResolver tableResolver): 
            base(tableResolver){
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

        /// <summary>
        /// 
        /// </summary>
        public string SingleProperty{
            get; 
            set; 
        }





        protected override void GenerateSqlClauseInternal(StringBuilder sb){
            // append the 'SELECT' clause
            sb.Append("SELECT ");

            var noPropertiesSelected = (_entityProperties == null);

            if (!noPropertiesSelected){
                // ensure that we have no properties to select
                noPropertiesSelected = (SelectedProperties.Count == 0);
            } 



            if (noPropertiesSelected){
                // select ALL the columns in the table
                if (string.IsNullOrEmpty(SingleProperty)){
                    sb.Append(" * ");
                } else{
                    sb.AppendFormat("[{0}]", SingleProperty);
                }
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
