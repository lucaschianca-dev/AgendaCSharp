using AgendaCSharp.Models;

namespace AgendaCSharp.Repositories;

public class ConsultaRepository
{
    private PacienteRepository _pacienteRepository;

    public ConsultaRepository(PacienteRepository pacienteRepository)
    {
        _pacienteRepository = pacienteRepository;
    }

    public void AdicionarConsulta(string cpf, Consulta consulta)
    {
        var paciente = _pacienteRepository.BuscarPacienteByCpf(cpf);

        if (paciente == null)
        {
            throw new InvalidOperationException($"|ERRO| - Paciente com CPF {cpf} não encontrado.");
        }

        if (paciente.Consultas.Any(c => c.Data > DateTime.Now || (c.Data == DateTime.Now.Date && c.HoraInicial > DateTime.Now.TimeOfDay)))
        {
            throw new InvalidOperationException($"|ERRO| - Paciente com CPF {cpf} já possui uma consulta futura.");
        }

        paciente.Consultas.Add(consulta);
    }

    public List<Consulta> BuscarConsultasPorCpf(string cpf)
    {
        var paciente = _pacienteRepository.BuscarPacienteByCpf(cpf);
        if (paciente == null)
        {
            throw new InvalidOperationException($"|ERRO| - Paciente com CPF {cpf} não encontrado.");
        }
        return paciente.Consultas;
    }

    public List<Consulta> BuscarTodasConsultas()
    {
        return _pacienteRepository.BuscarPacientes()
            .SelectMany(p => p.Consultas)
            .OrderBy(c => c.Data)
            .ThenBy(c => c.HoraInicial)
            .ToList();
    }

    public void RemoverConsultasPorCpf(string cpf)
    {
        var paciente = _pacienteRepository.BuscarPacienteByCpf(cpf);
        if (paciente != null)
        {
            paciente.Consultas.Clear();
        }
        else
        {
            throw new InvalidOperationException($"|ERRO| - Paciente com CPF {cpf} não encontrado.");
        }
    }
}
