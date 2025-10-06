using GYMManagementDL.Enitities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementDL.Enitities
{
    internal class GymUser: BaseEntity
    {
        public string Name { get; set; }=null!;
        public string Email { get; set; }=null!;
        public string PhoneNumber { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public string Password { get; set; } = null!;
        public Gender Gender { get; set; }
        public Address Address { get; set; } = null!;
    }

    [Owned]
    class Address
    {
       public int BuildingNo { get; set; }
       public string Street { get; set; } = null!;
       public string City { get; set; } = null!;


    }
}
