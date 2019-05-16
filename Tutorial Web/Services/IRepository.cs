using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tutorial_Web.Model;

namespace Tutorial_Web.Services
{
    public interface IRepository<T>where T:class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        T Add(T newModel);
    }
}
