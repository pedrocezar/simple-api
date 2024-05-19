namespace Simple.DDD.Domain.Contracts.Requests
{
    public class FinalizacaoRequest
    {
        public float ValorTotal { get; set; }
        public DateTime Data { get; set; }
        public DateTime DataEntrega { get; set; }
        public TimeSpan TempoGasto { get; set; }
    }
}
