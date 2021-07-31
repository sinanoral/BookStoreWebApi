using AutoMapper;
using WebApi.Application.GenreOperations.Commands.GetGenreDetail;
using WebApi.Application.GenreOperations.Commands.GetGenres;
using WebApi.Entities;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.GetBooks.GetBookDetailQuery;
using static WebApi.GetBooks.GetBooksQuery.GetBooksQuery;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre,
                opt => opt.MapFrom(src => src.Genre.Name));

            CreateMap<Book, BookViewModel>().ForMember(dest => dest.Genre,
                opt => opt.MapFrom(src => src.Genre.Name)).ForMember(dest => dest.PublishDate,
                opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyy")));

            CreateMap<Genre, GenreViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
        }
    }
}
