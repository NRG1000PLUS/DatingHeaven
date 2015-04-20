using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatingHeaven.Entities.Members;

namespace DatingHeaven.BusinessLogic.Services {
     public interface IHotListService{
         /// <summary>
         /// 
         /// </summary>
         /// <param name="memberId"></param>
         /// <param name="targetMemberId"></param>
         /// <param name="notify"></param>
         /// <returns></returns>
         bool AddMemberToHotList(int memberId, int targetMemberId, bool notify, string comment);

         /// <summary>
         /// s
         /// </summary>
         /// <param name="memberId"></param>
         /// <returns></returns>
         IList<Member> GetHotMembers(int memberId);
     }
}
