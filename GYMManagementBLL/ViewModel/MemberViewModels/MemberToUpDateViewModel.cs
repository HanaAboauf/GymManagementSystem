using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementBLL.ViewModel.MemberViewModels
{
    public class MemberToUpDateViewModel
    {
        public string Name { get; set; } = null!;
        public string? Photo { get; set; }


        #region Email property
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [DataType(DataType.EmailAddress)] //UI hint
        //[RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format")]
        [StringLength(maximumLength: 100, MinimumLength = 5, ErrorMessage = "The email lenght must be between 5 and 100 charactars")]
        public string Email { get; set; } = null!;
        #endregion

        #region PhoneNumber property
        [Required(ErrorMessage = "phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Ohone number must be valid Egyption number")]
        [DataType(DataType.PhoneNumber)] //UI hint  
        public string Phone { get; set; } = null!;
        #endregion


        [Required(ErrorMessage = "BuildingNumber is required")]
        [Range(1, 9000, ErrorMessage = "Building Number must be between 1,9000")]
        public int BuildingNumber { get; set; }

        [Required(ErrorMessage = "Street is required")]
        [StringLength(maximumLength: 30, MinimumLength = 2, ErrorMessage = "Street lenght must be between 2 and 30 chars")]
        public string Street { get; set; } = null!;

        [Required(ErrorMessage = "City is required")]
        [StringLength(maximumLength: 30, MinimumLength = 2, ErrorMessage = "City lenght must be between 2 and 30 chars")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The name must contain only letters and spaces")]
        public string City { get; set; } = null!;

    }
}
