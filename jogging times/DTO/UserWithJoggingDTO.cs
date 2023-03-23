using Microsoft.Build.Framework;

namespace jogging_times.DTO
{
    public class UserWithJoggingDTO
    {
        [Required]
        public string date { get; set; }
        [Required]
        public double time { get; set; }
        [Required]
        public  double distance { get; set; }
    }
}
