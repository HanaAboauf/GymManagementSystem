using GYMManagementBLL.Services.Interfaces;
using GYMManagementBLL.ViewModel.SessionViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GYMManagementPL.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _SessionService;

        public SessionController(ISessionService sessionService)
        {
            _SessionService = sessionService;
        }

        #region Get session
        public ActionResult Index()
        {
            var sessions = _SessionService.GetAllSessions();
            return View(sessions);
        }

        public ActionResult Details(int id)
        {
            if (id <= 0)
            {

                TempData["ErrorMessage"] = "Id should be greater than zero";
                return RedirectToAction(nameof(Index));
            }
            var session = _SessionService.GetSessionById(id);

            if (session == null)
            {
                TempData["ErrorMessage"] = "Id should be greater than zero";
                return RedirectToAction(nameof(Index));
            }
            return View(session);
        }
        #endregion

        #region Create session

        public ActionResult Create()
        {
            GetTrainerDropdown();
            GetCategoryDropdown();
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateSessionViewModel createdSession)
        {
            if (!ModelState.IsValid)
            {

                ModelState.AddModelError("DataInValid", "Check your data and missing field");
                return View(createdSession);
            }
            var isCreated = _SessionService.CreateSession(createdSession);
            if (!isCreated)
            {
                GetTrainerDropdown();
                GetCategoryDropdown();

                TempData["ErrorMessage"] = "Session creation failed";
                return View(createdSession);
            }
            TempData["SuccessMessage"] = "Session created successfully";
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Edit session 

        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id should be greater than zero";
                return RedirectToAction(nameof(Index));
            }
            var session = _SessionService.GetSessionToUpdate(id);
            if (session == null)
            {
                TempData["ErrorMessage"] = "Session not found";
                return RedirectToAction(nameof(Index));
            }
            GetTrainerDropdown();
            return View(session);
        }

        [HttpPost]
        public ActionResult Edit(int id, SessionToUpdateViewModel updatedSession)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInValid", "Check your data and missing field");
                return View(updatedSession);
            }
            var isUpdated = _SessionService.UpdateSession(id, updatedSession);
            if (!isUpdated)
            {
                GetTrainerDropdown();
                TempData["ErrorMessage"] = "Session update failed";
                return View(updatedSession);
            }
            TempData["SuccessMessage"] = "Session updated successfully";
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete session

        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id should be greater than zero";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.id = id;
            return View();
        }
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id should be greater than zero";
                return RedirectToAction(nameof(Index));
            }
            var session = _SessionService.GetSessionById(id);
            if (session == null)
            {
                TempData["ErrorMessage"] = "Session not found";
                return RedirectToAction(nameof(Index));
            }
            var isDeleted = _SessionService.RemoveSession(id);
            if (!isDeleted)
            {
                TempData["ErrorMessage"] = "Session deletion failed";
                return RedirectToAction(nameof(Index));
            }   
            TempData["SuccessMessage"] = "Session deleted successfully";
            return View(session);
        }
        #endregion

        #region Helper finctions

        private void GetTrainerDropdown()
        {
            var trainers = _SessionService.GetTrainerDropDownList();
            ViewBag.Trainers = new SelectList(trainers, "Id", "Name");
        }
        private void GetCategoryDropdown()
        {
            var categories = _SessionService.GetCategoryDropDownList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
        }
        #endregion
    }
}
