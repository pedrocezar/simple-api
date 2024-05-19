using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.DDD.Domain.Contracts.Responses
{
    public class EmpresaResponse : BaseResponse
    {
        public string Nome { get; set; }
        public string Cnpj { get; set; }
    }
}
