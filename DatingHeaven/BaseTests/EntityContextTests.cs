using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using DatingHeaven.DataAccessLayer;
using DatingHeaven.DataAccessLayer.Infrastructure;
using DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations;
using DatingHeaven.Entities;
using DatingHeaven.Entities.Member;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaseTests {
    [TestClass]
    public class EntityContextTests{
        private IEntityOperationsProvider _entityContextProvider;
        private IEntityInfoResolver _entityInfoResolver;
        private IDbContext _dbContext;

       

        [TestInitialize]
        public void Init(){
            _entityInfoResolver = new EntityInfoResolver();
            _dbContext = new DatingHeavenDbContext();
            _entityContextProvider = new EntityContextProvider(
                       _entityInfoResolver,
                       _dbContext
                );

            if (_dbContext.Database.Exists()){
                _dbContext.Database.Delete();
            }
        }

        [TestMethod]
        public void get_property_value(){
            var newMessage = new Message{
                 SenderId = 332,
                 ReceiverId = 222,
                 Body = "HHH",
                 Header = "dssss",
                 CreatedOn = DateTime.Now,
                 ModifiedOn = DateTime.Now
            };

            var returnedMessage = _dbContext.GetSet<Message>().Add(newMessage);
            _dbContext.SaveChanges();

            var body = (string)_entityContextProvider.GetProperty<Member>(returnedMessage.Id, "Body");
            Assert.IsTrue(returnedMessage.Body == body);
        } 
    }
}
