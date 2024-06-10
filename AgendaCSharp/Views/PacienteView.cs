using AgendaCSharp.Models;
using AgendaCSharp.Verificadores;

namespace AgendaCSharp.Views;

public class PacienteView
{
    private readonly IsNumerico _isNumerico;

    public PacienteView(IsNumerico isNumerico)
    {
        _isNumerico = isNumerico;
    }
    public void ExibirMensagem(string mensagem)
    {
        Console.WriteLine(mensagem);
    }

    public Paciente CapturarDados()
    {
        Console.WriteLine("-------------------------------");
        Console.WriteLine(" Informe os dados do paciente:");
        Console.WriteLine("-------------------------------");

        string cpfValidado;
        while (true)
        {
            Console.Write("CPF: ");
            var cpfSemValidacao = Console.ReadLine();

            if (string.IsNullOrEmpty(cpfSemValidacao))
            {
                Console.WriteLine("\n|ERRO| - CPF não pode ser nulo ou vazio! Tente novamente.\n");
            }
            else if (cpfSemValidacao.Length != 11)
            {
                Console.WriteLine("\n|ERRO| - CPF deve conter 11 dígitos numéricos! Tente novamente.\n");
            }
            else if (!IsNumerico.isAllDigits(cpfSemValidacao))
            {
                Console.WriteLine("\n|ERRO| - CPF deve conter apenas números! Tente novamente.\n");
            }
            else
            {
                cpfValidado = cpfSemValidacao;
                break;
            }
        }

        string nomeValidado;
        while (true)
        {
            Console.Write("Nome: ");
            var nomeSemValidacao = Console.ReadLine();

            if (!string.IsNullOrEmpty(nomeSemValidacao) && nomeSemValidacao.Length > 1 && nomeSemValidacao.Length <= 32)
            {
                nomeValidado = nomeSemValidacao;
                break;
            }
            else if (string.IsNullOrEmpty(nomeSemValidacao))
            {
                Console.WriteLine("\n|ERRO| - Nome não pode ser nulo! Tente novamente.\n");
            }
            else if(nomeSemValidacao.Length <= 1)
            {
                Console.WriteLine("\n|ERRO| - Nome deve conter mais de 1 caractere! Tente novamente.\n");
            }
            else if (nomeSemValidacao.Length >= 32)
            {
                Console.WriteLine("\n|ERRO| - Nome deve conter até 32 caracteres! Tente novamente.\n");
            }
            else
            {
                Console.WriteLine("\n|ERRO| - Nome Inválido! Tente novamente.\n");
            }
        }

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

        return new Paciente(cpfValidado, nomeValidado, dataDeNascimento);
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
