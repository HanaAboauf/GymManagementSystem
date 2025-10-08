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
    internal class PlanRepository : IPlanRepository
    {

        private readonly GymManagementDbContext _context;

        public PlanRepository(GymManagementDbContext dbcontext)
        {
            _context = dbcontext;
        }
        public int AddPlan(Plan member)
        {
           _context.Plans.Add(member);
            return _context.SaveChanges();
        }

        public int DeletePlan(int id)
        {
           var plan= _context.Plans.Find(id);
            if (plan is null)return 0;
            _context.Plans.Remove(plan);
            return _context.SaveChanges();
        }

        public IEnumerable<Plan> GetAllPlan()=> _context.Plans.ToList();

        public Plan? GetPlanById(int id)=> _context.Plans.Find(id);

        public int UpdatePlan(Plan plan)
        {
           _context.Plans.Update(plan);
            return _context.SaveChanges();  
        }
    }
}
