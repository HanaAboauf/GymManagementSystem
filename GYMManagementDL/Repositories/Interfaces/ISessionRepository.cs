using GYMManagementDL.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementDL.Repositories.Interfaces
{
    public interface ISessionRepository:IGenericRepository<Session>
    {
       IEnumerable<Session> GetAllSessionsWithTrainerAndCategory();

        int GetBookedSlots(int bookedSlots);

        Session? GetSessionWithTrainerAndCategory(int sessId);
    }
}
