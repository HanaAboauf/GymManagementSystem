using AutoMapper;
using GYMManagementBLL.Services.Interfaces;
using GYMManagementBLL.ViewModel.MemberViewModels;
using GYMManagementBLL.ViewModel.TrainerViewModels;
using GYMManagementDL.Enitities;
using GYMManagementDL.Repositories.Classes;
using GYMManagementDL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementBLL.Services.Classes
{
    internal class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TrainerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public bool CreateTrainer(CreateTrainerViewModel CreatedTrainer)
        {
            if(IsEmailExists(CreatedTrainer.Email)|| IsPhoneExists(CreatedTrainer.PhoneNumber)) return false;

            #region Manuall mapping
            //var trainer = new Trainer()
            //{
            //    Name = CreatedTrainer.Name,
            //    Email = CreatedTrainer.Email,
            //    PhoneNumber = CreatedTrainer.PhoneNumber,
            //    Gender = CreatedTrainer.Gender,
            //    Specialization = CreatedTrainer.Specialization,
            //    DateOfBirth = CreatedTrainer.DateOfBirth,
            //    Address = new Address()
            //    {
            //        BuildingNo = CreatedTrainer.BuildingNumber,
            //        Street = CreatedTrainer.Street,
            //        City = CreatedTrainer.City,
            //    }

            //}; 
            #endregion
            var trainer = _mapper.Map<Trainer>(CreatedTrainer);
            _unitOfWork.GetRepository<Trainer>().Add(trainer);
            return _unitOfWork.SaveChanges()>0;
            
        }

        public IEnumerable<TrainerViewModel> GetAllTrainers()
        {
            var Trainers = _unitOfWork.GetRepository<Trainer>().GetAll();

            if (Trainers is null || Trainers.Any()) return [];

            return _mapper.Map<IEnumerable<TrainerViewModel>>(Trainers);

            #region Manuall mapping
            //return Trainers.Select(t => new TrainerViewModel()
            //{
            //    Name = t.Name,
            //    Email = t.Email,
            //    Phone = t.PhoneNumber,
            //    Specialization = t.Specialization.ToString(),
            //}).ToList(); 
            #endregion
        }

        public TrainerViewModel? GetTrainer(int TrainerId)
        {
            try
            {
                var trainer = _unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
                if (trainer is null) return null;
                return _mapper.Map<TrainerViewModel>(trainer);
                #region Manuall mapping
                //return new TrainerViewModel()
                //{
                //    Name = trainer.Name,
                //    Email = trainer.Email,
                //    Phone = trainer.PhoneNumber,
                //    Specialization = trainer.Specialization.ToString() + " Trainer",
                //    DateOfBirth = trainer.DateOfBirth.ToShortDateString(),
                //    Address = $"{trainer.Address.BuildingNo} - {trainer.Address.Street} - {trainer.Address.City}",
                //}; 
                #endregion
            }
            catch
            {
                return null;
            }

        }

        public TrainerToUpdateViewModel? GetTrainerToUpDate(int TrainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
            if (trainer is null) return null;
            return _mapper.Map<TrainerToUpdateViewModel>(trainer);
            #region Manuall Mapping
            //return new TrainerToUpdateViewModel()
            //{
            //    Name = trainer.Name,
            //    Email = trainer.Email,
            //    Phone = trainer.PhoneNumber,
            //    BuildingNumber = trainer.Address.BuildingNo,
            //    Street = trainer.Address.Street,
            //    City = trainer.Address.City,
            //}; 
            #endregion
        }

        public bool UpdateTrainerDetails(int TrainerId, TrainerToUpdateViewModel updatedTrainer)
        {
            try
            {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(TrainerId);

            if (trainer is null || trainer.Id!=TrainerId) return false;

            _mapper.Map(updatedTrainer, trainer);

                #region Manuall mapping
                //trainer.Email = updatedTrainer.Email;
                //trainer.PhoneNumber = updatedTrainer.Phone;
                //trainer.Address.BuildingNo = updatedTrainer.BuildingNumber;
                //trainer.Address.Street = updatedTrainer.Street;
                //trainer.Address.City = updatedTrainer.City;
                //trainer.Specialization = updatedTrainer.Specialization;
                //trainer.UpdatedAt = DateTime.Now; 
                #endregion
                _unitOfWork.GetRepository<Trainer>().Update(trainer);
               return _unitOfWork.SaveChanges()>0;
            }
            catch
            {
                return false;
            }
            

           
        }
        public bool RemoveTrainer(int TrainerId)
        {
            try
            {
                var trainer = _unitOfWork.GetRepository<Trainer>().GetById(TrainerId);

                if (trainer is null || trainer.Id != TrainerId) return false;

                var FutureSession = _unitOfWork.GetRepository<Session>().GetAll(s => s.TrainerId == TrainerId && s.StartTime > DateTime.Now).Any();

                if (FutureSession) return false;

                _unitOfWork.GetRepository<Trainer>().Delete(trainer);

                return _unitOfWork.SaveChanges() > 0;


            }
            catch
            {
                return false;
            }



        }


        #region Helper Functions

        bool IsEmailExists(string email) => _unitOfWork.GetRepository<Trainer>().GetAll(x => x.Email == email).Any();
        bool IsPhoneExists(string phone) => _unitOfWork.GetRepository<Trainer>().GetAll(x => x.PhoneNumber == phone).Any();


        #endregion
    }
}
