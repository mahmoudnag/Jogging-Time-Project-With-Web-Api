using System.ComponentModel.DataAnnotations.Schema;

namespace jogging_times.Models
{
    public class joggingTime
    {
        public int Id { get; set; }
        public  string date { get; set; }
        public double time { get; set; }
        public double distance { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
       
        public virtual User User { get; set; }

    }
}
