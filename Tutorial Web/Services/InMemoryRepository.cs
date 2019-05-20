using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tutorial_Web.Model;

namespace Tutorial_Web.Services
{
    public class InMemoryRepository : IRepository<Student>
    {
        private List<Student> _students;
        public InMemoryRepository()
        {
            _students= new List<Student>
            {
                new Student()
                {
                    Id = 1,
                    FirstName = "Hex",
                    LastName = "sola",
                    BirthDate = new DateTime(1996,3,14)
                },
                new Student()
                {
                Id = 2,
                FirstName = "Hex2",
                LastName = "sola2",
                BirthDate = new DateTime(1997,3,14)
                },
                new Student()
                {
                    Id = 3,
                    FirstName = "Hex3",
                    LastName = "sola3",
                    BirthDate = new DateTime(1998,3,14)
                }
            };
        }

        public IEnumerable<Student> GetAll()
        {
            return _students;

        }

        public Student GetById(int id)
        {
            return _students.FirstOrDefault(x => x.Id == id);
           // return _students.Find(x=>x.Id==id);

        }

        public Student Add(Student newModel)//添加新的内容，新增加id
        {
            var maxId = _students.Max(x => x.Id);
            newModel.Id = maxId + 1;
            _students.Add(newModel);
            return newModel;
        }
    }
}
