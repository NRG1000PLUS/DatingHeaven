using System;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations;
using DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators;
using DatingHeaven.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaseTests {
    [TestClass]
    public class SqlGeneratorTests{
        private EntitySqlGeneratorsFactory _generatorsFactory;
        private IEntityInfoResolver _tableInfoResolver;

        [TestInitialize]
        public void Init(){
           _generatorsFactory = new EntitySqlGeneratorsFactory(new EntityInfoResolver()); 
        }

        [TestMethod]
        public void factory_should_create_generators_for_business_entities(){
            var generator = _generatorsFactory.CreateSelectSqlGenerator<Message>();

            Assert.IsTrue(generator != null);
        }

        [TestMethod]
        public void select_sql_generator_should_have_SinleProperty_NULL(){
            var generator = _generatorsFactory.CreateSelectSqlGenerator<Message>();
            Assert.IsTrue( generator != null);
        }


        [TestMethod]
        public void output_generated_sql_to_console(){
            var generator = _generatorsFactory.CreateSelectSqlGenerator<Message>();
            Debug.WriteLine(generator.GenerateSql());
        }

        [TestMethod]
        public void generated_sql_should_contain_ASTERIKS_symbol_when_no_property_selected(){
            var generator = _generatorsFactory.CreateSelectSqlGenerator<Message>();
            var sql = generator.GenerateSql();
            Assert.IsTrue(sql.Contains("*"));
        }

        [TestMethod]
        public void generated_sql_should_have_property_after_select_clause_when_SingleProperty_not_NULL(){
            var generator = _generatorsFactory.CreateSelectSqlGenerator<Message>();
            var sql = generator.GenerateSql();
            //Assert.IsTrue( sql);
        }


        [TestMethod]
        public void test_update_sql_generator(){
            var generator = _generatorsFactory.CreateUpdateSqlGenerator<Message>();
            generator.Key = 3232;
            generator.Set("SenderId", 9932);
            generator.Set("IsRead", true);

            Debug.WriteLine(generator.GenerateSql());
        }
    }
}
