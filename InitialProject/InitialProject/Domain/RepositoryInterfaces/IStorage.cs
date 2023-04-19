using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.RepositoryInterfaces
{
    public interface IStorage<T>
    {
        T Save(T entity);
        int NextId(Func<T, int> idSelector);
        List<T> GetAll();
        void Delete(T entity, Func<T, bool> predicate);
        T Update(T entity, Func<T, bool> predicate);
    }
}
