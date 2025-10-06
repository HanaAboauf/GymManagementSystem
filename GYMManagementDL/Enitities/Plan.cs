﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementDL.Enitities
{
    internal class Plan: BaseEntity
    {
        public string Name { get; set; }=null!;
        public string Description { get; set; }=null!;
        public int DurationDays { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }

        #region Relationships  

        #region Plan - MemberShip relationship

        public ICollection<MemberShip> MemberShips { get; set; }= null!;
        #endregion



        #endregion
    }
}
