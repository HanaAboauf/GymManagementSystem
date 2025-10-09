using GYMManagementBLL.Services.Interfaces;
using GYMManagementBLL.ViewModel.MemberViewModels;
using GYMManagementBLL.ViewModel.PlanViewModels;
using GYMManagementDL.Enitities;
using GYMManagementDL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementBLL.Services.Classes
{
    internal class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;
  

        public PlanService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var plans = _unitOfWork.GetRepository<Plan>().GetAll();
            if (plans == null ||plans.Any()) return [];
            return plans.Select(p => new PlanViewModel
            {
                Name = p.Name,
                Description = p.Description,
                DurationDays = p.DurationDays,
                Price = p.Price,
                IsActive = p.IsActive,
     
            }).ToList();

        }

        public PlanViewModel? GetPlan(int id)
        {
            var plan=_unitOfWork.GetRepository<Plan>().GetById(id);
            if (plan is null) return null;
            return new PlanViewModel()
            {
                Name = plan.Name,
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                Price = plan.Price,
                IsActive = plan.IsActive,
            };
        }

        public PlanToUpdateViewModel? GetPlanToUpdate(int id)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(id);
            if(plan is null || plan.IsActive == false|| HasActiveMemberShip(id)) return null;
            return new PlanToUpdateViewModel()
            {
                Name = plan.Name,
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                Price = plan.Price,

            };
        }

        public bool TogglePlanStatus(int id)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(id);
            if (plan is null || HasActiveMemberShip(id)) return false;

            plan.IsActive=plan.IsActive==true?false:true;

            plan.UpdatedAt = DateTime.Now;

            try
            {
                _unitOfWork.GetRepository<Plan>().Update(plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }


        }

        public bool UpdatePlanDetails(int id, PlanViewModel UpdatedPlan)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(id);
            if (plan is null|| HasActiveMemberShip(id)) return false;

            (plan.Name, plan.Description, plan.DurationDays, plan.Price,plan.UpdatedAt) = (UpdatedPlan.Name, UpdatedPlan.Description, UpdatedPlan.DurationDays, UpdatedPlan.Price,DateTime.Now);

            _unitOfWork.GetRepository<Plan>().Update(plan);
            return _unitOfWork.SaveChanges()>0;

        }

        #region Helper functions

        bool HasActiveMemberShip(int id)
        {
            return _unitOfWork.GetRepository<MemberShip>().GetAll(m=>m.PlanId==id &&  m.Status=="Active").Any();
        }
        #endregion
    }
}
