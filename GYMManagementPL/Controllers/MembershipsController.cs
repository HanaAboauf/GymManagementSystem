using GYMManagementBLL.Services.Classes;
using GYMManagementBLL.Services.Interfaces;
using GYMManagementBLL.ViewModel.MembershipsViewModels;
using GYMManagementDL.Enitities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GYMManagementPL.Controllers
{
    public class MembershipsController : Controller
    {
        private readonly IMembershipsService _MembershipsService;

        public MembershipsController(IMembershipsService membershipsService) {
            _MembershipsService = membershipsService;
        }
        public IActionResult Index()
        {
            var memberships = _MembershipsService.GetAllActiveMemberships(); 
            return View(memberships);
        }

        #region Create membership

        public IActionResult Create()
        {
            GetMemberDropdown();
            GetPlanDropdown();
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateMembershipViewModel createdMembership)
        {
            if (!ModelState.IsValid)
            {
                GetPlanDropdown();
                GetMemberDropdown();
                TempData["ErrorMessage"] = "Check your data";
                return View(createdMembership);
            }

            var res = _MembershipsService.CreateMembership(createdMembership);

            if (!res)
            {
                GetPlanDropdown();
                GetMemberDropdown();
                TempData["ErrorMessage"] = "Failed to create membership";
                return View(createdMembership);
            }

            TempData["SuccessMessage"] = "Membership created successfully";
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete membership

        [HttpPost]
        public IActionResult Delete([FromForm] int id)
        {
            if(id <= 0)
            {
                TempData["ErrorMessage"] = "id must be greater than zero";
                return RedirectToAction(nameof(Index));

            }
           
           var res= _MembershipsService.DeleteMembership(id);
            if (!res)
                TempData["ErrorMessage"] = "Failed to delete membership";

            TempData["SuccessMessage"] = "Membershipe deleted successfully";
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Load Member and Plans

        private void GetPlanDropdown()
        {
            var plans = _MembershipsService.GetAllPlansForDropDown() ;
            ViewBag.plans = new SelectList(plans, "Id", "Name");
        }

        private void GetMemberDropdown()
        {
            var members = _MembershipsService.GetAllMemberssForDropDown();
            ViewBag.members = new SelectList(members, "Id", "Name");
        }
        #endregion

    }
}
