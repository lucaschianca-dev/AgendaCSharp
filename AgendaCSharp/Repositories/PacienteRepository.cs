using AgendaCSharp.Models;

namespace AgendaCSharp.Repositories;

public class PacienteRepository
{
    private List<Paciente> _pacientes = new List<Paciente>();

    public void AdicionarPaciente(Paciente paciente)
    {
        _pacientes.Add(paciente);
    }

    public List<Paciente> ListarPacientes()
    {
    return _pacientes;
    }

    public void RemoverPaciente(String cpf)
    {
        var paciente = _pacientes.FirstOrDefault(p => p.Cpf == cpf);

        if (paciente != null)
        {
            _pacientes.Remove(paciente);
        }
    }
}
