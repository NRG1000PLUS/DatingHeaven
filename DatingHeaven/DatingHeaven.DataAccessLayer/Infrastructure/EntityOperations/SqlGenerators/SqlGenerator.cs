using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators {
    public abstract class SqlGenerator{
        private SqlGeneratorConfig _config;


        protected SqlGenerator(SqlGeneratorConfig config){
            if (config == null){
                throw new NullReferenceException("config");
            }
            _config = config;
        }


        public SqlGeneratorConfig Config{
            get{
                return _config;
            }
        }



       
        

        public abstract string GenerateSql();
    }
}
