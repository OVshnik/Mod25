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
        public AppContext appContext;
        public BookRepository(AppContext appContext)
        {
            this.appContext = appContext;
        }
        public Book SelectBookByIdInDb(int id)
        {
            using (var db = appContext)
            {
                var book = db.Books.FirstOrDefault(u => u.Id == id);
                return book;
            }
        }
        public List<Book> SelectAllBooksInDb()
        {
            using (var db = appContext)
            {
                var books = db.Books.ToList();
                return books;
            }
        }
        public void AddBookInDb(Book book)
        {
            using (var db = appContext)
            {
                db.Add(book);
                db.SaveChanges();
                Console.WriteLine("Книга {0} Добавлен в БД", book.Name);
            }
        }
        public void DeleteBookInDb(Book book)
        {
            using (var db = appContext)
            {
                db.Remove(book);
                db.SaveChanges();
                Console.WriteLine("Книга {0} удален из БД", book.Name);
            }
        }
        public void UpdateBookRealeaseYearInDb(int id, int year)
        {
            using (var db = appContext)
            {
                var book = db.Books.FirstOrDefault(u => u.Id == id);
                book.ReleaseYear = year;
                db.Entry(book).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public List<Book> GetBooksListByGenreAndYear(string genre, int year1, int year2)
        {
            using (var db = appContext)
            {
                var books = db.Books.Where(b => (b.Genre == genre) && (b.ReleaseYear >= year1) && (b.ReleaseYear <= year2)).ToList();
                return books;
            }
        }
        public int GetBooksCountByAuthor(Author author)
        {
            using (var db = appContext)
            {
                var bookNum = db.Books.Where(b => (b.AuthorId == author.Id)).Count();
                return bookNum;
            }
        }
        public int GetBooksCountByGenre(string genre)
        {
            using (var db = appContext)
            {
                var bookNum = db.Books.Where(b => b.Genre == genre).Count();
                return bookNum;
            }
        }
        public bool SearchBookInLibByNameAndAuthor(Author author, string bookName)
        {
            using (var db = appContext)
            {
                if (db.Books.Where(b => (b.AuthorId == author.Id) && (b.Name == bookName)) != null)
                {
                    return true;
                }
                return false;

            }
        }
        public int GetCountsBooksOnUser(User user)
        {
            using (var db = appContext)
            {
                var num=db.Books.Where(b=>b.UserId==user.Id).Count();
                return num;
            }
        }
        public bool SearchBookFromUser(User user, string bookName)
        {
            using (var db = appContext)
            {
                if (db.Books.Where(b => (b.UserId == user.Id) && (b.Name == bookName)) != null)
                { return true; }
                return false;
            }
        }

        public Book GetLastReleaseBook()
        {
            using (var db = appContext)
            {
                var book=db.Books.OrderByDescending(b => b.ReleaseYear).First();
                return book;
            }
        }
        public List<Book> GetAllBookInAlphabetOrder()
        {
            using (var db = appContext)
            {
                var books = db.Books.OrderBy(b => b.Name).ToList();
                return books;
            }
        }
        public List<Book> GetAllBookByReleaseYearOrderByDesc()
        {
            using (var db = appContext)
            {
                var books = db.Books.OrderByDescending(b=>b.ReleaseYear).ToList();
                
                return books;
            }
        }
    }
}
