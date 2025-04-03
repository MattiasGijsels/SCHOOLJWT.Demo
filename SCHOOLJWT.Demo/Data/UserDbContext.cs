using Microsoft.EntityFrameworkCore;
using SCHOOLJWT.Demo.Entities;

namespace SCHOOLJWT.Demo.Data
{
    public class UserDbContext(DbContextOptions<UserDbContext>options):DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}
