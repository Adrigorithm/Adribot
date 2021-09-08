using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adribot.src.entities
{
    public class Tag
    {
        public int TagId { get; set; }

        [Required]
        [MaxLength(4000)]
        public string Content { get; set; }

        [Required]
        [MaxLength(40)]
        public string TagName { get; set; }

        [Required]
        public int MemberId { get; set; }
        public Member Member { get; set; }
    }
}
