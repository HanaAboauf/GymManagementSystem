using GYMManagementDL.Data.Contexts;
using GYMManagementDL.Enitities;
using GYMManagementDL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementDL.Repositories.Classes
{
    internal class TrainerRepository : ITrainerRepository
    {
      private readonly  GymManagementDbContext dbContext=new GymManagementDbContext();
        public int AddTrainer(Trainer member)
        {
            dbContext.Trainers.Add(member);
            return dbContext.SaveChanges();
        }

        public int DeleteTrainer(int id)
        {
            var trainer = dbContext.Trainers.Find(id);
            if (trainer is null) return 0;
            dbContext.Trainers.Remove(trainer);
            return dbContext.SaveChanges();
        }

        public IEnumerable<Trainer> GetAllTrainers()=> dbContext.Trainers.ToList();

        public Trainer? GetTrainerById(int id)=> dbContext.Trainers.Find(id);

        public int UpdateTrainer(Trainer member)
        {
            dbContext.Trainers.Update(member);  
            return dbContext.SaveChanges();
        }
    }
}
