using GYMManagementBLL.ViewModel.SessionViewModels;
using GYMManagementBLL.ViewModel.TrainerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementBLL.Services.Interfaces
{
    public interface ISessionService
    {
        IEnumerable<SessionViewModel> GetAllSessions();

        SessionViewModel? GetSessionById(int id);

        bool CreateSession(CreateSessionViewModel session);

        SessionToUpdateViewModel? GetSessionToUpdate(int sessionId);

        bool UpdateSession(int sessionId, SessionToUpdateViewModel UpdatedSession);

        bool RemoveSession(int sessionId);
        IEnumerable<TrainerDropDownViewModel> GetTrainerDropDownList();
        IEnumerable<CategoryDropDownViewModel> GetCategoryDropDownList();
    }
}
