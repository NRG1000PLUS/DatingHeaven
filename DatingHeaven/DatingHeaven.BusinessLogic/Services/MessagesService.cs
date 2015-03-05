using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatingHeaven.DataAccessLayer;
using DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations;
using DatingHeaven.Entities;

namespace DatingHeaven.BusinessLogic.Services {
    class MessagesService : BaseService, IMessagesService{
        private readonly IRepository<Message> _messagesRepo;

        public MessagesService(IRepository<Message> messagesRepository,
                               IEntityOperationsProvider entityContextProvider): 
            base(entityContextProvider){
                   _messagesRepo = messagesRepository;
        }  


        public Entities.Message GetMessageById(int messageId){
            return _messagesRepo.GetById(messageId);
        }

        public IList<Entities.Message> GetUnreadMessages(int userId){
            return _messagesRepo.GetWhere(m => !m.IsRead && (m.ReceiverId == userId));
        }

        public IList<Entities.Message> GetMessages(int userId) {
            throw new NotImplementedException();
        }

        public void DeleteMessage(int userId, int messageId) {
            throw new NotImplementedException();
        }

        public void SendMessage(Entities.Message message) {
            throw new NotImplementedException();
        }

        public void SetMessageAsRead(int userId, int messageId) {
            if (EntityOperations != null){
                  EntityOperations.SetProperty<Message>(messageId, "IsRead", true);
            }
        }
    }
}
