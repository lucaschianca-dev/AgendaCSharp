using AgendaCSharp.DTOs;
using AgendaCSharp.Models;

namespace AgendaCSharp.Mappers;

public class PacienteMapper
{
    public static Paciente ToEntidade(PacienteDTO dto)
    {
        return new Paciente(dto.Cpf, dto.Nome, dto.DataDeNascimento, dto.Idade)
        {
            Consultas = dto.Consultas.Select(ConsultaMapper.ToEntity).ToList()
        };
    }

    public static PacienteDTO ToDTO(Paciente entity)
    {
        return new PacienteDTO
        {
            Cpf = entity.Cpf,
            Nome = entity.Nome,
            DataDeNascimento = entity.DataDeNascimento,
            Idade = entity.Idade,
            Consultas = entity.Consultas.Select(ConsultaMapper.ToDTO).ToList()
        };
    }
}
