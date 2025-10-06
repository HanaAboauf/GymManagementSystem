using GYMManagementDL.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementDL.Repositories.Interfaces
{
    internal interface IMemberSessionRepository
    {
        public IEnumerable<MemberSession> GetAllMembers();

        public MemberSession? GetMemberById(int id);

        public int AddMember(MemberSession member);

        public int UpdateMember(MemberSession member);

        public int DeleteMember(int id);
    }
}
