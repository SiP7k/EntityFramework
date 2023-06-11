using EntityFramework.Entities;
using EntityFramework.Repositories;

namespace EntityFramework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new AppContext())
            {
                var bookRepository = new BookRepository(db);
                var userRepository = new UserRepository(db);

                var book1 = new Book { Title = "MasterIMargrita", ReleaseYear = 2005 };
                var book2 = new Book { Title = "GoreOtUma", ReleaseYear = 2001 };
                var user1 = new User { Name = "Savva", Email = "cavvach@gmail.com" };
                var user2 = new User { Name = "Irma", Email = "irma@gmail.com" };

                user2.Books.AddRange(new List<Book> { book1, book2 });
                user1.Books.Add(book1);

                bookRepository.Add(book1);
                bookRepository.Add(book2);
                userRepository.Add(user1);
                userRepository.Add(user2);
                db.SaveChanges();
                
            }
        }
    }
}