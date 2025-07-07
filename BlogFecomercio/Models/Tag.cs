using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogFecomercio.Models
{
    [Table("Tags")]
    public class Tag
    {
        [Key]
        public int TagId { get; set; }
        [Required]
        public string Name { get; set; } = null!;

    }
}
