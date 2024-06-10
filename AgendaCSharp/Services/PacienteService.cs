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
        var pacienteExistente = _pacienteRepository.BuscarPacienteByCpf(paciente.Cpf);
        if (pacienteExistente != null)
        {
            throw new InvalidOperationException($"|ERRO| - CPF {paciente.Cpf} já está cadastrado!");
        }

        _pacienteRepository.AdicionarPaciente(paciente);
    }

    public List<Paciente> BuscarPacientes()
    {
        return _pacienteRepository.BuscarPacientes();
    }

    public void RemoverPacienteByCpf(string cpf)
    {
            if (string.IsNullOrWhiteSpace(cpf))
            {
                throw new ArgumentException("O CPF é obrigatório.");
            }

            _pacienteRepository.RemoverPacienteByCpf(cpf);
        }

    public bool VerificarCpf(string cpf)
    {
        return _pacienteRepository.BuscarPacienteByCpf(cpf) != null;
    }
}