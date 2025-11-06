using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementBLL.ViewModel.MemberScheduleViewModels
{
    public class MembersForOngoingSessions
    {
        public string Name { get; set; } = null!;

        public bool IsAttended { get; set; }=false;
    }
}
