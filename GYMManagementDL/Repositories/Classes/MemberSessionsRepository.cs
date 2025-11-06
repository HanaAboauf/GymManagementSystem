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
    public class MemberSessionsRepository : GenericRepository<MemberSession>, IMemberSessionsRepository
    {
        private readonly GymManagementDbContext _Dbcontext;

        public MemberSessionsRepository(GymManagementDbContext dbcontext) : base(dbcontext)
        {
            _Dbcontext = dbcontext;
        }

        public IEnumerable<MemberSession> GetMembersSessions(int id)
        {
            return _Dbcontext.MemberSessions
                             .Include(ms => ms.Member)
                             .Include(ms => ms.Session)
                             .Where(ms => ms.SessionId == id) 
                             .ToList();

        }
    }
}

