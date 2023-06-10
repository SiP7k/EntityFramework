using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Entities;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace EntityFramework.Repositories
{
    public class BookRepository : IRepository<Book>
    {
        private AppContext _database;
        public BookRepository(AppContext db)
        {
            _database = db;
        }
        public Book GetById(int id)
        {
            return _database.Books.FirstOrDefault(u => u.Id == id);
        }

        public List<Book> GetAll()
        {
            return _database.Books.ToList();
        }

        public void Add(Book book)
        {
            _database.Books.Add(book);
        }

        public void Delete(Book book)
        {
            _database.Books.Remove(book);
        }

        public void UpdateReleaseYearById(int id)
        {
            Console.WriteLine($"Введите обновлённый год выхода книги:");

            string userInput = Console.ReadLine();

            if (Int32.TryParse(userInput, out int result))
            {
                GetById(id).ReleaseYear = result;
                Console.WriteLine("Год выхода книги успешно обновлён!");
            }
            else
            {
                Console.WriteLine("Ошибка, неправильный формат года выхода книги!");
            }
        }
    }
}
