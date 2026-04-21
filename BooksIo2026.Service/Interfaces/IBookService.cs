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
        (bool Success, List<string> Errors) Add(BookCreateDto bookDto);
        (bool Success, List<string> Errors) Update(BookUpdateDto bookDto);
        (bool Success, List<string> Errors) Delete(int id);

    }
}
