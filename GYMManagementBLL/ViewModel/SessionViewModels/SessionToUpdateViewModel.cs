using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementBLL.ViewModel.SessionViewModels
{
    public class SessionToUpdateViewModel
    {
        [Required(ErrorMessage = "Trainer is required")]
        [Display(Name = "Trainer")]
        public int TrainerId { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Description")]
        [StringLength(500,MinimumLength =10,ErrorMessage ="Discription must be between 10 and 500")]
        public string Description { get; set; } = null!;


        [Required(ErrorMessage = "StartTime is required")]
        [Display(Name = "Start Date & Time")]
        public DateTime StartTime { get; set; }
        [Required(ErrorMessage = "EndTime is required")]
        [Display(Name = "End Date & Time")]
        public DateTime EndTime { get; set; }
    }
}
