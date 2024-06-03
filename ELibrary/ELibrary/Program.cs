using ELibrary.Entities;
using ELibrary.lib.Repository;
using AppContext = ELibrary.lib.AppContext;

public class Program
{
    static void Main(string[] args)
    {
        var db = new AppContext();
        var userRep=new UserRepository(db);
        var bookRep=new BookRepository(db);

        var user1 = new User { Name = "Andrey", Email = "and90@gmail.com" };
        var user2 = new User { Name = "Ivan", Email = "iv89@gmail.com" };
        var user3 = new User { Name = "Elena", Email = "helen95@gmail.com" };

        userRep.AddUserInDb(user1);
        userRep.AddUserInDb(user2);
        userRep.AddUserInDb(user3);

        var author1 = new Author { Name = "Irwin", LastName = "Shaw" };
        var author2 = new Author { Name = "Ken", LastName = "Kesey" };
        var author3 = new Author { Name = "Louis", LastName = "Boussenard" };

        using (db)
        {
            db.AddRange(author1, author2, author3);
            db.SaveChanges();
        }

        var book1 = new Book { Name = "Nightwork", ReleaseYear = 1975, Genre = "Detective", AuthorId = author1.Id };
        var book2 = new Book { Name = "One Flew Over the Cuckoo's Nest", ReleaseYear=1962 ,Genre = "Roman", AuthorId = author2.Id };
        var book3 = new Book { Name = "The diamonds thievs", ReleaseYear=1883 ,Genre = "Adventure", AuthorId = author3.Id };

        bookRep.AddBookInDb(book1);
        bookRep.AddBookInDb(book2);
        bookRep.AddBookInDb(book3);

        using (db)
        {
            author1.Books.Add(book1);
            author2.Books.Add(book2);
            author3.Books.Add(book3);
            db.SaveChanges();
        }

        userRep.GiveBookOnUserHands(user1, book1);
        userRep.GiveBookOnUserHands(user2, book2);
        userRep.GiveBookOnUserHands(user3, book3);

        


    }
}