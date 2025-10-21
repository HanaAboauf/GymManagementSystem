using GYMManagementBLL.ViewModel.PlanViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementBLL.Services.Interfaces
{
    public interface IPlanService
    {
        IEnumerable<PlanViewModel>GetAllPlans();

        PlanViewModel? GetPlan(int id);

        PlanToUpdateViewModel? GetPlanToUpdate(int id);

        bool UpdatePlanDetails(int id,PlanToUpdateViewModel UpdatedPlan);

        bool TogglePlanStatus(int id);
    }
}
