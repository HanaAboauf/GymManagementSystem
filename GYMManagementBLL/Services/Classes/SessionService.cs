using AutoMapper;
using GYMManagementBLL.Services.Interfaces;
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
    internal class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
           _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<SessionViewModel> GetAllSessions()
        {
           var sessions = _unitOfWork.SessionRepository.GetAllSessionsWithTrainerAndCategory();
            if (!sessions.Any()) return [];
            var MappedSessions=_mapper.Map<IEnumerable<Session>,IEnumerable<SessionViewModel>>(sessions);
            foreach (var session in MappedSessions)
                session.AvailableSlots = session.Capacity - _unitOfWork.SessionRepository.GetBookedSlots(session.Id);
          
            return MappedSessions;
        }

        public SessionViewModel? GetSessionById(int id)
        {
            var session=_unitOfWork.SessionRepository.GetSessionWithTrainerAndCategory(id);
            if (session is null) return null;
            var MappedSession = _mapper.Map<Session, SessionViewModel>(session);
            MappedSession.AvailableSlots = MappedSession.Capacity - _unitOfWork.SessionRepository.GetBookedSlots(MappedSession.Id);
            return MappedSession;
        }

        public bool CreateSession(CreateSessionViewModel CreatedSession)
        {
            try
            {
                if (!IsCategoryExists(CreatedSession.CategoryId) || !IsTrainerExists(CreatedSession.TrainerId) || !IsDateValid(CreatedSession.StartTime, CreatedSession.EndTime)) return false;

                if (CreatedSession.Capacity > 25 || CreatedSession.Capacity < 1) return false;

                var Mappedsession = _mapper.Map<Session>(CreatedSession);

                _unitOfWork.GetRepository<Session>().Add(Mappedsession);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch(Exception ex) 
            {
                Console.WriteLine($"Create session falied {ex}");
            
                return false;
            }



        }

        public SessionToUpdateViewModel? GetSessionToUpdate(int sessionId)
        {
          var session=_unitOfWork.SessionRepository.GetById(sessionId);
            if (session == null) return null;
            if(!IsSessionAvailableToUpdating(session)) return null;

            return _mapper.Map<SessionToUpdateViewModel>(session);
        }

        public bool UpdateSession(int sessionId, SessionToUpdateViewModel UpdatedSession)
        {
            try
            {
                var session = _unitOfWork.SessionRepository.GetById(sessionId);

                if (!IsSessionAvailableToUpdating(session!)) return false;

                if (!IsDateValid(session!.StartTime, session.EndTime)) return false;

                if (!IsTrainerExists(session.TrainerId)) return false;

                _mapper.Map(UpdatedSession, session);
                session.UpdatedAt = DateTime.Now;

                _unitOfWork.SessionRepository.Update(session);

                return _unitOfWork.SaveChanges() > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update is falied {ex}");
                return false;
            }


        }

        public bool RemoveSession(int sessionId)
        {
           var session=_unitOfWork.SessionRepository.GetById(sessionId);

            if(!IsSessionAvailableToRemoving(session!)) return false;
            _unitOfWork.SessionRepository.Delete(session!);
            return _unitOfWork.SaveChanges() > 0;

        }

        #region Helper Methods

        bool IsCategoryExists(int categoryId)=>_unitOfWork.GetRepository<Category>().GetById(categoryId) is not null;

        bool IsTrainerExists(int trainerId) => _unitOfWork.GetRepository<Trainer>().GetById(trainerId) is not null;

        bool IsDateValid(DateTime startDate, DateTime endDate) => startDate > endDate;

        bool IsSessionAvailableToUpdating(Session session)
        {
            if (session == null) return false;

            if(session.StartTime<=DateTime.Now) return false;

            if (session.EndTime<DateTime.Now) return false;

            var HasBooking = _unitOfWork.SessionRepository.GetBookedSlots(session.Id) > 0;

            if (HasBooking) return false;

            return true;

        }

        bool IsSessionAvailableToRemoving(Session session)
        {
            if (session == null) return false;

            if (session.StartTime <= DateTime.Now&& session.EndTime>DateTime.Now) return false;

            if (session.StartTime > DateTime.Now) return false;

            var HasBooking = _unitOfWork.SessionRepository.GetBookedSlots(session.Id) > 0;

            if (HasBooking) return false;

            return true;

        }
        #endregion
    }
}
