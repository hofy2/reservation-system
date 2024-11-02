using AutoMapper;
using Domin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reservations.Common.DTOs
{

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<LoginDto, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username));
        }
    }

    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
