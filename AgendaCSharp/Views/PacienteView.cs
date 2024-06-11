using AgendaCSharp.Models;
using AgendaCSharp.Services;
using AgendaCSharp.Verificadores;

namespace AgendaCSharp.Views;

public class PacienteView
{
    private readonly IsNumerico _isNumerico;
    private readonly PacienteService _pacienteService;

    public PacienteView(IsNumerico isNumerico, PacienteService pacienteService)
    {
        _isNumerico = isNumerico;
        _pacienteService = pacienteService;
    }

    public void ExibirMensagem(string mensagem)
    {
        Console.WriteLine(mensagem);
    }

    public Paciente CapturarDados()
    {
        ExibirMensagem("-------------------------------");
        ExibirMensagem(" Informe os dados do paciente:");
        ExibirMensagem("-------------------------------");

        string cpfValidado;
        while (true)
        {
            Console.Write("CPF: ");
            var cpfSemValidacao = Console.ReadLine();

            if (string.IsNullOrEmpty(cpfSemValidacao))
            {
                ExibirMensagem("\n|ERRO| - CPF não pode ser nulo ou vazio! Tente novamente.\n");
            }
            else if (cpfSemValidacao.Length != 11)
            {
                ExibirMensagem("\n|ERRO| - CPF deve conter 11 dígitos! Tente novamente.\n");
            }
            else if (!IsNumerico.isAllDigits(cpfSemValidacao))
            {
                ExibirMensagem("\n|ERRO| - CPF deve conter apenas números! Tente novamente.\n");
            }
            else if (_pacienteService.VerificarCpf(cpfSemValidacao))
            {
                ExibirMensagem("\n|ERRO| - CPF já cadastrado! Tente novamente.\n");
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
        int idade;
        while (true)
        {
            Console.Write("Data de Nascimento (DDMMAAAA): ");
            var dataInput = Console.ReadLine();

            
            if (DateTime.TryParseExact(dataInput, "ddMMyyyy", null, System.Globalization.DateTimeStyles.None, out dataDeNascimento))
            {
                idade = VerificaDataDeNascimento.CalcularIdade(dataDeNascimento);
                
                if (idade >= 13)
                {
                    break;
                }
                else
                {
                    ExibirMensagem("\n|ERRO| - A idade mínima para cadastro é 13 anos. Tente novamente.\n");
                }
            }
            else
            {
                Console.WriteLine("Data inválida. Tente novamente.");
            }
        }

        return new Paciente(cpfValidado, nomeValidado, dataDeNascimento, idade);
    }

    public void ExibirPacientes (List<Paciente> pacientes)
    {
        ExibirMensagem("\n---------------------------------------------------------------------");
        ExibirMensagem("CPF         Nome                                Dt. Nasc.       Idade");
        ExibirMensagem("---------------------------------------------------------------------\n");
        foreach (var paciente in pacientes)
        {
            string dataNascimentoFormatada = paciente.DataDeNascimento.ToString("dd/MM/yyyy").PadRight(18);
            Console.WriteLine($"{paciente.Cpf.PadRight(11)} {paciente.Nome.PadRight(35)} {dataNascimentoFormatada} {paciente.Idade}");
            foreach (var consulta in paciente.Consultas.Where(c => c.Data > DateTime.Now || (c.Data == DateTime.Now.Date && c.HoraInicial > DateTime.Now.TimeOfDay)))
            {
                Console.WriteLine($"  Consulta - Data: {consulta.Data:dd/MM/yyyy}, Hora: {consulta.HoraInicial:hh\\:mm} - {consulta.HoraFinal:hh\\:mm}");
            }
        }
    }

    public string CapturarCpfParaRemocao()
    {
        Console.WriteLine();
        Console.Write("CPF: ");
        return Console.ReadLine();
    }
}
