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
        paciente.Consultas.Add(consulta);
    }

    public List<Consulta> BuscarConsultasByCpf(string cpf)
    {
        var paciente = _pacienteRepository.BuscarPacienteByCpf(cpf);
        if (paciente == null)
        {
            return new List<Consulta>();
        }
        return paciente.Consultas;
    }

    public List<Consulta> BuscarTodasConsultas()
    {
        return _pacienteRepository.BuscarTodosPacientes()
            .SelectMany(p => p.Consultas)
            .OrderBy(c => c.Data)
            .ThenBy(c => c.HoraInicial)
            .ToList();
    }

    public void RemoverConsultasByCpf(string cpf)
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
