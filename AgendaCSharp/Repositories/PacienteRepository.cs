using AgendaCSharp.Models;

namespace AgendaCSharp.Repositories;

public class PacienteRepository
{
    private List<Paciente> _pacientes = new List<Paciente>();

    public void AdicionarPaciente(Paciente paciente)
    {
        _pacientes.Add(paciente);
    }

    public List<Paciente> BuscarPacientes()
    {
    return _pacientes;
    }

    public Paciente BuscarPacienteByCpf(string cpf)
    { 
        return _pacientes.FirstOrDefault(p => p.Cpf == cpf);
    }

    public void RemoverPacienteByCpf(String cpf)
    {
        var paciente = _pacientes.FirstOrDefault(p => p.Cpf == cpf);

        if (paciente != null)
        {
            _pacientes.Remove(paciente);
        }
    }
}
