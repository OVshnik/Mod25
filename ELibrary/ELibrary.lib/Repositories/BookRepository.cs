using ELibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.lib.Repository
{
    public class BookRepository
    {
        private AppContext _appContext;
        public BookRepository(AppContext appContext)
        {
            _appContext = appContext;
        }
        public Book SelectBookByIdInDb(int id)
        {

            var book = _appContext.Books.FirstOrDefault(u => u.Id == id);
            return book;

        }
        public List<Book> SelectAllBooksInDb()
        {

            var books = _appContext.Books.ToList();
            return books;

        }
        public void AddBookInDb(Book book)
        {
            _appContext.Add(book);
            _appContext.SaveChanges();
            Console.WriteLine("Книга {0} добавлена в БД", book.Name);

        }
        public void DeleteBookInDb(Book book)
        {
            _appContext.Remove(book);
            _appContext.SaveChanges();
            Console.WriteLine("Книга {0} удалена из БД", book.Name);

        }
        public void UpdateBookRealeaseYearInDb(int id, int year)
        {

            var book = _appContext.Books.FirstOrDefault(u => u.Id == id);
            book.ReleaseYear = year;
            _appContext.Entry(book).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _appContext.SaveChanges();

        }
        public List<Book> GetBooksListByGenreAndYear(string genre, int year1, int year2)
        {

            var books = _appContext.Books.Where(b => (b.Genre == genre) && (b.ReleaseYear >= year1) && (b.ReleaseYear <= year2)).ToList();
            return books;
        }
        public int GetBooksCountByAuthor(Author author)
        {

            var bookNum = _appContext.Books.Where(b => (b.AuthorId == author.Id)).Count();
            return bookNum;

        }
        public int GetBooksCountByGenre(string genre)
        {
            var bookNum = _appContext.Books.Where(b => b.Genre == genre).Count();
            return bookNum;

        }
        public bool SearchBookInLibByNameAndAuthor(Author author, string bookName)
        {

            if (_appContext.Books.Where(b => (b.AuthorId == author.Id) && (b.Name == bookName)) != null)
            {
                return true;
            }
            return false;

        }
        public int GetCountsBooksOnUser(User user)
        {

            var num = _appContext.Books.Where(b => b.UserId == user.Id).Count();
            return num;

        }
        public bool SearchBookFromUser(User user, string bookName)
        {

            if (_appContext.Books.Where(b => (b.UserId == user.Id) && (b.Name == bookName)) != null)
            { return true; }
            return false;

        }

        public Book GetLastReleaseBook()
        {
            var book = _appContext.Books.OrderByDescending(b => b.ReleaseYear).First();
            return book;

        }
        public List<Book> GetAllBookInAlphabetOrder()
        {

            var books = _appContext.Books.OrderBy(b => b.Name).ToList();
            return books;

        }
        public List<Book> GetAllBookByReleaseYearOrderByDesc()
        {
            var books = _appContext.Books.OrderByDescending(b => b.ReleaseYear).ToList();

            return books;

        }
    }
}
