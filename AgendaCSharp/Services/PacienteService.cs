using AgendaCSharp.Models;
using AgendaCSharp.Repositories;

namespace AgendaCSharp.Services;

public class PacienteService
{
    private readonly PacienteRepository _pacienteRepository;
    private readonly ConsultaRepository _consultaRepository;

    public PacienteService(PacienteRepository pacienteRepository, ConsultaRepository consultaRepository)
    {
        _pacienteRepository = pacienteRepository;
        _consultaRepository = consultaRepository;
    }

    public void AdicionarPaciente(Paciente paciente)
    {
        var pacienteExistente = _pacienteRepository.BuscarPacienteByCpf(paciente.Cpf);
        if (pacienteExistente != null)
        {
            throw new InvalidOperationException($"\n - CPF {paciente.Cpf} já está cadastrado!\n");
        }

        _pacienteRepository.AdicionarPaciente(paciente);
    }

    public List<Paciente> BuscarTodosPacientes()
    {
        return _pacienteRepository.BuscarTodosPacientes();
    }

    public List<Paciente> BuscarTodosPacientesByCpf()
    {
        return _pacienteRepository.BuscarTodosPacientesByCpf();
    }

    public List<Paciente> BuscarTodosPacientesByNome()
    {
        return _pacienteRepository.BuscarTodosPacientesByNome();
    }

    public void RemoverPacienteByCpf(string cpf)
    {
        Paciente paciente = _pacienteRepository.BuscarPacienteByCpf(cpf);

        if (string.IsNullOrEmpty(cpf))
        {
            throw new ArgumentException("CPF não pode ser nulo ou vazio.\n");
        }

        if (paciente == null)
        {
            throw new InvalidOperationException($"Paciente com CPF {cpf} não encontrado.\n");
        }

        var consultasFuturas = paciente.Consultas
                .Where(c => c.Data >= DateTime.Now)
                .ToList();

        if (consultasFuturas.Any())
        {
            throw new InvalidOperationException($"O paciente com CPF {cpf} possui consultas futuras agendadas.\n");
        }
        _pacienteRepository.RemoverPacienteByCpf(cpf);
    }

    public bool VerificarCpf(string cpf)
    {
        return _pacienteRepository.BuscarPacienteByCpf(cpf) != null;
    }

    public void AdicionarConsulta(string cpf, Consulta consulta)
    {
        _consultaRepository.AdicionarConsulta(cpf, consulta);
    }
}