namespace Simple.DDD.Domain.Entities
{
    public class Finalizacao : BaseEntity
    {
        public float ValorTotal { get; set; }
        public DateTime Data { get; set; }
        public DateTime DataEntrega { get; set; }
        public TimeSpan TempoGasto { get; set; }

        public virtual ICollection<OrdemServico> OrdensServicos { get; set; }
    }
}
