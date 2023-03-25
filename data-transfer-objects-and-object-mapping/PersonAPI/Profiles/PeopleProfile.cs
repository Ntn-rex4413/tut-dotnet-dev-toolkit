using AutoMapper;
using PersonAPI.Dtos;
using PersonAPI.Models;

namespace PersonAPI.Profiles
{
    public class PeopleProfile : Profile
    {
        public PeopleProfile()
        {
            // Source -> Destination
            CreateMap<Person, PersonReadDto>()
                // ~ in our destination Age property maps to YearsAlive property in our source
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.YearsAlive));

            // Map for creation of models
            CreateMap<PersonCreateDto, Person>();
        }
    }
}