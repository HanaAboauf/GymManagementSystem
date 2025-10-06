using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementDL.Enitities
{
    internal class Member:GymUser
    {
        // JoinDate= CreatedAt in the BaseEntity
        public string? photo { get; set; }
    }
}
