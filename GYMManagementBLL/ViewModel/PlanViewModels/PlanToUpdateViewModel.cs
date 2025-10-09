using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementBLL.ViewModel.PlanViewModels
{
    internal class PlanToUpdateViewModel
    {
        [Required(ErrorMessage ="Plan Name is required")]
        [StringLength(maximumLength:50,ErrorMessage ="Plan name lenght must be less than 51 chars ")]

        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(maximumLength: 200, ErrorMessage = "Plan name lenght must be less than 51 chars ")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Duration Days is required")]
        [Range(1,365,ErrorMessage = "Duration Days  must be between 1 and 365 days")]
        public int DurationDays { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.1,10000,ErrorMessage = "Price must be between 0.1 and 10000$ ")]
        public decimal Price { get; set; }
    }
}
