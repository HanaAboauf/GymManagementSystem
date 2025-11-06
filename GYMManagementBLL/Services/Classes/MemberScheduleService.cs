using AutoMapper;
using GYMManagementBLL.Services.Interfaces;
using GYMManagementBLL.ViewModel;
using GYMManagementBLL.ViewModel.MemberScheduleViewModels;
using GYMManagementBLL.ViewModel.SessionViewModels;
using GYMManagementDL.Enitities;
using GYMManagementDL.Repositories.Classes;
using GYMManagementDL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementBLL.Services.Classes
{
    public class MemberScheduleService : IMemberScheduleService
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IMapper _Mapper;

        public MemberScheduleService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            _Mapper = mapper;
        }

        public IEnumerable<SessionViewModel> GetUpcomingAndOngoingSessions()
        {
            var sessions = _UnitOfWork.SessionRepository.GetAllSessionsWithTrainerAndCategory();
            if (!sessions.Any()) return [];
            var MappedSessions = _Mapper.Map<IEnumerable<Session>, IEnumerable<SessionViewModel>>(sessions);
            foreach (var session in MappedSessions)
                session.AvailableSlots = session.Capacity - _UnitOfWork.SessionRepository.GetBookedSlots(session.Id);
            MappedSessions= MappedSessions.Where(ms => ms.Status == "Ongoing" || ms.Status == "Upcoming");

            return MappedSessions;
        }
        public IEnumerable<MembersForOngoingSessions> GetMembersForOngoingSession(int sessionId)
        {
            var allBooking = _UnitOfWork.MemberSessionsRepository.GetMembersSessions(sessionId);
            if (!allBooking.Any()) return [] ;
            var MembersForOngoingSessions = allBooking.Where(ms => ms.Session.StartTime <= DateTime.Now && ms.Session.EndTime > DateTime.Now)
                                 .Select(ms => new MembersForOngoingSessions
                                 {
                                     IsAttended=ms.IsAttended,
                                     Name=ms.Member.Name,

                                 });
            if (!MembersForOngoingSessions.Any()) return [] ;

            return MembersForOngoingSessions;

        }
        public IEnumerable<MembersOfUpcomingSessionsviewModel> GetMembersForUpcomingSession(int sessionId)
        {

            var allBooking = _UnitOfWork.MemberSessionsRepository.GetMembersSessions(sessionId);
            if (!allBooking.Any()) return [];
            var MembersOfUpcomingSessions = allBooking
                                 .Select(ms => new MembersOfUpcomingSessionsviewModel
                                 {
                                     Id=ms.MemberId,
                                     Name=ms.Member.Name,
                                     BookingDate=ms.CreatedAt,
                                 });
            if (!MembersOfUpcomingSessions.Any()) return [];

            return MembersOfUpcomingSessions;
        }


        public bool CreateMemberBooking(CreateMemberSessionViewModel bookedMember)
        {
            try
            {
                if(!HasActiveMembership(bookedMember.MemberId)) return false;
                if(!HasAvailableCapacity(bookedMember.SessionId)) return false;
                if(!IsBookedOnce(bookedMember.MemberId,bookedMember.SessionId)) return false;
                if(!IsSessionUpcoming( bookedMember.SessionId)) return false;

                var mappedMemberSession = _Mapper.Map<MemberSession>(bookedMember);

                _UnitOfWork.GetRepository<MemberSession>().Add(mappedMemberSession);

                return _UnitOfWork.SaveChanges() > 0;

               


            }
            catch (Exception ex) {
                Console.WriteLine($"Failed to Make the booking {ex.Message}");
                return false;
            }

           
        }

        public bool DeleteMemberBooking(int memberId)
        {
            throw new NotImplementedException();
        }

        #region Helper functions

        bool HasActiveMembership(int memberId)
        {
            var membership = _UnitOfWork.GetRepository<MemberShip>().GetAll(ms=>ms.MemberId== memberId);
            return membership.Any();

        }
        bool HasAvailableCapacity(int sessionId)
        {
            var session=_UnitOfWork.GetRepository<Session>().GetById(sessionId);
            if (session == null) return false;
            return session.Capacity > _UnitOfWork.SessionRepository.GetBookedSlots(session.Id);


        }
        bool IsBookedOnce(int memberId, int sessionId)
        {
            var memberSession = _UnitOfWork.GetRepository<MemberSession>().GetAll(ms=>ms.SessionId==sessionId && ms.MemberId==memberId);
            return memberSession.Any();
        }
        bool IsSessionUpcoming (int sessionId)
        {
            var session = _UnitOfWork.GetRepository<Session>().GetById(sessionId);
            if (session == null) return false;
            return session.StartTime <= DateTime.Now && session.EndTime > DateTime.Now;
        }


        #endregion



    }
}
