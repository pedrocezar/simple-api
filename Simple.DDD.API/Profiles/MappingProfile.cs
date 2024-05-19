using Simple.DDD.Domain.Contracts.Requests;
using Simple.DDD.Domain.Contracts.Responses;
using Simple.DDD.Domain.Entities;
using AutoMapper;

namespace Simple.DDD.API.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Entity to Request

            CreateMap<UsuarioRequest, Usuario>();
            CreateMap<EmpresaRequest, Empresa>();
            CreateMap<FinalizacaoRequest, Finalizacao>();
            CreateMap<OrdemServicoRequest, OrdemServico>();
            CreateMap<ServicoRequest, Servico>();
            CreateMap<PerfilRequest, Perfil>();

            #endregion

            #region Response to Entity

            CreateMap<Usuario, UsuarioResponse>();
            CreateMap<Empresa, EmpresaResponse>();
            CreateMap<Finalizacao, FinalizacaoResponse>();
            CreateMap<OrdemServico, OrdemServicoResponse>();
            CreateMap<Servico, ServicoResponse>();
            CreateMap<Perfil, PerfilResponse>();
            CreateMap<Nacionalidade, NacionalidadeResponse>()
                .ForMember(dst => dst.Sigla, map => map.MapFrom(src => src.Country.FirstOrDefault().CountryId))
                .ForMember(dst => dst.Probabilidade, map => map.MapFrom(src => src.Country.FirstOrDefault().Probability * 100));

            #endregion
        }
    }
}
