using System.ComponentModel.DataAnnotations;

namespace BlogFecomercio.Models
{
    public class Curtida
    {
        public int PostId { get; set; }
        public Post Post { get; set; } = null!;

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public DateTime DataCurtida { get; set; } = DateTime.UtcNow;
    }
}
