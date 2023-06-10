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

                var user1 = new User { Name = "Savva", Email = "cavvach@gmail.com" };
                var user2 = new User { Name = "Irma", Email = "irma@gmail.com" };

                userRepository.Add(user1);
                userRepository.Add(user2);
                db.SaveChanges();

                userRepository.UpdateUsernameById(1);
                userRepository.Delete(user2);
                db.SaveChanges();
                
            }
        }
    }
}