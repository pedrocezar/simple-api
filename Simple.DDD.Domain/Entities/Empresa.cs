namespace Simple.DDD.Domain.Entities
{
    public class Empresa : BaseEntity
    {
        public string Nome { get; set; }
        public string Cnpj { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
