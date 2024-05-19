namespace Simple.DDD.Domain.Contracts.Requests
{
    public class OrdemServicoRequest
    {
        public DateTime Data { get; set; }
        public string Defeito { get; set; }
        public string Equipamento { get; set; }

        public int UsuarioId { get; set; }

        public int TecnicoId { get; set; }

        public int FinalizacaoId { get; set; }
    }
}
