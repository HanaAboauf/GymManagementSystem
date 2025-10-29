using AutoMapper;
using GYMManagementBLL.Services.AttachmentService;
using GYMManagementBLL.Services.Interfaces;
using GYMManagementBLL.ViewModel.MemberViewModels;
using GYMManagementDL.Enitities;
using GYMManagementDL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementBLL.Services.Classes
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWok;
        private readonly IPlanRepository _planRepository;
        private readonly IMapper _mapper;
        private readonly IAttachmentService _AttachmentService;

        public MemberService(IUnitOfWork unitOfWok,IPlanRepository planRepository, IMapper mapper, IAttachmentService attachmentService)
        {
           _unitOfWok = unitOfWok;
           _planRepository = planRepository;
           _mapper = mapper;
            _AttachmentService = attachmentService;
        }


        public bool CreateMember(CreateMemberViewModel createdMember)
        {
            try
            {

                if (IsEmailExists(createdMember.Email) || IsPhoneExists(createdMember.Phone)) return false;

                var photoFileName = _AttachmentService.Upload("members", createdMember.PhotoFile!);

                #region Mauall mapping
                //    var member=new Member()
                //{
                //    Name = createdMember.Name,
                //    Email = createdMember.Email,
                //    PhoneNumber = createdMember.Phone,
                //    Gender = createdMember.Gender,
                //    DateOfBirth = createdMember.DateOfBirth,
                //    Address = new Address()
                //    {
                //        BuildingNo = createdMember.BuildingNumber,
                //        City = createdMember.City,
                //        Street = createdMember.Street,
                //    },
                //    HealthRecord = new HealthRecord()
                //    {
                //        Height = createdMember.HealthRecord.Height,
                //        Weight = createdMember.HealthRecord.Weight,
                //        BloodType = createdMember.HealthRecord.BloodType,
                //        Note = createdMember.HealthRecord.Note,

                //    }

                //}; 
                #endregion

                var member = _mapper.Map<Member>(createdMember);
                member.Photo = photoFileName!;
                _unitOfWok.GetRepository<Member>().Add(member);

                var isUploaded= _unitOfWok.SaveChanges()>0;
                if (isUploaded)
                {
                    return true;
                }
                else
                {
                    _AttachmentService.Delete("members", photoFileName!);
                    return false;
                }

            }
            catch { 
                return false;
            }

        }


        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var Members = _unitOfWok.GetRepository<Member>().GetAll();
            if (Members is null || !Members.Any()) return [];

            return _mapper.Map<IEnumerable<Member>, IEnumerable<MemberViewModel>>(Members);

            #region ManuallMapping
            //return Members.Select(m => new MemberViewModel
            //{
            //    Id = m.Id,
            //    Name = m.Name,
            //    Email = m.Email,
            //    PhoneNumber = m.PhoneNumber,
            //    Gender=m.Gender.ToString(),
            //    Photo=m.Photo,
            //}).ToList(); 
            #endregion
        }

        public MemberViewModel? GetMemberDetails(int MemberId)
        {
            try
            {
                var Member = _unitOfWok.GetRepository<Member>().GetById(MemberId);

                if (Member == null) return null;
                #region Manuall Mapping
                //var memberViewModel = new MemberViewModel()
                //{
                //    Name = Member.Name,
                //    Email = Member.Email,
                //    PhoneNumber = Member.PhoneNumber,
                //    Gender = Member.Gender.ToString(),
                //    Photo = Member.Photo,
                //    Address=$"{Member.Address.BuildingNo} - {Member.Address.Street} - {Member.Address.City}",
                //    DateOfBirth = Member.DateOfBirth.ToShortDateString(),

                //}; 
                #endregion

                var memberViewModel = _mapper.Map<MemberViewModel>(Member);
                var ActiveMembership = _unitOfWok.GetRepository<MemberShip>().GetAll(ms => ms.MemberId == Member.Id && ms.Status == "Active").FirstOrDefault();

                if (ActiveMembership is not null)
                {
                    memberViewModel.MembershipStartDate = ActiveMembership.CreatedAt.ToShortDateString();
                    memberViewModel.MembershipEndDate = ActiveMembership.EndDate.ToShortDateString();
                    var plan = _planRepository.GetPlanById(ActiveMembership.PlanId);
                    memberViewModel.PlanName = plan?.Name;
                }
                return memberViewModel;

            }
            catch
            {
                return null;

            }

        }

        public HealthRecordViewModel? GetMemberHealthRecord(int MemberId)
        {
            var memberHealthRecord= _unitOfWok.GetRepository<HealthRecord>().GetById(MemberId);
            if (memberHealthRecord == null) return null;
            return _mapper.Map<HealthRecordViewModel>(memberHealthRecord);
        }

        public MemberToUpDateViewModel? GetMemberToUpDate(int MemberId)
        {
            var member = _unitOfWok.GetRepository<Member>().GetById(MemberId);

            if (member == null) return null;

            return _mapper.Map<MemberToUpDateViewModel>(member);
        }

        public bool RemoveMember(int id)
        {
           var member= _unitOfWok.GetRepository<Member>().GetById(id);

            if(member is null) return false;

            var SessionIds= _unitOfWok.GetRepository<MemberSession>().GetAll(X=>X.MemberId==id ).Select(s=>s.SessionId);
            var hasFutureSessions= _unitOfWok.GetRepository<Session>().GetAll(x=>SessionIds.Contains(x.Id) && x.StartTime >DateTime.Now).Any();

            if (hasFutureSessions) return false;

            var ActiveMemberShip= _unitOfWok.GetRepository<MemberShip>().GetAll(x=>x.MemberId==id);

            try
            {
                if (ActiveMemberShip.Any())
                {
                    foreach (var membership in ActiveMemberShip)

                        _unitOfWok.GetRepository<MemberShip>().Delete(membership);

                }
                _unitOfWok.GetRepository<Member>().Delete(member);

                var isDeleted= _unitOfWok.SaveChanges()>0;
                if (isDeleted)
                    _AttachmentService.Delete("members", member.Photo);
                return isDeleted;

            }
            catch
            {
                return false;   

            }        
        }

        public bool UpdateMemberDetails(int id,MemberToUpDateViewModel updatedMember)
        {
            try
            {
                var member = _unitOfWok.GetRepository<Member>().GetById(id);
                if (member is null || member.Id != id) return false;
                var EmailExists = _unitOfWok.GetRepository<Member>()
                                          .GetAll(x => x.Email == updatedMember.Email && x.Id != id).Any();
                var PhoneExists = _unitOfWok.GetRepository<Member>()
                           .GetAll(x => x.Email == updatedMember.Phone && x.Id != id).Any();

                if (EmailExists || PhoneExists) return false;

               _mapper.Map(updatedMember, member);

                _unitOfWok.GetRepository<Member>().Update(member);

                return _unitOfWok.SaveChanges()>0;

            }
            catch
            {
                return false;

            }



        }



        #region Helper Methods
        bool IsEmailExists(string email) => _unitOfWok.GetRepository<Member>().GetAll(x => x.Email == email).Any();
        bool IsPhoneExists(string phone) => _unitOfWok.GetRepository<Member>().GetAll(x => x.PhoneNumber == phone).Any();
        #endregion
    }




    }
