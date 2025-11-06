using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementBLL.ViewModel.MemberScheduleViewModels
{
    public class MembersOfUpcomingSessionsviewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public DateTime BookingDate { get; set; }

    }
}
