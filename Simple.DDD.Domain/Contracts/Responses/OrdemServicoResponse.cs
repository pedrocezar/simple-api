namespace Simple.DDD.Domain.Contracts.Responses
{
    public class OrdemServicoResponse : BaseResponse
    {
        public DateTime Data { get; set; }
        public string Defeito { get; set; }
        public string Equipamento { get; set; }

        public UsuarioResponse Usuario { get; set; }

        public FinalizacaoResponse Finalizacao { get; set; }
    }
}
