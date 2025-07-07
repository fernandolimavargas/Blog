using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogFecomercio.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int Usuarioid { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set;  }

        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Seguidor> Seguindo { get; set; } = new List<Seguidor>();
        public ICollection<Seguidor> Seguidores { get; set; } = new List<Seguidor>();
        public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
        public ICollection<Curtida> Curtidas { get; set; } = new List<Curtida>();
    }
}
