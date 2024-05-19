namespace Simple.DDD.Domain.Contracts.Responses
{
    public class UsuarioResponse : BaseResponse
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Nacionalidade { get; set; }

        public EmpresaResponse Empresa { get; set; }
        public PerfilResponse Perfil { get; set; }
    }
}
