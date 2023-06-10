using EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        protected T GetById(int id);
        public List<T> GetAll();
        public void Add(T entity);
        public void Delete(T entity);
    }
}
