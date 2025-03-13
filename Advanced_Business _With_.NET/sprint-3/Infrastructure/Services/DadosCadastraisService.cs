using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class DadosCadastraisService : IDadosCadastraisService
{
    private readonly IDadosCadastraisRepository _dadosCadastraisRepository;

    public DadosCadastraisService(IDadosCadastraisRepository dadosCadastraisRepository)
    {
        _dadosCadastraisRepository = dadosCadastraisRepository;
    }

    public DadosCadastraisDTO ObterDadosCadastraisPorUsuarioId(string userId)
    {
        var usuario = _dadosCadastraisRepository.ObterUsuarioId(userId);
        var endereco = _dadosCadastraisRepository.ObterEnderecoId(userId);
        var diasPreferencia = _dadosCadastraisRepository.ObterDiasId(userId);
        var turnos = _dadosCadastraisRepository.ObterTurnosId(userId);
        var horarios = _dadosCadastraisRepository.ObterHorariosId(userId);

        // Verifica se o usuário existe
        if (usuario == null)
        {
            throw new Exception($"Usuário com ID {userId} não encontrado.");
        }

        return new DadosCadastraisDTO
        {
            Usuario = new UsuarioDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Telefone = usuario.Telefone,
                Senha = usuario.Senha,
                Perfil = usuario.Perfil
            },
            Endereco = endereco != null && endereco.Rua != null && endereco.CEP != null && endereco.Cidade != null && endereco.Estado != null && endereco.Bairro != null
                ? new EnderecoDTO
                {
                    CEP = endereco.CEP,
                    Estado = endereco.Estado,
                    Cidade = endereco.Cidade,
                    Bairro = endereco.Bairro,
                    Rua = endereco.Rua
                    
                    
                }
                : new EnderecoDTO(),


            DiasPreferencia = diasPreferencia != null
                ? diasPreferencia.Select(dp => new DiasPreferenciaDTO
                {
                    DiasSemana = dp.DiasSemana
                }).ToList()
                : new List<DiasPreferenciaDTO>(),

            TurnosPreferencia = turnos != null
                ? turnos.Select(t => new TurnoDTO
                {
                    TurnoPreferencia = t.TurnoPreferencia
                }).ToList()
                : new List<TurnoDTO>(),

            HorariosPreferencia = horarios != null
                ? horarios.Select(h => new HorariosDTO
                {
                    HorariosPreferencia = h.HorariosPreferencia
                }).ToList()
                : new List<HorariosDTO>()
        };
    }
}
