using System.ComponentModel.DataAnnotations;

namespace BlogFecomercio.Models
{
    public class Comentario
    {
        [Key]
        public int ComentarioId { get; set; }
        public string Texto { get; set; } = null!;
        public DateTime DataHoraComentario { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; } = null!;
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;
    }
}
