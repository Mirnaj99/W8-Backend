

using AutoMapper;
using W8_Backend.Data.Entities;
using W8_Backend.Models.UserModels.Input;

namespace W8_Backend.Helpers
{
    public class AutoMapperProfile : Profile
    {
        // Defining the needed maps between objects for the automapper
        //Automapper maps the attributes with the same name and type between objects to avoid setting them field by field
        public AutoMapperProfile()
        {
        
            CreateMap<Users, UserCreateRequest>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UserCreateRequest, Users>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Users, UserUpdateRequest>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UserUpdateRequest, Users>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
           
          
      
            CreateMap<int?, int>().ConvertUsing((src, dest) => { if (src.HasValue) return src.Value; return dest; });
            CreateMap<bool?, bool>().ConvertUsing((src, dest) => { if (src.HasValue) return src.Value; return dest; });
            CreateMap<decimal?, decimal>().ConvertUsing((src, dest) => { if (src.HasValue) return src.Value; return dest; });




        }
    }
}
