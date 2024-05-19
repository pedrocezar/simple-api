namespace Simple.DDD.Domain.Contracts.Responses
{
    public class AutenticaoResponse
    {
        public string Token { get; set; }
        public DateTime? DataExpiracao { get; set; }
    }
}
