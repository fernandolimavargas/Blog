namespace BlogFecomercio.Models
{
    public class Seguidor
    {
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;
        public int SeguidorId { get; set; }
        public Usuario SeguidorUsuario { get; set; } = null!;
        public DateTime DataSeguimento { get; set; }
    }
}
