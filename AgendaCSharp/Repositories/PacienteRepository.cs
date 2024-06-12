using AgendaCSharp.Models;

namespace AgendaCSharp.Repositories;

public class PacienteRepository
{
    private List<Paciente> _pacientes = new List<Paciente>();

    public void AdicionarPaciente(Paciente paciente)
    {
        _pacientes.Add(paciente);
    }

    public List<Paciente> BuscarTodosPacientes()
    {
        return _pacientes;
    }

    public List<Paciente> BuscarTodosPacientesByNome()
    {
        return _pacientes.OrderBy(p => p.Nome).ToList();
    }

    public List<Paciente> BuscarTodosPacientesByCpf()
    {
        return _pacientes.OrderBy(p => p.Cpf).ToList();
    }

    public Paciente BuscarPacienteByCpf(string cpf)
    {
        try
        {
            return _pacientes.FirstOrDefault(p => p.Cpf == cpf);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao buscar paciente por CPF no repositório.", ex);
        }
    }

    public void RemoverPacienteByCpf(String cpf)
    {
        var paciente = _pacientes.FirstOrDefault(p => p.Cpf == cpf);
        if (paciente != null)
        {
            _pacientes.Remove(paciente);
        }
        else
        {
            Console.WriteLine("");
            throw new InvalidOperationException($"|ERRO| - Nenhum paciente cadastrado com o CPF: {cpf}\n");
        }
    }
}
