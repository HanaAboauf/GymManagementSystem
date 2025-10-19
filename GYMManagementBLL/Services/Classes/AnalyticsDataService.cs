using GYMManagementBLL.Services.Interfaces;
using GYMManagementBLL.ViewModel.AnalyticsDataViewModels;
using GYMManagementDL.Enitities;
using GYMManagementDL.Repositories.Interfaces;

namespace GYMManagementBLL.Services.Classes
{
    public class AnalyticsDataService : IAnalyticsDataService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnalyticsDataService(IUnitOfWork unitOfWork)
        {
           _unitOfWork = unitOfWork;
        }

        public AnalyticsDataViewModel GetAnalyticsData()
        {
            var totalMembers = _unitOfWork.GetRepository<Member>().GetAll().Count();
            var totalTrainers= _unitOfWork.GetRepository<Trainer>().GetAll().Count();
            var allSessions = _unitOfWork.GetRepository<Session>().GetAll();

            return new AnalyticsDataViewModel()
            {
               TotalMembers = totalMembers,
               ActiveMembers=_unitOfWork.GetRepository<MemberShip>().GetAll(ms=>ms.Status=="Active").Count(),
                TotalTrainers = totalTrainers,
               UpcomingSessions = allSessions.Count(s => s.StartTime>DateTime.Now),
               OngoingSessions = allSessions.Count(s => s.StartTime <= DateTime.Now && s.EndTime >= DateTime.Now),
               CompletedSessions = allSessions.Count(s => s.EndTime < DateTime.Now)
            };


        }

    }
}
