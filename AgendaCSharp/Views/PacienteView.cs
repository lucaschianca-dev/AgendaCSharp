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
        Console.WriteLine("-------------------------------");
        Console.WriteLine(" Informe os dados do paciente:");
        Console.WriteLine("-------------------------------");
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
        ExibirMensagem("\n---------------------------------------------------------------------");
        ExibirMensagem("CPF         Nome                                Dt. Nasc.       Idade");
        ExibirMensagem("---------------------------------------------------------------------\n");
        foreach (var paciente in pacientes)
        {
            Console.WriteLine($"{paciente.Cpf.PadRight(11)} {paciente.Nome.PadRight(35)} {paciente.DataDeNascimento:dd/MM/yyyy} 18");
        }
    }

    public string CapturarCpfParaRemocao()
    {
        Console.WriteLine();
        Console.Write("CPF: ");
        return Console.ReadLine();
    }
}
