using AutoMapper;
using WebApi.Application.ActorOperations.Commands.CreateActor;
using WebApi.Application.ActorOperations.Commands.UpdateActor;
using WebApi.Application.ActorOperations.Queries.GetActorDetail;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using WebApi.Application.CustomerOperations.Commands.UpdateCustomer;
using WebApi.Application.CustomerOperations.Queries.GetCustomerDetail;
using WebApi.Application.CustomerOperations.Queries.GetCustomers;
using WebApi.Application.DirectorOperations.Commands.CreateDirector;
using WebApi.Application.DirectorOperations.Commands.UpdateDirector;
using WebApi.Application.DirectorOperations.Queries.GetDirectorDetail;
using WebApi.Application.DirectorOperations.Queries.GetDirectors;
using WebApi.Application.MovieOperations.Commands.CreateMovie;
using WebApi.Application.MovieOperations.Commands.UpdateMovie;
using WebApi.Application.MovieOperations.Queries.GetMovieDetail;
using WebApi.Application.MovieOperations.Queries.GetMovies;
using WebApi.Application.OrderOperations.Commands.CreateOrder;
using WebApi.Application.OrderOperations.Commands.DeleteOrder;
using WebApi.Application.OrderOperations.Commands.UpdateOrder;
using WebApi.Application.OrderOperations.Queries.GetOrderDetail;
using WebApi.Entities;
using static WebApi.Application.ActorOperations.Queries.GetActors.GetActorsQuery;
using static WebApi.Application.OrderOperations.Queries.GetOrders.GetOrdersQuery;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Actor
            CreateMap<CreateActorModel, Actor>();
            CreateMap<UpdateActorModel, Actor>();
            CreateMap<Actor, ActorViewModel>();
            CreateMap<Actor, GetActorDetailViewModel>();

            //Customer
            CreateMap<CreateCustomerModel, Customer>();
            CreateMap<UpdateCustomerModel, Customer>();
            CreateMap<Customer, GetCustomerDetailViewModel>();
            CreateMap<Customer, CustomersViewModel>();

            //Director
            CreateMap<CreateDirectorModel, Director>();
            CreateMap<UpdateDirectorModel, Director>();
            CreateMap<Director, DirectorsViewModel>();
            CreateMap<Director, DirectorDetailViewModel>();

            //Movie
            CreateMap<CreateMovieModel, Movie>();
            CreateMap<UpdateMovieModel, Movie>();
            CreateMap<Movie, MoviesViewModel>();
            CreateMap<Movie, MovieDetailViewModel>();

            //Order
            CreateMap<CreateOrderModel, Order>();
            CreateMap<UpdateOrderModel, Order>();
            CreateMap<SoftDeleteOrderModel, Order>();
            CreateMap<Order, OrdersViewModel>();
            CreateMap<Order, OrderDetailViewModel>();
        }
    }
}
