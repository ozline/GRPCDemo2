using Microsoft.EntityFrameworkCore;
namespace CXBIM.UserService.Service.Models
{
    public class User
    {
        public long userid { get; set; }
        public string? username { get; set; }
        public long create_at { get; set; }
        public string? password { get; set; }
    }
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
    }
}