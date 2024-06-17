using AgendaCSharp.Models;

namespace AgendaCSharp.Repositories;

public class PacienteRepository
{
    private List<Paciente> _pacientes = new List<Paciente>();

    public void AdicionarPaciente(Paciente paciente) => _pacientes.Add(paciente);

    public List<Paciente> BuscarTodosPacientes() => _pacientes;

    public List<Paciente> BuscarTodosPacientesPorNome() => _pacientes.OrderBy(p => p.Nome).ToList();

    public List<Paciente> BuscarTodosPacientesPorCpf() => _pacientes.OrderBy(p => p.Cpf).ToList();

    public Paciente BuscarPacienteByCpf(string cpf) => _pacientes.FirstOrDefault(p => p.Cpf == cpf);

    public void RemoverPacienteByCpf(String cpf)
    {
        var paciente = BuscarPacienteByCpf(cpf);
        if (paciente != null)
        {
            _pacientes.Remove(paciente);
        }
        else
        {
            throw new InvalidOperationException($"|ERRO| - Nenhum paciente cadastrado com o CPF: {cpf}\n");
        }
    }
}
