using AutoMapper;
using GYMManagementBLL.ViewModel.MemberViewModels;
using GYMManagementBLL.ViewModel.PlanViewModels;
using GYMManagementBLL.ViewModel.SessionViewModels;
using GYMManagementBLL.ViewModel.TrainerViewModels;
using GYMManagementDL.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementBLL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Session Mapper
            CreateMap<Session, SessionViewModel>().ForMember(dest => dest.CategoryName, Options => Options.MapFrom(src => src.Category.CategoryName))
                                         .ForMember(dest => dest.TrainerName, Options => Options.MapFrom(src => src.Trainer.Name))
                                         .ForMember(dest => dest.AvailableSlots, Options => Options.Ignore());

            CreateMap<CreateSessionViewModel, Session>();

            CreateMap<Session, SessionToUpdateViewModel>().ReverseMap();

            CreateMap<Trainer, TrainerDropDownViewModel>();
            CreateMap<Category, CategoryDropDownViewModel>().ForMember(dest=>dest.Name,opt=>opt.MapFrom(src=>src.CategoryName));

            #endregion

            #region Plan Mapper

            CreateMap<Plan,PlanViewModel>();

            CreateMap<Plan, PlanToUpdateViewModel>();

            CreateMap<PlanToUpdateViewModel, Plan>();


            #endregion

            #region Member Mapper
            //CreateMap<CreateMemberViewModel, Member>().ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address()
            //{
            //    BuildingNo = src.BuildingNumber,
            //    Street = src.Street,
            //    City = src.City

            //}));


            //second option for mapping if we want to use it as a separete entity

            CreateMap<CreateMemberViewModel, Member>().ForMember(dest => dest.Address, opt => opt.MapFrom(src => src))
                                                      .ForMember(dest=>dest.HealthRecord,opt=>opt.MapFrom(src=>src.HealthRecord))
                                                      .ForMember(dest=>dest.PhoneNumber,opt=>opt.MapFrom(src=>src.Phone));
            CreateMap<HealthRecordViewModel, HealthRecord>().ReverseMap();

            CreateMap<CreateMemberViewModel, Address>().ForMember(dest => dest.BuildingNo, opt => opt.MapFrom(src => src.BuildingNumber))
                                                       .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
                                                       .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City));





            CreateMap<Member, MemberViewModel>().ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Address.BuildingNo} - {src.Address.Street} - {src.Address.City}"))
                                                .ForMember(dest=>dest.DateOfBirth,opt=>opt.MapFrom( src =>src.DateOfBirth.ToShortDateString()))
                                                .ForMember(dest=>dest.Gender,opt=>opt.MapFrom(src=>src.Gender.ToString()));

            CreateMap<Member,MemberToUpDateViewModel>().ForMember(dest=>dest.BuildingNumber,opt=>opt.MapFrom(src=>src.Address.BuildingNo))
                                                      .ForMember(dest=>dest.Street,opt=>opt.MapFrom(src=>src.Address.Street))
                                                      .ForMember(dest=>dest.City,opt=>opt.MapFrom(src=>src.Address.City))
                                                      .ForMember(dest=>dest.Phone,opt=>opt.MapFrom(src=>src.PhoneNumber));

            // I used afterMap here because we don't want to create new obj from address we just want to update the existing one
            // Also we ignore name and photo because they won't be updated in this mapping
            CreateMap<MemberToUpDateViewModel, Member>().ForMember(dest => dest.Name,opt=>opt.Ignore())
                                                        .ForMember(dest => dest.Photo,opt=>opt.Ignore())
                                                        .ForMember(dest => dest.PhoneNumber,opt=>opt.MapFrom(src=>src.Phone))
                                                        .AfterMap((src, dest) =>
                                                        {
                                                            dest.Address.BuildingNo = src.BuildingNumber;
                                                            dest.Address.Street = src.Street;
                                                            dest.Address.City = src.City;
                                                            dest.UpdatedAt = DateTime.Now; // doesn't accurate if there were other steps before safe changes
                                                        });

            #endregion

            #region Trainer Mapper

            CreateMap<CreateTrainerViewModel, Trainer>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                {
                    BuildingNo = src.BuildingNumber,
                    Street = src.Street,
                    City = src.City
                }));

            CreateMap<Trainer, TrainerViewModel>()
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest=>dest.Address,opt=>opt.MapFrom(src=>$"{src.Address.BuildingNo} - {src.Address.Street} - {src.Address.City}"));

            CreateMap<Trainer, TrainerToUpdateViewModel>()
                .ForMember(dist => dist.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dist => dist.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dist => dist.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNo))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber));

            CreateMap<TrainerToUpdateViewModel, Trainer>()
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.Name, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                dest.Address.BuildingNo = src.BuildingNumber;
                dest.Address.City = src.City;
                dest.Address.Street = src.Street;
                dest.UpdatedAt = DateTime.Now;
            });
            #endregion


        }
    }
}
