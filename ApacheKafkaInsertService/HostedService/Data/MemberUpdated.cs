using System.ComponentModel.DataAnnotations;

namespace HostedService.Data
{
    public class MemberUpdated
    {
        [Key]
        public String SyncId { get; set; }
        public String MailId { get; set; }
        public String PAN { get; set; }
        public String State { get; set; }
        public String Address { get; set; }
        public int ContactNumber { get; set; }
    }
}
