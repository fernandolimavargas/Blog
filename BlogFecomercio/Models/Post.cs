using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogFecomercio.Models
{
    [Table("Posts")]
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; } = null;
        public string? Body { get; set; } = null;
        public DateTime DataHoraPostados { get; set; }

        public string? TagNome { get; set; } = null; 
        
        public int Usuarioid { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
        public ICollection<Curtida> Curtidas { get; set; } = new List<Curtida>();

    }
}
