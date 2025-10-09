using GYMManagementBLL.Services.Interfaces;
using GYMManagementBLL.ViewModel;
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
    internal class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWok;
        private readonly IPlanRepository _planRepository;

        public MemberService(IUnitOfWork unitOfWok,IPlanRepository planRepository)
        {
           _unitOfWok = unitOfWok;
           _planRepository = planRepository;
        }


        public bool CreateMember(CreateMemberViewModel createdMember)
        {
            try
            {

                if (IsEmailExists(createdMember.Email) || IsPhoneExists(createdMember.Phone)) return false;

                    var member=new Member()
                {
                    Name = createdMember.Name,
                    Email = createdMember.Email,
                    PhoneNumber = createdMember.Phone,
                    Gender = createdMember.Gender,
                    DateOfBirth = createdMember.DateOfBirth,
                    Address = new Address()
                    {
                        BuildingNo = createdMember.BuildingNumber,
                        City = createdMember.City,
                        Street = createdMember.Street,
                    },
                    HealthRecord = new HealthRecord()
                    {
                        Height = createdMember.HealthRecord.Height,
                        Weight = createdMember.HealthRecord.Weight,
                        BloodType = createdMember.HealthRecord.BloodType,
                        Note = createdMember.HealthRecord.Note,

                    }

                };
                _unitOfWok.GetRepository<Member>().Add(member);

                return _unitOfWok.SaveChanges()>0;

            }
            catch { 
                return false;
            }

        }


        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var Members = _unitOfWok.GetRepository<Member>().GetAll();
            if (Members is null || !Members.Any()) return [];
            return Members.Select(m => new MemberViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Email = m.Email,
                Phone = m.PhoneNumber,
                Gender=m.Gender.ToString(),
                Photo=m.Photo,
            }).ToList();
        }

        public MemberViewModel? GetMember(int MemberId)
        {
            try
            {
                var Member = _unitOfWok.GetRepository<Member>().GetById(MemberId);

                if (Member == null) return null;
                var memberViewModel = new MemberViewModel()
                {
                    Name = Member.Name,
                    Email = Member.Email,
                    Phone = Member.PhoneNumber,
                    Gender = Member.Gender.ToString(),
                    Photo = Member.Photo,
                    DateOfBirth = Member.DateOfBirth.ToShortDateString(),

                };
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
            return new HealthRecordViewModel()
            {
                Height= memberHealthRecord.Height,
                Weight= memberHealthRecord.Weight,
                BloodType= memberHealthRecord.BloodType,
                Note= memberHealthRecord.Note,
            };

            
        }

        public MemberToUpDateViewModel? GetMemberToUpDate(int MemberId)
        {
            var member = _unitOfWok.GetRepository<Member>().GetById(MemberId);

            if (member == null) return null;

            return new MemberToUpDateViewModel()
            {
                Name=member.Name,
                Photo=member.Photo,
                Email=member.Email,
                Phone=member.PhoneNumber,
                BuildingNumber=member.Address.BuildingNo,
                Street=member.Address.Street,   
                City=member.Address.City,

            };
        }

        public bool RemoveMember(int id)
        {
           var member= _unitOfWok.GetRepository<Member>().GetById(id);

            if(member is null) return false;

            var HasActiveMembersession= _unitOfWok.GetRepository<MemberSession>().GetAll(X=>X.MemberId==id && X.Session.StartTime>DateTime.Now).Any();

            if (HasActiveMembersession) return false;

            var ActiveMemberShip= _unitOfWok.GetRepository<MemberShip>().GetAll(x=>x.MemberId==id);

            try
            {
                if (ActiveMemberShip.Any())
                {
                    foreach (var membership in ActiveMemberShip)

                        _unitOfWok.GetRepository<MemberShip>().Delete(membership);

                }
                _unitOfWok.GetRepository<Member>().Delete(member);

                return _unitOfWok.SaveChanges()>0;

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

                if (IsEmailExists(updatedMember.Email) || IsPhoneExists(updatedMember.Phone)) return false;

                member.Email = updatedMember.Email;
                member.PhoneNumber = updatedMember.Phone;
                member.Address.BuildingNo = updatedMember.BuildingNumber;
                member.Address.Street = updatedMember.Street;
                member.Address.City = updatedMember.City;
                member.UpdatedAt = DateTime.Now;

                 _unitOfWok.GetRepository<Member>().Update(member);

                return _unitOfWok.SaveChanges()>0;

            }
            catch
            {
                return false;

            }



        }



        #region Helper Methods
        bool IsEmailExists(string email) => _memberRepository.GetAll(x => x.Email == email).Any();
        bool IsPhoneExists(string phone) => _memberRepository.GetAll(x => x.PhoneNumber == phone).Any();
        #endregion
    }




    }
