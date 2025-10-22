using AutoMapper;
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
    public class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PlanService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var plans = _unitOfWork.GetRepository<Plan>().GetAll();
            if (plans == null || !plans.Any()) return [];

            return _mapper.Map<IEnumerable<PlanViewModel>>(plans);

            #region Manual Mapping

            //return plans.Select(p => new PlanViewModel
            //{
            //    Name = p.Name,
            //    Description = p.Description,
            //    DurationDays = p.DurationDays,
            //    Price = p.Price,
            //    IsActive = p.IsActive,

            //}).ToList(); 
            #endregion

        }

        public PlanViewModel? GetPlan(int id)
        {
            var plan=_unitOfWork.GetRepository<Plan>().GetById(id);
            if (plan is null) return null;
          return  _mapper.Map<PlanViewModel>(plan);

            #region Manual Mapping
            //return new PlanViewModel()
            //{
            //    Name = plan.Name,
            //    Description = plan.Description,
            //    DurationDays = plan.DurationDays,
            //    Price = plan.Price,
            //    IsActive = plan.IsActive,
            //}; 
            #endregion
        }

        public PlanToUpdateViewModel? GetPlanToUpdate(int id)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(id);
            if(plan is null || plan.IsActive == false|| HasActiveMemberShip(id)) return null;
           return _mapper.Map<PlanToUpdateViewModel>(plan);

            #region Manual Mapping
            //return new PlanToUpdateViewModel()
            //{
            //    Name = plan.Name,
            //    Description = plan.Description,
            //    DurationDays = plan.DurationDays,
            //    Price = plan.Price,

            //}; 
            #endregion
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

        public bool UpdatePlanDetails(int id, PlanToUpdateViewModel UpdatedPlan)
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
