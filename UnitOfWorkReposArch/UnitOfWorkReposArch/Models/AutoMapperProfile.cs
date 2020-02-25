using AutoMapper;
using UoWR.Arch.Domain;
using UoWR.Arch.Models.PersonModel;

namespace UoWR.Arch.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<PersonDTO, Person>();
        }
    }
}
