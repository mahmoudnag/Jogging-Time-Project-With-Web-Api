using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace jogging_times.Models
{
    public class User : IdentityUser
    {
      
            [Required, MaxLength(50)]
            public string FirstName { get; set; }

            [Required, MaxLength(50)]
        public string LastName { get; set; }
        
        public virtual ICollection<joggingTime> JoggingTimes { get; set; }
    }
    }

