using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Entities;

namespace EntityFramework.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private AppContext _database { get; set; }
        public UserRepository(AppContext db)
        {
            _database = db;
        }
        public User GetById(int id)
        {
            return _database.Users.FirstOrDefault(u => u.Id == id);
        }

        public List<User> GetAll()
        {
            return _database.Users.ToList();
        }

        public void Add(User user)
        {
            _database.Users.Add(user);
        }

        public void Delete(User user)
        {
            _database.Users.Remove(user);
        }
        public void UpdateUsernameById(int id)
        {
            Console.WriteLine($"Введите новое имя пользователя:");

            string userInput = Console.ReadLine();

            if (userInput == null)
            {
                Console.WriteLine("Ошибка, имя пользователя не может быть пустым!");
            }
            else
            {
                _database.Users.FirstOrDefault(u => u.Id == id).Name = userInput;
                Console.WriteLine("Имя пользователя успешно обновлено!");
            }
        }
        public bool DoesUserHaveBook(User user, Book book)
        {
            return user.Books.Contains(book);
        }
        public int CountUserBooks(User user)
        {
            return user.Books.Count();
        }
    }
}
