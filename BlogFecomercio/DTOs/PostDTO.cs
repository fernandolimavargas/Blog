namespace BlogFecomercio.DTOs
{
    public class PostDTO
    {
    public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Body { get; set; } = "";
        public DateTime DataHoraPostados { get; set; }
        public string? TagNome { get; set; }
        public string Username{ get; set; }
        public int TotalCurtidas { get; set;  }
        public List<ComentarioDTO> Comentarios { get; set; } = new();
    }
}
