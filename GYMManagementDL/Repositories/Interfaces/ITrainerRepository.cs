using GYMManagementDL.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementDL.Repositories.Interfaces
{
    internal interface ITrainerRepository
    {
        public IEnumerable<Trainer> GetAllTrainers();

        public Trainer? GetTrainerById(int id);

        public int AddTrainer(Trainer member);

        public int UpdateTrainer(Trainer member);

        public int DeleteTrainer(int id);
    }
}
