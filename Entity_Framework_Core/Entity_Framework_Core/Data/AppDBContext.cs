using Microsoft.EntityFrameworkCore;

namespace Entity_Framework_Core.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options): base(options)
        {
            

        }
    }
}
