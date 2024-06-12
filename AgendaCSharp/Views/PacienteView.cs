using AgendaCSharp.Models;
using AgendaCSharp.Services;
using AgendaCSharp.Verificadores;
using Colorful;
using Console = Colorful.Console;
using System.Drawing;
using System.Collections.Generic;

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

    public Paciente CapturarDados()
    {
        Console.WriteLine("\n ► Informe os dados do paciente:\n", Color.Aqua);

        string cpfValidado;
        while (true)
        {
            Console.Write("CPF: ");
            var cpfSemValidacao = Console.ReadLine();

            if (string.IsNullOrEmpty(cpfSemValidacao))
            {
                ExibirErro("ERRO", " CPF não pode ser nulo ou vazio! Tente novamente.\n");
            }
            else if (cpfSemValidacao.Length != 11)
            {
                ExibirErro("ERRO", " CPF deve conter 11 dígitos! Tente novamente.\n");
            }
            else if (!IsNumerico.isAllDigits(cpfSemValidacao))
            {
                ExibirErro("ERRO", " CPF deve conter apenas números! Tente novamente.\n");
            }
            else if (_pacienteService.VerificarCpf(cpfSemValidacao))
            {
                ExibirErro("ERRO", " CPF já cadastrado! Tente novamente.\n");
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

            if (!string.IsNullOrEmpty(nomeSemValidacao) && nomeSemValidacao.Length >= 5 && nomeSemValidacao.Length <= 32)
            {
                nomeValidado = nomeSemValidacao;
                break;
            }
            else if (string.IsNullOrEmpty(nomeSemValidacao))
            {
                ExibirErro("ERRO", "- Nome não pode ser nulo! Tente novamente.\n");
            }
            else if (nomeSemValidacao.Length < 5)
            {
                ExibirErro("ERRO", "- Nome deve conter mais de 5 caracteres! Tente novamente.\n");
            }
            else if (nomeSemValidacao.Length >= 32)
            {
                ExibirErro("ERRO", "- Nome deve conter até 32 caracteres! Tente novamente.\n");
            }
            else
            {
                ExibirErro("ERRO", " - Nome Inválido! Tente novamente.\n");
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
                    ExibirErro("ERRO", "- A idade mínima para cadastro é 13 anos! Tente novamente.\n");
                }
            }
            else
            {
                ExibirErro("ERRO", "- Data inválida! Tente novamente.\n");
            }
        }

        return new Paciente(cpfValidado, nomeValidado, dataDeNascimento, idade);
    }

    public void ExibirPacientes(List<Paciente> pacientes)
    {
        ExibeLogoListaDeConsultas();
        Console.WriteLine("\n---------------------------------------------------------------------", Color.AntiqueWhite);
        ConstruirPlanilha("CPF", "Nome", "Dt. Nasc.", "Idade");
        Console.WriteLine("---------------------------------------------------------------------\n", Color.AntiqueWhite);
        foreach (var paciente in pacientes)
        {
            string dataNascimentoFormatada = paciente.DataDeNascimento.ToString("dd/MM/yyyy").PadRight(18);

            Console.WriteLine($"{paciente.Cpf.PadRight(11)} {paciente.Nome.PadRight(35)} {dataNascimentoFormatada} {paciente.Idade}", Color.AntiqueWhite);

            foreach (var consulta in paciente.Consultas.Where(c => c.Data > DateTime.Now || (c.Data == DateTime.Now.Date && c.HoraInicial > DateTime.Now.TimeOfDay)))
            {
                Console.WriteLine($"  Consulta - Data: {consulta.Data:dd/MM/yyyy}, Hora: {consulta.HoraInicial:hh\\:mm} - {consulta.HoraFinal:hh\\:mm}", Color.Cyan);
            }
        }
    }

    public string CapturarCpfParaRemocao()
    {
        Console.WriteLine();
        Console.Write("CPF: ");
        return Console.ReadLine();
    }

    public void ExibirErro(string erro, string mensagem)
    {
        Console.Write("\n[");
        Console.Write(erro, Color.Aqua);
        Console.Write("]");
        Console.WriteLine(mensagem);
    }

    public void ConstruirPlanilha(string cpf, string nome, string dataNascimento, string idade)
    {
        Console.WriteLine("{0,-11} {1,-32} {2,12} {3,11}", Color.Aqua, cpf, nome, dataNascimento, idade);
    }

    public void ExibirMensagem(string mensagem)
    {
        Console.Write(mensagem);
    }

    public void ExibirMensagemVerde(string mensagem)
    {
        Console.Write(mensagem, Color.LimeGreen);
    }

    public void ExibirMensagemVermelho(string mensagem)
    {
        Console.Write(mensagem, Color.Crimson);
    }
    public void ExibirMensagemAqua(string mensagem)
    {
        Console.Write(mensagem, Color.Aqua);
    }

    public void ExibeLogoListaDeConsultas()
    {
        string logoListaDeConsultas = @"
  _     _     _              _        ____            _            _            
 | |   (_)___| |_ __ _    __| | ___  |  _ \ __ _  ___(_) ___ _ __ | |_ ___  ___ 
 | |   | / __| __/ _` |  / _` |/ _ \ | |_) / _` |/ __| |/ _ \ '_ \| __/ _ \/ __|
 | |___| \__ \ || (_| | | (_| |  __/ |  __/ (_| | (__| |  __/ | | | ||  __/\__ \
 |_____|_|___/\__\__,_|  \__,_|\___| |_|   \__,_|\___|_|\___|_| |_|\__\___||___/
                                                                                
";
        Console.WriteLine(logoListaDeConsultas, Color.Aqua);
    }
}
