using System.ComponentModel.DataAnnotations;

namespace MHSync.Model
{
    public class SyncData
    {
        [Key]
        public string SyncId { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Born { get; set; }
        [Required]
        public DateTime Admission { get; set; }
        [Required]
        public string PracticingArea { get; set; }
        [Required]
        public string PracticingLocation { get; set; }
        [Required]
        public string Position { get; set; }
    }
}
