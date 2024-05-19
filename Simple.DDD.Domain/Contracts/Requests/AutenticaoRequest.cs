using System.ComponentModel.DataAnnotations;

namespace Simple.DDD.Domain.Contracts.Requests
{
    public class AutenticaoRequest
    {
        [Required(ErrorMessage = "O campo 'Email' é obrigatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo 'Senha' é obrigatorio")]
        public string Senha { get; set; }
    }
}
