using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adribot.src.entities
{
    public class Ban
    {
        [Key]
        public int BanId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Reason { get; set; }

        public int UserId { get; set; }
        public Member Member { get; set; }
    }
}
