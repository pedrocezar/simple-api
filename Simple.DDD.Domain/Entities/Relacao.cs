namespace Simple.DDD.Domain.Entities
{
    public class Relacao : BaseEntity
    {
        public int OrdemServicoId { get; set; }
        public virtual OrdemServico OrdemServico { get; set; }

        public int ServicoId { get; set; }
        public virtual Servico Servico { get; set; }    
    }
}
