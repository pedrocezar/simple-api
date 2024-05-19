namespace Simple.DDD.Domain.Entities
{
    public class Servico : BaseEntity
    {
        public string Atividade { get; set; }

        public virtual ICollection<Relacao> Relacoes { get; set; }
    }
}
