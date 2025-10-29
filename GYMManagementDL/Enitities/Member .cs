using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementDL.Enitities
{
    public class Member:GymUser
    {
        // JoinDate= CreatedAt in the BaseEntity
        public string Photo { get; set; }=null!;

        #region Relationships

        #region Member-Health relationship

        public virtual HealthRecord HealthRecord { get; set; }= null!;
        #endregion

        #region Member-MemberShip relationship
        public  ICollection<MemberShip> MemberShips { get; set; }= null!;
        #endregion

        #region Member - MemberSession relationship
        public ICollection<MemberSession> MemberSessions { get; set; }= null!;
        #endregion


        #endregion
    }
}
