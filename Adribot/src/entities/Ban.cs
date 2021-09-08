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
        public int BanId { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }
        public string Reason { get; set; }

        [Required]
        public int MemberId { get; set; }
        public Member Member { get; set; }
    }
}
