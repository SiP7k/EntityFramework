using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Entities;
using Microsoft.EntityFrameworkCore.Update.Internal;
using EntityFramework.Helpers;


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
        public Book GetLastBook()
        {
            return _database.Books.MaxBy(b => b.ReleaseYear);
        }

        public List<Book> GetAll()
        {
            return _database.Books.ToList();
        }
        public List<Book> GetAllOrderedByTitle()
        {
            return _database.Books.OrderBy(b=>b.Title).ToList();
        }
        public List<Book> GetAllOrderedByReleaseYear()
        {
            return _database.Books.OrderBy(b => b.ReleaseYear).ToList();
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

            GetById(id).ReleaseYear = StringConverterToInt.ConvertToInt();
            Console.WriteLine("Год выхода книги успешно обновлён!");
        }
        public List<Book> GetBooksByGenre()
        {
            bool isGengreNotChoosen = true;
            Genres genre = Genres.Romance;
            Console.WriteLine("Какой жанр вас интересует?\n1) Fantasy\n2) Horror\n3) Detective\n4) Romance");

            while (isGengreNotChoosen)
            {
                Console.Write("Введите цифру интересущего вас жанра: ");
                string userInput = Console.ReadLine();
                
                switch (userInput)
                {
                    case "1":
                        {
                            genre = Genres.Fantasy;
                            break;
                        }
                    case "2":
                        {
                            genre = Genres.Horror;
                            break;
                        }
                    case "3":
                        {
                            genre = Genres.Detective;
                            break;
                        }
                    case "4":
                        {
                            genre = Genres.Romance;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Ошибка в выборе жанра книги!");
                            isGengreNotChoosen = false;
                            break;
                        }
                }
            }
            return _database.Books.Where(b => b.Genre == genre).ToList();
        }
        public List<Book> GetBooksByIntervalOfYears()
        {
            Console.WriteLine("Введите год, не раньше которого должны были выйти книги");
            int year1 = StringConverterToInt.ConvertToInt();

            Console.WriteLine("Введите год, не позже которого должны были выйти книги");
            int year2 = StringConverterToInt.ConvertToInt();

            return _database.Books.Where(b => b.ReleaseYear > year1 && b.ReleaseYear < year2).ToList();
        }
        public List<Book> GetBooksByYearAndGenre() 
        {
            List<Book> firstList = GetBooksByGenre();
            List<Book> secondList = GetBooksByIntervalOfYears();
            List<Book> result = firstList.Intersect(secondList).ToList();
            
            return result;
        }
        public int GetBooksCountByAuthor()
        {
            int result = 0;
            var authors = GetAndPrintAllAuthors();

            bool isWorking = true;
            Console.Write("Введите имя автора для получения количества его книг: ");

            while(isWorking)
            {
                string userInput = Console.ReadLine();

                if (authors.Contains(userInput))
                {
                    result = _database.Books.Where(a => a.Author == userInput).Count();
                    isWorking = false;
                }
                else
                {
                    Console.WriteLine("Такого автора не сущесвтует в нашей библиотеке, попробуйте ещё раз!");
                }
            }
            return result;
        }
        public int GetBooksCountByGenre()
        {
            return GetBooksByGenre().Count();
        }
        public bool DoesBookExistByAuthorAndTitle()
        {
            Console.WriteLine("Введите имя автора книги:");
            string author = Console.ReadLine();

            Console.WriteLine("Введите название книги:");
            string title = Console.ReadLine();

            return _database.Books.Any(b => b.Title == title && b.Author == author);
        }
        private List<string?> GetAndPrintAllAuthors()
        {
            var authors = GetAllAuthors();

            Console.WriteLine("Список авторов:");

            foreach (var author in authors)
            {
                Console.WriteLine(author);
            }
            Console.WriteLine();

            return authors;
        }
        private List<string?> GetAllAuthors()
        {
            return GetAll().Select(b => b.Author).Distinct().ToList();
        }
    }
}
