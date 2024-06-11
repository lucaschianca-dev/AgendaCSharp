using AgendaCSharp.Models;
using AgendaCSharp.Repositories;

namespace AgendaCSharp.Services;

public class ConsultaService
{
    private readonly ConsultaRepository _consultaRepository;
    private readonly PacienteRepository _pacienteRepository;

    public ConsultaService(ConsultaRepository consultaRepository, PacienteRepository pacienteRepository)
    {
        _consultaRepository = consultaRepository;
        _pacienteRepository = pacienteRepository;
    }

    public void AdicionarConsulta(string cpf, Consulta consulta)
    {
        var paciente = _pacienteRepository.BuscarPacienteByCpf(cpf);

        if (consulta.Data <= DateTime.Now.Date && consulta.HoraInicial <= DateTime.Now.TimeOfDay)
        {
            throw new InvalidOperationException("|ERRO| - A consulta deve ser agendada para uma data e hora futuras.");
        }

        if (paciente.Consultas.Any(c => c.Data == consulta.Data &&
            ((consulta.HoraInicial >= c.HoraInicial && consulta.HoraInicial < c.HoraFinal) ||
            (consulta.HoraFinal > c.HoraInicial && consulta.HoraFinal <= c.HoraFinal))))
        {
            throw new InvalidOperationException("|ERRO| - Horário de consulta sobreposto com uma consulta existente.");
        }

        _consultaRepository.AdicionarConsulta(cpf, consulta);
    }

    public List<Consulta> BuscarTodasConsultas()
    {
        return _consultaRepository.BuscarTodasConsultas();
    }

    public List<Consulta> BuscarConsultasPorCpf(string cpf)
    {
        return _consultaRepository.BuscarConsultasPorCpf(cpf);
    }
}
