using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adribot.src.entities
{
    [Table("tags")]
    public class Tag
    {
        [Key]
        [Column("tagId")]
        public int TagId { get; set; }

        [Column("guildId")]
        public ulong GuildId { get; set; }

        [Column("authorId")]
        public ulong AuthorId { get; set; }

        [Column("content")]
        public string Content { get; set; }

        [Column("tagName")]
        public string TagName { get; set; }
    }
}
