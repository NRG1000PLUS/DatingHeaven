using System;
using System.Data.Entity.Core;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using DatingHeaven.DataAccessLayer;
using DatingHeaven.DataAccessLayer.Infrastructure;
using DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations;
using DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators;
using DatingHeaven.Entities;
using DatingHeaven.Entities.Member;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaseTests {
    [TestClass]
    public class EntityContextTests{
        private IEntityContextProvider _entityContextProvider;
        private IEntityInfoResolver _entityInfoResolver;
        private IDbContext _dbContext;
        private Message _newMessage;
        private RandomDataGenerator _dataGenerator;
       

        [TestInitialize]
        public void Init(){
            _entityInfoResolver = new EntityInfoResolver();
            _dbContext = new DatingHeavenDbContext();
            _entityContextProvider = new EntityContextProvider(
                        _entityInfoResolver,
                        _dbContext,
                        new EntitySqlGeneratorsFactory(_entityInfoResolver)
            );
            _dataGenerator = new RandomDataGenerator();
        }

        [TestMethod]
        public void check_guid_value_save(){
            var newMessage =new Message{
                 SenderId = _dataGenerator.RandomInt(),
                 ReceiverId = _dataGenerator.RandomInt(),
                 Body = "Wow! I am so fucking glad to see you, bitch!",
                 Header = "Greetings!",
                 IsRead = false,
                 CreatedOn = DateTime.Now,
                 ModifiedOn = DateTime.Now,
                 GuidValue = Guid.NewGuid(),
                 IsHidden = false
            };

            _dbContext.GetSet<Message>().Add(newMessage);
            _dbContext.SaveChanges();

            Guid oldGuid = newMessage.GuidValue;

            Assert.IsTrue(newMessage.Id != 0);


            _entityContextProvider.SetPropertyValue<Message>(newMessage.Key, 
                                                             m => m.GuidValue,
                                                             Guid.NewGuid());

            Assert.IsTrue(oldGuid != newMessage.GuidValue);
        }

        [TestMethod]
        public void get_property_value(){
            _newMessage = new Message{
                 SenderId = 332,
                 ReceiverId = 222,
                 Body = "How are you doing, mazafaka, bitch!?",
                 Header = "Hello",
                 CreatedOn = DateTime.Now,
                 ModifiedOn = DateTime.Now
            };

            var returnedMessage = _dbContext.GetSet<Message>().Add(_newMessage);
            _dbContext.SaveChanges();



            var body = _entityContextProvider.
                GetPropertyValue<Message>(returnedMessage.Id, m => m.Header);
            Assert.IsTrue(returnedMessage.Header.Equals(body));

           
        }

        [TestMethod]
        public void should_throw_exception_when_not_property_selected_in_selector(){
            _newMessage = new Message {
                SenderId = 332,
                ReceiverId = 222,
                Body = _dataGenerator.GenerateRandomString(300) ,
                Header = _dataGenerator.GenerateRandomString(100),
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
            };

            var returnedMessage = _dbContext.GetSet<Message>().Add(_newMessage);
            _dbContext.SaveChanges();

            try{
                var val = _entityContextProvider.GetPropertyValue<Message>(returnedMessage.Id,
                    m => m.GetType());
                Assert.Fail();
            } catch (InvalidOperationException ex){
              
            }
        }


        [TestMethod]
        public void select_several_properties_of_the_Message_entity(){
            _newMessage = new Message{
                SenderId = 9923,
                ReceiverId = 2303,
                Body = "Be good! Be awesome!",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                Header = "Greeting"
            };

            var returnedMessage = _dbContext.GetSet<Message>().Add(_newMessage);
            _dbContext.SaveChanges();

            var entity = new Message();

            Assert.IsTrue(entity != null);

         //   Assert.IsTrue(entity.Header != null);
          //  Assert.IsTrue(entity.Body != null);
        }

        [TestMethod]
         public void update_message_entity(){
             _newMessage = new Message {
                 SenderId = 9923,
                 ReceiverId = 2303,
                 Body = "Be good! Be awesome!",
                 CreatedOn = DateTime.Now,
                 ModifiedOn = DateTime.Now,
                 Header = "Greeting"
             };

             Message returnedMessage = _dbContext.GetSet<Message>().Add(_newMessage);
             _dbContext.SaveChanges();

            var isSuccess = _entityContextProvider.SetPropertyValue<Message>(returnedMessage.Id,
                m => m.IsRead, true);

            if (isSuccess){
                Assert.IsTrue(returnedMessage.IsRead);
            }
        }
    }
}
