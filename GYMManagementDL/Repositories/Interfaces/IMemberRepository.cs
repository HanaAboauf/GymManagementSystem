using GYMManagementDL.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementDL.Repositories.Interfaces
{
    internal interface IMemberRepository
    {
        public IEnumerable<Member> GetAllMembers();

        public Member? GetMemberById(int id);

        public int AddMember(Member member);

        public int UpdateMember(Member member);

        public int DeleteMember(int id);
    }
}
