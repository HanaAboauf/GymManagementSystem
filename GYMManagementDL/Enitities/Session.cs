using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementDL.Enitities
{
    public class Session: BaseEntity  
    {
        public string Description { get; set; }=null!;
        public int Capacity { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        #region Relationships

        #region Session-Category relationship
        
        public int CategoryId { get; set; }
        public Category Category { get; set; }=null!;
        #endregion

        #region Session-Trainer relationship

        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; } = null!;
        #endregion

        #region Session-MemberSession relationship
        public ICollection<MemberSession> MemberSessions { get; set; }= null!;
        #endregion
        #endregion
    }
}
