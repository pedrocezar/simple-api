namespace Simple.DDD.Domain.Contracts.Requests
{
    public class UsuarioRequest
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
        public string Nacionalidade { get; set; }

        public int EmpresaId { get; set; }
        public int PerfilId { get; set; }
    }
}
