using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementBLL.ViewModel.MembershipsViewModels
{
    public class MembershipsViewModel
    {
        public int Id { get; set; }
        public string MemberName { get; set; } = string.Empty;

        public string PlanName { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
