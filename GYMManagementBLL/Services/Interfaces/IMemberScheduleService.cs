using GYMManagementBLL.ViewModel;
using GYMManagementBLL.ViewModel.MemberScheduleViewModels;
using GYMManagementBLL.ViewModel.SessionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementBLL.Services.Interfaces
{
    public interface IMemberScheduleService
    {
        public IEnumerable<SessionViewModel> GetUpcomingAndOngoingSessions();

        public IEnumerable<MembersOfUpcomingSessionsviewModel>GetMembersForUpcomingSession(int id);
        public IEnumerable<MembersForOngoingSessions> GetMembersForOngoingSession(int id);

        public bool CreateMemberBooking(CreateMemberSessionViewModel bookedMember);

        public bool DeleteMemberBooking(int memberId);



    }
}
