using System.ComponentModel.DataAnnotations;

namespace Simple.DDD.Domain.Contracts.Requests
{
    public class EmpresaRequest
    {
        [Required(ErrorMessage = "O campo 'Nome' é obrigatorio")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\s]*$", ErrorMessage = "Use apenas letras no campo 'Nome'")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo 'Cnpj' é obrigatorio")]
        public string Cnpj { get; set; }
    }
}
