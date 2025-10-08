using GYMManagementDL.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementDL.Repositories.Interfaces
{
    internal interface IPlanRepository
    {
        public IEnumerable<Plan> GetAllPlan();

        public Plan? GetPlanById(int id);

        public int AddPlan(Plan member);

        public int UpdatePlan(Plan member);

        public int DeletePlan(int id);
    }
}
