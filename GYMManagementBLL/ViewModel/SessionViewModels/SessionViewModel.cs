using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementBLL.ViewModel.SessionViewModels
{
    internal class SessionViewModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }=null!;
        public string Description { get; set; } =null!;
        public string TrainerName { get; set; } = null!;
        public int Capacity { get; set; }
        public int AvailableSlots { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        #region Computed Property

        public string DisplayedDate => $"{ StartDate :MMM dd, yyy}";

        public string TimeRangeDisplayed => $"{StartDate:hh:mm tt} - {EndDate:hh:mm tt}";

        public TimeSpan Duration=> EndDate- StartDate;

        public string Status
        {
            get
            {
                if (StartDate > DateTime.Now) return "Upcoming";
                else if (StartDate<= DateTime.Now && EndDate > DateTime.Now) return "Ongoing";
                else return "Completed";
            }
        }

        #endregion

    }
}
