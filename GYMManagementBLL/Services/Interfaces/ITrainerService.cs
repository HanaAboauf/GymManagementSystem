using GYMManagementBLL.ViewModel;
using GYMManagementBLL.ViewModel.MemberViewModels;
using GYMManagementBLL.ViewModel.TrainerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementBLL.Services.Interfaces
{
    internal interface ITrainerService
    {
        IEnumerable<TrainerViewModel> GetAllTrainers();

        bool CreateTrainer(CreateTrainerViewModel CreatedTrainer);

        TrainerViewModel? GetTrainerDetails(int TrainerId);

        TrainerToUpdateViewModel? GetTrainerToUpDate(int TrainerId);

        bool UpdateTrainerDetails(int TrainerId, TrainerToUpdateViewModel updatedTrainer);

        bool RemoveTrainer(int TrainerId);
    }
}
