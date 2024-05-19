using System.ComponentModel;

namespace Simple.DDD.Domain.Enums
{
    public enum StatusException
    {
        [Description("Nenhum")]
        Nenhum = 0,
        [Description("Ocorreu algo inesperado")]
        Erro = 1,
        [Description("Dado não encontrado")]
        NaoEncontrado = 2,
        [Description("Acesso não autorizado")]
        NaoAutorizado = 3,
        [Description("Campo(s) obrigatório(s) não informado(s)")]
        Obrigatoriedade = 4,
        [Description("Campo(s) com formato(s) incorreto(s)")]
        FormatoIncorreto = 5,
        [Description("Dado não processado")]
        NaoProcessado = 6,
        [Description("Acesso proibido")]
        AcessoProibido = 7
    }
}
