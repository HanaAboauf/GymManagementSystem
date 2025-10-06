using GYMManagementDL.Enitities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementDL.Enitities
{
    internal class Trainer: GymUser
    {
        //HireDate= CreatedAt in the BaseEntity

        public Specialities Specialization { get; set; }

        public ICollection<Session> Sessions { get; set; } = null!;
    }
}
