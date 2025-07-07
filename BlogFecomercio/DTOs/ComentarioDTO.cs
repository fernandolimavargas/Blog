namespace BlogFecomercio.DTOs
{
    public class ComentarioDTO
    {
        public int Id { get; set; }
        public string Conteudo { get; set; } = "";
        public string Autor { get; set; }
        public DateTime DataComentario { get; set; }
    }
}
