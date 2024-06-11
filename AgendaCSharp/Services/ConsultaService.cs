using AgendaCSharp.Models;
using AgendaCSharp.Repositories;

namespace AgendaCSharp.Services;

public class ConsultaService
{
    private ConsultaRepository _consultaRepository;

    public ConsultaService(ConsultaRepository consultaRepository)
    {
        _consultaRepository = consultaRepository;
    }

    public void AdicionarConsulta(Consulta consulta)
    {
        _consultaRepository.AdicionarConsulta(consulta);
    }

    public List<Consulta> BuscarTodasConsultas()
    {
        return _consultaRepository.BuscarTodasConsultas();
    }
}
