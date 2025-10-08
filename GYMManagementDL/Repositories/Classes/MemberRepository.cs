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
    internal class MemberRepository : IMemberRepository
    {
        private readonly GymManagementDbContext _dbcontext;

        public MemberRepository(GymManagementDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IEnumerable<Member> GetAllMembers()=> _dbcontext.Members.ToList();
        public Member? GetMemberById(int id)=> _dbcontext.Members.Find(id);
        public int AddMember(Member member)
        {
           _dbcontext.Members.Add(member);
              return _dbcontext.SaveChanges();
        }
        public int UpdateMember(Member member)
        {
            _dbcontext.Members.Update(member);
            return _dbcontext.SaveChanges();
        }

        public int DeleteMember(int id)
        {
            var member = _dbcontext.Members.Find(id);
            if(member is null) return 0;    
            _dbcontext.Members.Remove(member);
            return _dbcontext.SaveChanges();
        }



    }
}
