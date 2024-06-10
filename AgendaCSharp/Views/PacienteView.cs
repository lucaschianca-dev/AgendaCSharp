using AgendaCSharp.Models;

namespace AgendaCSharp.Views;

public class PacienteView
{
    public void ExibirMensagem(string mensagem)
    {
        Console.WriteLine(mensagem);
    }

    public Paciente CapturarDados()
    {
        Console.WriteLine("Informe os dados do paciente:");
        Console.Write("CPF: ");
        var cpf = Console.ReadLine();
        Console.Write("Nome: ");
        var nome = Console.ReadLine();

        DateTime dataDeNascimento;
        while (true)
        {
            Console.Write("Data de Nascimento (DDMMAAAA): ");
            var dataInput = Console.ReadLine();
            if (DateTime.TryParseExact(dataInput, "ddMMyyyy", null, System.Globalization.DateTimeStyles.None, out dataDeNascimento))
            {
                break;
            }
            else
            {
                Console.WriteLine("Data inválida. Tente novamente.");
            }
        }

        return new Paciente(cpf, nome, dataDeNascimento);
    }

    public void ExibirPacientes (List<Paciente> pacientes)
    {
        Console.WriteLine("Lista de Pacientes:");
        foreach (var paciente in pacientes)
        {
            Console.WriteLine($"CPF: {paciente.Cpf}, Nome: {paciente.Nome}, Data de Nascimento: {paciente.DataDeNascimento:dd/MM/yyyy}");
        }
    }

    public string CapturarCpfParaRemocao()
    {
        Console.Write("Informe o CPF do paciente a ser removido: ");
        return Console.ReadLine();
    }
}
