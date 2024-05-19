namespace Simple.DDD.Domain.Contracts.Responses
{
    public class FinalizacaoResponse : BaseResponse
    {
        public float ValorTotal { get; set; }
        public DateTime Data { get; set; }
        public DateTime DataEntrega { get; set; }
        public TimeSpan TempoGasto { get; set; }
    }
}
