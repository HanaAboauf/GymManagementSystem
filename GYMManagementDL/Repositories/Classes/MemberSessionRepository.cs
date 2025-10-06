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
    internal class MemberSessionRepository : IMemberSessionRepository
    {
        private readonly GymManagementDbContext dbcontext = new GymManagementDbContext();
        public int AddMember(MemberSession member)
        {
            dbcontext.MemberSessions.Add(member);
            return dbcontext.SaveChanges();
        }

        public int DeleteMember(int id)
        {
            var member = dbcontext.MemberSessions.Find(id);
            if (member is null) return 0;
            dbcontext.MemberSessions.Remove(member);
            return dbcontext.SaveChanges();
        }

        public IEnumerable<MemberSession> GetAllMembers()=> dbcontext.MemberSessions.ToList();

        public MemberSession? GetMemberById(int id)=> dbcontext.MemberSessions.Find(id);

        public int UpdateMember(MemberSession member)
        {
           dbcontext.MemberSessions.Update(member);
              return dbcontext.SaveChanges();
        }
    }
}
