using System.ComponentModel;

namespace EBP.API.Models
{
    public class BookTicketRequest
    {
        [DefaultValue(1)]
        public int ReqularCount { get; set; }
        [DefaultValue(1)]
        public int VipCount { get; set; }
        [DefaultValue(1)]
        public int StudentCount { get; set; }
    }
}
