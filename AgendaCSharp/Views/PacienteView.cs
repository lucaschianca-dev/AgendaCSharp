using AgendaCSharp.Models;

namespace AgendaCSharp.Views;

public class PacienteView
{
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
}
