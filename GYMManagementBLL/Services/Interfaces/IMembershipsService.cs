using GYMManagementBLL.ViewModel;
using GYMManagementBLL.ViewModel.MembershipsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementBLL.Services.Interfaces
{
    public interface IMembershipsService
    {
        public IEnumerable<MembershipsViewModel> GetAllActiveMemberships();

        public IEnumerable<PlanDropDownViewModel> GetAllPlansForDropDown();

        public IEnumerable<MemberDropDownViewModel> GetAllMemberssForDropDown();

        public bool CreateMembership(CreateMembershipViewModel createdMembership);

        public bool DeleteMembership(int membershipId);
    }
}
