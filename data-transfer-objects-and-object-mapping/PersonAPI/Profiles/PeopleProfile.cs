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
            CreateMap<PersonCreateDto, Person>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.House, opt => opt.NullSubstitute("Hufflepuff"));

            // Map for updating of models
            CreateMap<PersonUpdateDto, Person>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<string, int>().ConvertUsing<IntTypeConverter>();
        }
    }
}