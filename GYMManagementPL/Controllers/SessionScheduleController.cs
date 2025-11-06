using GYMManagementBLL.Services.Interfaces;
using GYMManagementDL.Enitities;
using Microsoft.AspNetCore.Mvc;

namespace GYMManagementPL.Controllers
{
    public class SessionScheduleController : Controller
    {
        private readonly IMemberScheduleService _MemberScheduleService;

        public SessionScheduleController(IMemberScheduleService memberScheduleService)
        {
            _MemberScheduleService = memberScheduleService;
        }

        public IActionResult Index()
        {
            var allBookingSessions=_MemberScheduleService.GetUpcomingAndOngoingSessions();
            return View(allBookingSessions);
        }

        public IActionResult GetMembersForUpcomingSession(int sessionId)
        {
            if (sessionId == 0) {
                TempData["ErrorMessage"] = "Session Id must be greater than zero";
                return RedirectToAction("Index");
            }
            var members = _MemberScheduleService.GetMembersForUpcomingSession(sessionId);
            return View(members);

        }

        public IActionResult GetMembersForOngoingSessions(int sessionId)
        {
            if (sessionId == 0)
            {
                TempData["ErrorMessage"] = "Session Id must be greater than zero";
                return RedirectToAction("Index");
            }
            var members = _MemberScheduleService.GetMembersForOngoingSession(sessionId);
            return View(members);
        }
    }
}
