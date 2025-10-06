using GYMManagementDL.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementDL.Repositories.Interfaces
{
    internal interface ISessionRepository
    {
        public IEnumerable<Session> GetAllSessions();

        public Session? GetSessionById(int id);

        public int AddSession(Session member);

        public int UpdateSession(Session member);

        public int DeleteSession(int id);
    }
}
