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
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly GymManagementDbContext _dbcontext;

        public SessionRepository(GymManagementDbContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IEnumerable<Session> GetAllSessionsWithTrainerAndCategory()
        {
            return _dbcontext.Sessions.Include(x => x.Trainer)
                                             .Include(x => x.Category).ToList();
        }

        public int GetBookedSlots(int sessionId)
        {
            return _dbcontext.Sessions.Count(s=>s.Id == sessionId);
        }

        public Session? GetSessionWithTrainerAndCategory(int sessionId)
        {
            return _dbcontext.Sessions.Include(s=>s.Trainer)
                                             .Include(x => x.Category)
                                             .FirstOrDefault(s=>s.Id== sessionId);          
        }
    }
}
