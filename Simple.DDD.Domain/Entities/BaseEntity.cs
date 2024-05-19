namespace Simple.DDD.Domain.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            Ativo = true;
        }

        public int Id { get; set; }

        public bool Ativo { get; set; }

        public int? UsuarioInclusao { get; set; }
        public DateTime DataInclusao { get; set; }

        public int? UsuarioAlteracao { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}
