using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementDL.Enitities
{
    public class MemberSession: BaseEntity
    {

        //BookingDate= CreatedAt in the BaseEntity

        public bool IsAttended { get; set; }


        #region RelationShips

        #region MemberSession - Member
        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;
        #endregion

        #region MemberSession - Session
        public int SessionId { get; set; }
        public Session Session { get; set; } = null!;  
        #endregion

        #endregion
    }
}
