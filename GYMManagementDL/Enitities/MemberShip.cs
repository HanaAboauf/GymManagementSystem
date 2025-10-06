using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementDL.Enitities
{
    internal class MemberShip: BaseEntity
    {
        #region Attributes
        // Membership start date = CreatedAt in the BaseEntity

        public DateTime EndDate { get; set; }

        public string Status
        {
            get
            {
                return DateTime.Now >= EndDate ? "Expired" : "Active";
            }
        }
        #endregion

        #region Relationships

        #region MemberShip-Member realtionship

        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;
        #endregion

        #region MemberShip-Plan
        public int PlanId { get; set; }
        public Plan Plan { get; set; } = null!;  
        #endregion

        #endregion

    }
}
