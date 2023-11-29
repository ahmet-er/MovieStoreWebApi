using AutoMapper;
using WebApi.Application.ActorOperations.Commands.CreateActor;
using WebApi.Application.ActorOperations.Commands.UpdateActor;
using WebApi.Application.ActorOperations.Queries.GetActorDetail;
using WebApi.Entities;
using static WebApi.Application.ActorOperations.Queries.GetActors.GetActorsQuery;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Actor
            CreateMap<CreateActorModel, Actor>();
            CreateMap<UpdateActorModel, Actor>()
                .ForMember(dest => dest.MovieActors, opt => opt.MapFrom(src => src.MovieActors));
            CreateMap<Actor, ActorViewModel>()
                .ForMember(dest => dest.MovieActors, opt => opt.MapFrom(src => src.MovieActors));
            CreateMap<Actor, GetActorDetailViewModel>()
                .ForMember(dest => dest.MovieActors, opt => opt.MapFrom(src => src.MovieActors));
        }
    }
}
