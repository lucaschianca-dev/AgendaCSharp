using AgendaCSharp.Models;
using AgendaCSharp.Repositories;

namespace AgendaCSharp.Services;

public class PacienteService
{
    private PacienteRepository _pacienteRepository;

    public PacienteService(PacienteRepository pacienteRepository)
    {
        _pacienteRepository = pacienteRepository;
    }

    public void AdicionarPaciente(Paciente paciente)
    {
        _pacienteRepository.AdicionarPaciente(paciente);
    }

    public List<Paciente> BuscarPacientes()
    {
        return _pacienteRepository.BuscarPacientes();
    }

    public void RemoverPacienteByCpf(string cpf)
    {
        _pacienteRepository.RemoverPacienteByCpf(cpf);
    }
}