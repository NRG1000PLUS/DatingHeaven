using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using DatingHeaven.BusinessLogic.Services;
using DatingHeaven.DataAccessLayer;
using DatingHeaven.Entities;
using DatingHeaven.Entities.Members;

namespace DatingHeaven.BusinessLogic.ServicesImplementation {
    class HotListService : BaseService, IHotListService{
        private readonly IRepository<HotListEntry> _repoHotListEntries;

        public HotListService(IRepository<HotListEntry> hotListRepository){
            _repoHotListEntries = hotListRepository;
        }


        public bool AddMemberToHotList(int memberId, int targetMemberId, bool notify, string comment) {
            if (memberId == targetMemberId){
                // 
                throw new InvalidOperationException("Cannot add a member to itself");
            }

            bool alreadyHasInList = CheckIfAlreadyAdded(memberId, targetMemberId);
            if (alreadyHasInList){
                // if we have the 
                return false;
            }

            var newEntry = new HotListEntry{
                    MemberId = memberId,
                    TargetMemberId = targetMemberId,
                    Comment = comment,
                    ShouldNotify = notify
            };


            _repoHotListEntries.Insert(newEntry);

            if (notify){
                    
            }
          
            return true;
        }

        public IList<Member> GetHotMembers(int memberId){
            var membersList = new List<Member>();
            var hotListEntries = _repoHotListEntries.GetWhereInclude(entry => entry.MemberId, memberId,
                entry => entry.TargetMember);

            if (hotListEntries != null){
                // select the TARGET members to the list
                membersList.AddRange(hotListEntries.Select(entry => entry.TargetMember));
            }

            return membersList;
        }

        private bool CheckIfAlreadyAdded(int memberId, int targetMemberId){
            HotListEntry entry = _repoHotListEntries.GetById(
                                        new[]{memberId, targetMemberId});
            return (entry != null);
        }
    }
}
