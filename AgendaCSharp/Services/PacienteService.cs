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
}