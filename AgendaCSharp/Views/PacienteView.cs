using AgendaCSharp.Models;
using AgendaCSharp.DTOs;
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
    private PacienteService _pacienteService;

    public PacienteView(IsNumerico isNumerico)
    {
        _isNumerico = isNumerico;
    }

    public void SetPacienteService(PacienteService pacienteService)
    {
        _pacienteService = pacienteService;
    }

    public PacienteDTO CapturarDados()
    {
        Console.WriteLine("\n ► Informe os dados do paciente:\n", Color.Aqua);

        string cpf;
        while (true)
        {
            Console.Write("CPF: ");
            cpf = Console.ReadLine();
            break;
        }

        string nome;
        while (true)
        {
            Console.Write("Nome: ");
            nome = Console.ReadLine();
            break;
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
                ExibirErro("ERRO", "Data inválida! Tente novamente.\n");
            }
        }
        return new PacienteDTO { Cpf = cpf, Nome = nome, DataDeNascimento = dataDeNascimento, Consultas = new List<ConsultaDTO>() };
    }

    public void ExibirPacientes(List<PacienteDTO> pacientes)
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
                Console.WriteLine($"            Agendado para: {consulta.Data:dd/MM/yyyy}\n            {consulta.HoraInicial:hh\\:mm} às {consulta.HoraFinal:hh\\:mm}", Color.Cyan);
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
        Console.Write(erro, Color.Crimson);
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
