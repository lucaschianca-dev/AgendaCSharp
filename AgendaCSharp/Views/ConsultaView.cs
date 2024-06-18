using AgendaCSharp.Models;
using Colorful;
using Console = Colorful.Console;
using System.Drawing;
using AgendaCSharp.DTOs;

namespace AgendaCSharp.Views;

public class ConsultaView
{
    public string CapturarCpf()
    {
        ExibirMensagemAqua("\n ► Digite CPF do paciente a ser agendado\n");
        ExibirMensagemSimboloAqua("\n> ", "CPF: ");
        return Console.ReadLine();
    }

    public string CapturarCpfParaRemocao()
    {
        ExibirMensagemAqua("\n ► Digite CPF do paciente para cancelamento da consulta\n");
        ExibirMensagemSimboloAqua("\n> ", "CPF: ");
        return Console.ReadLine();
    }

    public ConsultaDTO CapturarDadosConsulta()
    {
        ExibirMensagemAqua("\n ► Digite os dados da consulta\n");

        DateTime data;
        while (true)
        {
            ExibirMensagemSimboloAqua("\n> ", "Data da Consulta (DDMMAAAA): ");
            var dataInput = Console.ReadLine();

            if (DateTime.TryParseExact(dataInput, "ddMMyyyy", null, System.Globalization.DateTimeStyles.None, out data))
            {
                break;
            }
            else
            {
                ExibirMensagemErro("[ERRO] Data inválida. Tente novamente.\n");
            }
        }

        TimeSpan horaInicial;
        while (true)
        {
            Console.Write("Hora Inicial (HHMM): ");
            var horaInicialInput = Console.ReadLine();
            if (TimeSpan.TryParseExact(horaInicialInput, "hhmm", null, out horaInicial))
            {
                break;
            }
            else
            {
                ExibirMensagemErro("[ERRO] Hora inicial inválida. Tente novamente.\n");
            }
        }

        TimeSpan horaFinal;
        while (true)
        {
            Console.Write("Hora Final (HHMM): ");
            var horaFinalInput = Console.ReadLine();
            if (TimeSpan.TryParseExact(horaFinalInput, "hhmm", null, out horaFinal))
            {
                break;
            }
            else
            {
                ExibirMensagemErro("[ERRO] Hora final inválida. Tente novamente.\n");
            }
        }

        return new ConsultaDTO { Data = data, HoraInicial = horaInicial, HoraFinal = horaFinal };
    }

    public void ExibirConsultas(List<ConsultaDTO> consultas)
    {
        ExibeLogoListaDeConsultas();
        Console.WriteLine("\n---------------------------------------------------------------------");
        Console.WriteLine("Data         H.Ini.   H.Fim");
        Console.WriteLine("---------------------------------------------------------------------\n");

        foreach (var consulta in consultas)
        {
            Console.WriteLine($"{consulta.Data:dd/MM/yyyy} {consulta.HoraInicial:hh\\:mm} {consulta.HoraFinal:hh\\:mm}", Color.AntiqueWhite);
        }
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
    public void ExibirMensagemErro(string erro)
    {
        Console.Write(erro, Color.Crimson);
    }
    public void ExibirMensagemSimboloAqua(string simbolo, string mensagem)
    {
        Console.Write(simbolo, Color.Aqua);
        Console.Write(mensagem);
    }
    public void ExibeLogoListaDeConsultas()
    {
        string logoListaDeConsultas = @"
  _     _     _              _         ____                      _ _            
 | |   (_)___| |_ __ _    __| | ___   / ___|___  _ __  ___ _   _| | |_ __ _ ___ 
 | |   | / __| __/ _` |  / _` |/ _ \ | |   / _ \| '_ \/ __| | | | | __/ _` / __|
 | |___| \__ \ || (_| | | (_| |  __/ | |__| (_) | | | \__ \ |_| | | || (_| \__ \
 |_____|_|___/\__\__,_|  \__,_|\___|  \____\___/|_| |_|___/\__,_|_|\__\__,_|___/
                                                                                
";
        Console.WriteLine(logoListaDeConsultas, Color.Aqua);
    }

    public void ExibirMensagemTentarNovamente()
    {
        ExibirMensagem("\nDeseja tentar novamente? (");
        ExibirMensagemAqua("y");
        ExibirMensagem("/");
        ExibirMensagemAqua("n");
        ExibirMensagem(")\n");
    }
}
