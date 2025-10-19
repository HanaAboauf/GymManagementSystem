using GYMManagementBLL.Services.Interfaces;
using GYMManagementBLL.ViewModel.MemberViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GYMManagementPL.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        #region Get All Members
        public ActionResult Index()
        {
            var Members = _memberService.GetAllMembers();
            return View(Members);
        }
        #endregion


        #region Get Member Details

        public ActionResult GetMemberDetail(int id)
        {
            if (id <= 0) {
                TempData["ErrorMessage"] = "Id should be greater than zero";
                return RedirectToAction(nameof(Index));
            }

            var memberDetails=_memberService.GetMemberDetails(id);
            if (memberDetails == null){
                TempData["ErrorMessage"] = "Member not found";
                return RedirectToAction(nameof(Index));
            }

            return View(memberDetails);
        }


        #endregion

        #region Get Health Record

        public ActionResult GetMemberHealthRecord(int id)
        {
            if (id <= 0){
                TempData["ErrorMessage"] = "Id should be greater than zero";
                return RedirectToAction(nameof(Index));
            }
            var healthRecord = _memberService.GetMemberHealthRecord(id);
            if (healthRecord == null){
                TempData["ErrorMessage"] = "Health Record not found";
                return RedirectToAction(nameof(Index));
            }
            return View(healthRecord);
        }
        #endregion

        #region Create Member
        public ActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateMember(CreateMemberViewModel createdMember)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInValid","Check your data and missing field");
                return View(nameof(Create), createdMember);
            }
            var res = _memberService.CreateMember(createdMember);
            if (res)
                TempData["SuccessMessage"] = "Member created successfully";         
            else
                ModelState.AddModelError("ErrorMessage", "Failed to create member,check your phone or e-mail");

            return RedirectToAction(nameof(Index));

        }
        #endregion

        #region Edit Member

        public ActionResult EditMember(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id should be greater than zero";
                return RedirectToAction(nameof(Index));
            }
            var member = _memberService.GetMemberToUpDate(id);
            if (member == null)
            {
                TempData["ErrorMessage"] = "Member not found";
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        [HttpPost]
        public ActionResult EditMember([FromRoute]int id, MemberToUpDateViewModel updatedMember)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id should be greater than zero";
                return View(nameof(EditMember));
            }
            var res = _memberService.UpdateMemberDetails(id,updatedMember);
            if (res)
            {
                TempData["SuccessMessage"] = "Member updated successfully";
            }
            else
            {
                TempData["ErrorMessage"]= " Member Failed updated Failed to update";
            }
            return RedirectToAction(nameof(Index));

        }
        #endregion

        #region Remove Member

        public ActionResult RemoveMember(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id should be greater than zero";
                return RedirectToAction(nameof(Index));
            }

            var memberDetails = _memberService.GetMemberDetails(id);
            if (memberDetails == null)
            {
                TempData["ErrorMessage"] = "Member not found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.id=memberDetails.Id;
            ViewBag.name=memberDetails.Name;
            return View();

        }

        public ActionResult DeleteConfirmed([FromForm] int id) { 
            var res = _memberService.RemoveMember(id);  
            if (res)
            {
                TempData["SuccessMessage"] = "Member removed successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to remove member";
            }
            return RedirectToAction(nameof(Index));


        }
        #endregion
    }
}
