using GYMManagementDL.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementDL.Repositories.Interfaces
{
    internal interface IMemberShipRepository
    {
        public IEnumerable<MemberShip> GetAllMembers();

        public MemberShip? GetMemberById(int id);

        public int AddMember(MemberShip member);

        public int UpdateMember(MemberShip member);

        public int DeleteMember(int id);
    }
}
