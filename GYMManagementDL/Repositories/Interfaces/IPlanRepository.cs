using GYMManagementDL.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementDL.Repositories.Interfaces
{
    public interface IPlanRepository
    {
        public IEnumerable<Plan> GetAllPlan();
        public Plan? GetPlanById(int id);
        public int UpdatePlan(Plan plan);

    }
}
