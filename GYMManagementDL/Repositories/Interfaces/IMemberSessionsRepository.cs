using GYMManagementDL.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementDL.Repositories.Interfaces
{
    public interface IMemberSessionsRepository:IGenericRepository<MemberSession>
    {
        public IEnumerable<MemberSession> GetMembersSessions(int id);
    }
}
