using DatingHeaven.Entities;

namespace DatingHeaven.DataAccessLayer.Infrastructure {
    public interface IEntityChangesLogService{

        /// <summary>
        /// Log action
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userId"></param>
        void LogAdded(BaseEntity entity, int userId);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userId"></param>
        void LogChanged(BaseEntity entity, int userId);
    }
}
