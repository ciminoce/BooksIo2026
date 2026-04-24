using BooksIo2026.Service.Common;
using BooksIo2026.Service.DTOs.Book;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksIo2026.Service.Interfaces
{
    public interface IBookService
    {
        List<BookListDto> GetAll();
        BookUpdateDto? GetForUpdate(int id);
        BookDetailDto? GetById(int id);
        Result Add(BookCreateDto bookDto);
        Result Update(BookUpdateDto bookDto);
        Result Delete(int id);

    }
}
