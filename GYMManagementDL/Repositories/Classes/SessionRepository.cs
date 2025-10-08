using GYMManagementDL.Data.Contexts;
using GYMManagementDL.Enitities;
using GYMManagementDL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementDL.Repositories.Classes
{
    internal class SessionRepository : ISessionRepository
    {
        private readonly GymManagementDbContext _context;

        public SessionRepository(GymManagementDbContext dbcontext)
        {
            _context = dbcontext;
        }
        public int AddSession(Session member)
        {
            _context.Sessions.Add(member);
            return _context.SaveChanges();
        }

        public int DeleteSession(int id)
        {
           var session= _context.Sessions.Find(id);

            if(session is null) return 0;

            _context.Sessions.Remove(session);
            return _context.SaveChanges();
        }

        public IEnumerable<Session> GetAllSessions()=> _context.Sessions.ToList();

        public Session? GetSessionById(int id)=> _context.Sessions.Find(id);

        public int UpdateSession(Session member)
        {
            _context.Sessions.Update(member);
            return _context.SaveChanges();
        }
    }
}
