using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace jogging_times.Models
{
    public class ApplicationDbContext : IdentityDbContext<User>
    
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public virtual DbSet<joggingTime> joggingTimes { get; set; }

        public void ListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
