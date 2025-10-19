using GYMManagementBLL.Services.Interfaces;
using GYMManagementBLL.ViewModel.TrainerViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GYMManagementPL.Controllers
{
    public class TrainerController : Controller
    {
        private readonly ITrainerService _trainerService;

        public TrainerController(ITrainerService trainerService)
        {
           _trainerService = trainerService;
        }
        #region Get Trainer

        public ActionResult Index()
        {
            var trainers = _trainerService.GetAllTrainers();

            return View(trainers);
        }
        public ActionResult GetTrainerDetail(int id) { 
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id should be greater than zero";
                return RedirectToAction(nameof(Index));
            }
            var trainerDetails = _trainerService.GetTrainerDetails(id);
            if (trainerDetails == null)
            {
                TempData["ErrorMessage"] = "Trainer not found";
                return RedirectToAction(nameof(Index));
            }
            return View(trainerDetails);
        }
        #endregion

        #region Create Trainer

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTrainer(CreateTrainerViewModel createdTrainer)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInValid", "Check your data and missing field");
                return View(nameof(Create), createdTrainer);
            }
            var isCreated = _trainerService.CreateTrainer(createdTrainer);
            if (!isCreated)
            {
               ModelState.AddModelError("CreateFailed", "Failed to create trainer, check your phone or email");
                return View(nameof(Create),createdTrainer);
            }
            TempData["SuccessMessage"] = "Trainer created successfully.";

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Edit Trainer

        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id should be greater than zero";
                return RedirectToAction(nameof(Index));
            }
            var trainerDetails = _trainerService.GetTrainerToUpDate(id);
            if (trainerDetails == null)
            {
                TempData["ErrorMessage"] = "Trainer not found";
                return RedirectToAction(nameof(Index));
            }
            return View(trainerDetails);
        }

        [HttpPost]
        public ActionResult Edit([FromRoute] int id, TrainerToUpdateViewModel updatedTrainer)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInValid", "Check your data and missing field");
                return View(nameof(Edit), updatedTrainer);
            }
            var isUpdated = _trainerService.UpdateTrainerDetails(id, updatedTrainer);

            if (!isUpdated)
                TempData["ErrorMessage"] = "Failed to update trainer details.";
            else
                TempData["SuccessMessage"] = "Trainer updated successfully.";

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Remove Trainer

        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id should be greater than zero";
                return RedirectToAction(nameof(Index));
            }
            var trainer = _trainerService.GetTrainerDetails(id);
            if (trainer is null){
                TempData["ErrorMessage"] = "Trainer not found";
               return RedirectToAction(nameof(Index));
            }
               ViewBag.id=trainer.Id;
               ViewBag.name=trainer.Name;
            return View();

        }
        [HttpPost]
        public ActionResult DeleteConfirmed([FromRoute] int id)
        {
            var isDeleted = _trainerService.RemoveTrainer(id);
            if (!isDeleted)
                TempData["ErrorMessage"] = "Failed to delete trainer.";
            else
                TempData["SuccessMessage"] = "Trainer deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
