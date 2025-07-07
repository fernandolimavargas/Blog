namespace BlogFecomercio.Services.Interfaces
{
    public interface ICurtidaService
    {
        public Task<string> CurtirOuDescurtir(int postId, int usuarioId); 
    }
}
