using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tutorial_Web.Data;
using Tutorial_Web.Model;

namespace Tutorial_Web.Services
{
    public class EfCoreRepository:IRepository<Student>
    {
        private readonly DataContext _context;

        public EfCoreRepository(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<Student> GetAll()
        {
            return _context.Students.ToList();
        }

        public Student GetById(int id)
        {
            return _context.Students.Find(id);
        }

        public Student Add(Student newModel)
        {
            _context.Students.Add(newModel);
            _context.SaveChanges();
            return newModel;
        }
    }
}
