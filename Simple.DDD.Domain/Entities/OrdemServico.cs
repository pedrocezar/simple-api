namespace Simple.DDD.Domain.Entities
{
    public class OrdemServico : BaseEntity
    {
        public DateTime Data { get; set; }
        public string Defeito { get; set; }
        public string Equipamento { get; set; }

        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }

        public int FinalizacaoId { get; set; }
        public virtual Finalizacao Finalizacao { get; set; }

        public virtual ICollection<Relacao> Relacoes { get; set; }
    }
}
