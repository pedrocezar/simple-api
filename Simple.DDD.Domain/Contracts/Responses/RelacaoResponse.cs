namespace Simple.DDD.Domain.Contracts.Responses
{
    public class RelacaoResponse : BaseResponse
    {
        public OrdemServicoResponse OrdemServico { get; set; }

        public ServicoResponse Servico { get; set; }    
    }
}
