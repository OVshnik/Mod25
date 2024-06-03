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
        private AppContext _appContext { get; set; }

        public UserRepository(AppContext appContext)
        {
            _appContext = appContext;
        }

        public User SelectUserByIdInDb(int id)
        {

            var user = _appContext.Users.FirstOrDefault(u => u.Id == id);
            return user;

        }
        public List<User> SelectAllUsersInDb()
        {

            var users = _appContext.Users.ToList();
            return users;

        }
        public void AddUserInDb(User user)
        {

            _appContext.Add(user);
            _appContext.SaveChanges();
            Console.WriteLine("Пользователь {0} добавлен в БД", user.Name);

        }
        public void DeleteUserInDb(User user)
        {

            _appContext.Remove(user);
            _appContext.SaveChanges();
            Console.WriteLine("Пользователь {0} удален из БД", user.Name);

        }
        public void UpdateUserNameInDb(int id, string name)
        {

            var user = _appContext.Users.FirstOrDefault(u => u.Id == id);
            user.Name = name;
            _appContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _appContext.SaveChanges();

        }
        public void GiveBookOnUserHands(User user, Book book)
        {

            var user1 = SelectUserByIdInDb(user.Id);
            if (!user1.Books.Contains(book))
            {
                user1.Books.Add(book);
                _appContext.SaveChanges();
            }
            else
            Console.WriteLine("Данная книга уже выдана пользователю");


        }

    }
}
