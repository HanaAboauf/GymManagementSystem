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
    internal class MemberShipRepository : IMemberShipRepository
    {
        private readonly GymManagementDbContext _context= new GymManagementDbContext();
        public int AddMember(MemberShip member)
        {
            _context.MemberShips.Add(member);
            return _context.SaveChanges();

        }

        public int DeleteMember(int id)
        {
            var member = _context.MemberShips.Find(id);
            if (member is null) return 0;

            _context.MemberShips.Remove(member);
            return _context.SaveChanges();

        }

        public IEnumerable<MemberShip> GetAllMembers()=> _context.MemberShips.ToList();
        

        public MemberShip? GetMemberById(int id)=> _context.MemberShips.Find(id);

        public int UpdateMember(MemberShip member)
        {
          _context.MemberShips.Update(member);
            return _context.SaveChanges();
        }
    }
}
