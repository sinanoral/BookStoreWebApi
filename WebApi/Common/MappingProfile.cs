using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));

            CreateMap<Book, BookViewModel>().ForMember(dest => dest.Genre,
                opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString())).ForMember(dest => dest.PublishDate,
                opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyy")));

        }
    }
}
