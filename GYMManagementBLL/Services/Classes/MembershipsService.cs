using AutoMapper;
using AutoMapper.Execution;
using GYMManagementBLL.Services.Interfaces;
using GYMManagementBLL.ViewModel;
using GYMManagementBLL.ViewModel.MembershipsViewModels;
using GYMManagementDL.Enitities;
using GYMManagementDL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Member = GYMManagementDL.Enitities.Member;

namespace GYMManagementBLL.Services.Classes
{
    public class MembershipsService : IMembershipsService
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IMapper _Mapper;

        public MembershipsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            _Mapper = mapper;
        }

        public IEnumerable<MembershipsViewModel> GetAllActiveMemberships()
        {
            var ActiveMemberships = _UnitOfWork.MembershipRepository.GetMembershipsWithMemberAndPlan();
            if (ActiveMemberships is null || !ActiveMemberships.Any()) return [];

            return _Mapper.Map<IEnumerable<MembershipsViewModel>>(ActiveMemberships);

        }

        public bool CreateMembership(CreateMembershipViewModel createdMembership)
        {
            try
            {
                if (!MemberIsExists(createdMembership.MemberId) || !PlanIsExists(createdMembership.PlanId)) return false;
                if (MemberHasMemberShip(createdMembership.MemberId)) return false;
                if (!PlanIsActive(createdMembership.PlanId)) return false;
               var MeppedMembership= _Mapper.Map<MemberShip>(createdMembership);
                _UnitOfWork.GetRepository<MemberShip>().Add(MeppedMembership);


                return _UnitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Can't create this membership {ex.Message}");
                return false;

            }
        }

        public bool DeleteMembership(int membershipId)
        {
            try
            {
                var membership = _UnitOfWork.GetRepository<MemberShip>().GetById(membershipId);
                if (membership is null || membership.Status != "Active") return false;
                _UnitOfWork.GetRepository<MemberShip>().Delete(membership);
                return _UnitOfWork.SaveChanges() > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Can't delete this membership {ex}");
                return false;
            }
        }
        public IEnumerable<PlanDropDownViewModel> GetAllPlansForDropDown()
        {
            var Plans = _UnitOfWork.GetRepository<Plan>().GetAll();
            if (Plans is null || !Plans.Any()) return [];
            return _Mapper.Map<IEnumerable<PlanDropDownViewModel>>(Plans);
        }

        public IEnumerable<MemberDropDownViewModel> GetAllMemberssForDropDown()
        {
            var Members = _UnitOfWork.GetRepository<Member>().GetAll();
            if (Members is null || !Members.Any()) return [];
            return _Mapper.Map<IEnumerable<MemberDropDownViewModel>>(Members);
        }


        #region Helper functions

        bool MemberIsExists(int memberId)
        {
            var member = _UnitOfWork.GetRepository<Member>().GetById(memberId);
            return member is not null;
        }
        bool PlanIsExists(int planId)
        {
            var plan = _UnitOfWork.GetRepository<Plan>().GetById(planId);
            return plan is not null;
        }

        bool MemberHasMemberShip(int memberId)
        {
            var membership = _UnitOfWork.GetRepository<MemberShip>().GetAll(ms => ms.MemberId == memberId);
            return membership ==null;
        }

        bool PlanIsActive(int planId)
        {
            var plan = _UnitOfWork.GetRepository<Plan>().GetById(planId);
            return plan is not null && plan.IsActive;
        }
        #endregion
    }
}
