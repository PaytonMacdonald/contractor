using System.ComponentModel.DataAnnotations;

namespace contractor.Models
{
    public class Job
    {
        public int Id { get; set; }
        public Account Creator { get; set; }
        [Required]
        [MaxLength(20)]
        public string Title { get; set; }
        public string Body { get; set; }
        public string CreatorId { get; set; }
    }
}