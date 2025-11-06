using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementBLL.ViewModel.MemberScheduleViewModels
{
    public class CreateMemberSessionViewModel
    {
        [Required(ErrorMessage ="Member id is required")]
        [Display(Name ="Member")]
        public int MemberId { get; set; }
        public int SessionId { get; set; }

    }
}
