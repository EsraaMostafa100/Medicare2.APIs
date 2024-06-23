using AutoMapper;
using Medicare2.APIs.DTOs;
using Medicare2.Core.Entities;

namespace Medicare2.APIs.Helpers
{
    public class MappingProfiles : AutoMapper.Profile
    {
        public MappingProfiles()
        {
            CreateMap<pharmacy, PharmacyToReturnDTo>();
            CreateMap<CustomerCartDTo, Usercart>();
            CreateMap<CartItemDto, CartItem>();
        }
    }
}
