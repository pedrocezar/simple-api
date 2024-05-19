namespace Simple.DDD.Domain.Entities
{
    public class Usuario : BaseEntity
    {
        public string Nome { get; set; }
        public string Nacionalidade { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }

        public int EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }

        public int PerfilId { get; set; }
        public virtual Perfil Perfil { get; set; }

        public virtual ICollection<OrdemServico> OrdensServicos { get; set; }
    }
}
