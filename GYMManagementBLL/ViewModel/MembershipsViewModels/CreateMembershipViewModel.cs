using System.ComponentModel.DataAnnotations;

namespace GYMManagementBLL.ViewModel.MembershipsViewModels
{
    public class CreateMembershipViewModel
    {
        [Required(ErrorMessage ="Member Id is required")]
        [Display(Name ="Member")]
        public int MemberId { get; set; }

        [Required(ErrorMessage ="Plan Id is required")]
        [Display(Name = "Plan")]
        public int PlanId { get; set; }

    }
}