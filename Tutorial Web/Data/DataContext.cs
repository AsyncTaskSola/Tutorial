using Microsoft.EntityFrameworkCore;
using Tutorial_Web.Model;

namespace Tutorial_Web.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext>Options):base(Options)
        {
            
        }
        public  DbSet<Student> Students { get; set; }
    }
}
