using ELibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.lib.Repository
{
    public class UserRepository
    {
        public AppContext AppContext { get; set; }  

        public UserRepository(AppContext appContext)
        {
            AppContext = appContext;
        }

        public User SelectUserByIdInDb(int id)
        {
            using (var db=AppContext)
            {
                var user=db.Users.FirstOrDefault(u => u.Id == id);
                return user;
            }
        }
        public List<User>SelectAllUsersInDb()
        {
            using (var db= AppContext)
            {
                var users = db.Users.ToList();
                return users;
            }
        }
        public void AddUserInDb(User user)
        {
            using (var db=AppContext) 
            {
                db.Add(user);
                db.SaveChanges();
                Console.WriteLine("Пользователь {0} Добавлен в БД",user.Name);
            }
        }
        public void DeleteUserInDb(User user)
        {
            using (var db = AppContext)
            {
                db.Remove(user);
                db.SaveChanges();
                Console.WriteLine("Пользователь {0} удален из БД",user.Name);
            }
        }
        public void UpdateUserNameInDb(int id, string name)
        {
            using (var db = AppContext)
            {
                var user = db.Users.FirstOrDefault(u => u.Id == id);
                user.Name = name;
                db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public void GiveBookOnUserHands(User user, Book book)
        {
            using (var db = AppContext)
            {
                var user1 = SelectUserByIdInDb(user.Id);
                if (!user1.Books.Contains(book))
                {
                    user1.Books.Add(book);
                    db.SaveChanges();
                }
                Console.WriteLine("Данная книга уже выдана пользователю");

            }
        }

    }
}
