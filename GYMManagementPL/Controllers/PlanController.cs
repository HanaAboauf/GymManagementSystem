using GYMManagementBLL.Services.Interfaces;
using GYMManagementBLL.ViewModel.PlanViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GYMManagementPL.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanService _PlanService;

        public PlanController(IPlanService planService)
        {
            _PlanService = planService;
        }

        #region Get Plan
        public ActionResult Index()
        {
            var plans = _PlanService.GetAllPlans();
            return View(plans);
        } 

        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id should be greater than zero";
                return RedirectToAction(nameof(Index));
            }
            var planDetails = _PlanService.GetPlan(id);
            if (planDetails == null)
            {
                TempData["ErrorMessage"] = "Plan not found";
                return RedirectToAction(nameof(Index));
            }
            return View(planDetails);
        }
        #endregion

        #region Edit Plan

        public ActionResult Edit(int id) {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id should be greater than zero";
                return RedirectToAction(nameof(Index));
            }
            var planToUpdate = _PlanService.GetPlanToUpdate(id);
            if (planToUpdate == null)
            {
                TempData["ErrorMessage"] = "Plan not found";
                return RedirectToAction(nameof(Index));
            }
            return View(planToUpdate);
        }
        [HttpPost]
        public ActionResult Edit(int id,PlanToUpdateViewModel updatedPlan)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("WrongData", "Please correct the errors and try again.");
                return View(updatedPlan);
            }
            var isUpdated = _PlanService.UpdatePlanDetails(id, updatedPlan);
            if (!isUpdated)
            {
                TempData["ErrorMessage"] = "Failed to update plan. Please try again.";
                return View(updatedPlan);
            }
            TempData["SuccessMessage"] = "Plan updated successfully.";
            return RedirectToAction(nameof(Index));

        }

        public ActionResult Toggle(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id should be greater than zero";
                return RedirectToAction(nameof(Index));
            }
            var isToggled = _PlanService.TogglePlanStatus(id);
            if (!isToggled)          
                TempData["ErrorMessage"] = "Failed to toggle plan status. Please try again.";

            else
                TempData["SuccessMessage"] = "Plan status toggled successfully.";
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
