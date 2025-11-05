using GYMManagementDL.Data.Contexts;
using GYMManagementDL.Enitities;
using GYMManagementDL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementDL.Repositories.Classes
{
    public class MembershipsRepository : GenericRepository<MemberShip>, IMembershipRepository
    {
        private readonly GymManagementDbContext _Dbcontext;

        public MembershipsRepository(GymManagementDbContext dbcontext) : base(dbcontext)
        {
            _Dbcontext = dbcontext;
        }
        public IEnumerable<MemberShip> GetMembershipsWithMemberAndPlan()
        {
            var memberships = _Dbcontext.MemberShips.Include(ms => ms.Member)
                                                  .Include(ms => ms.Plan);
            return memberships;
        }
    }
}
