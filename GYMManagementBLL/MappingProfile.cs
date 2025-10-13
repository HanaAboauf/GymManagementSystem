using AutoMapper;
using GYMManagementBLL.ViewModel.SessionViewModels;
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
            CreateMap<Session, SessionViewModel>().ForMember(dest => dest.CategoryName, Options => Options.MapFrom(src => src.Category.CategoryName))
                                                 .ForMember(dest => dest.TrainerName, Options => Options.MapFrom(src => src.Trainer.Name))
                                                 .ForMember(dest => dest.AvailableSlots, Options => Options.Ignore());

            CreateMap<CreateSessionViewModel, Session>();

            CreateMap<Session, SessionToUpdateViewModel>();
            CreateMap<SessionToUpdateViewModel, Session>();



        }
    }
}
