using AgendaCSharp.DTOs;
using AgendaCSharp.Models;

namespace AgendaCSharp.Mappers;

public class ConsultaMapper
{
    public static Consulta ToEntity(ConsultaDTO dto)
    {
        return new Consulta(dto.Data, dto.HoraInicial, dto.HoraFinal);
    }

    public static ConsultaDTO ToDTO(Consulta entity)
    {
        return new ConsultaDTO
        {
            Data = entity.Data,
            HoraInicial = entity.HoraInicial,
            HoraFinal = entity.HoraFinal
        };
    }
}
