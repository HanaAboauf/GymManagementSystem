using GYMManagementDL.Data.Contexts;
using GYMManagementDL.Enitities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementDL.Repositories.Interfaces
{
    public interface IMembershipRepository
    {
        public IEnumerable<MemberShip> GetMembershipsWithMemberAndPlan();
    }
}
