namespace Simple.DDD.Domain.Entities
{
    public class Perfil : BaseEntity
    {
        public string Nome { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
