using AgendaCSharp.Models;
using Colorful;
using Console = Colorful.Console;
using System.Drawing;

namespace AgendaCSharp.Views;

public class ConsultaView
{
    public string CapturarCpf()
    {
        Console.Write("CPF: ");
        return Console.ReadLine();
    }

    public Consulta CapturarDadosConsulta()
    {
        Console.WriteLine("\n>> Digite os dados da consulta <<\n");

        DateTime data;
        while (true)
        {
            Console.Write("Data da Consulta (DDMMAAAA): ");
            var dataInput = Console.ReadLine();

            if (DateTime.TryParseExact(dataInput, "ddMMyyyy", null, System.Globalization.DateTimeStyles.None, out data))
            {
                if (data >= DateTime.Now.Date)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("A consulta deve ser para uma data futura. Tente novamente.");
                }
            }
            else
            {
                Console.WriteLine("Data inválida. Tente novamente.");
            }
        }

        TimeSpan horaInicial;
        while (true)
        {
            Console.Write("Hora Inicial (HHMM): ");
            var horaInicialInput = Console.ReadLine();
            if (TimeSpan.TryParseExact(horaInicialInput, "hhmm", null, out horaInicial) && horaInicial.Minutes % 15 == 0)
            {
                if (horaInicial >= new TimeSpan(8, 0, 0) && horaInicial <= new TimeSpan(19, 0, 0))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Hora inicial fora do horário de funcionamento (08:00 - 19:00). Tente novamente.");
                }
            }
            else
            {
                Console.WriteLine("Hora inicial inválida ou não é múltiplo de 15 minutos. Tente novamente.");
            }
        }

        TimeSpan horaFinal;
        while (true)
        {
            Console.Write("Hora Final (HHMM): ");
            var horaFinalInput = Console.ReadLine();
            if (TimeSpan.TryParseExact(horaFinalInput, "hhmm", null, out horaFinal) && horaFinal.Minutes % 15 == 0)
            {
                if (horaFinal > horaInicial && horaFinal >= new TimeSpan(8, 0, 0) && horaFinal <= new TimeSpan(19, 0, 0))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Hora final deve ser após a hora inicial e dentro do horário de funcionamento (08:00 - 19:00). Tente novamente.");
                }
            }
            else
            {
                Console.WriteLine("Hora final inválida ou não é múltiplo de 15 minutos. Tente novamente.");
            }
        }

        return new Consulta(data, horaInicial, horaFinal);
    }

    public void ExibirConsultas(List<Consulta> consultas)
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
    public void ExibirMensagemErro(string erro, string mensagem)
    {
        Console.Write("\n|");
        Console.Write(erro, Color.Crimson);
        Console.Write("| ");
        Console.Write(mensagem);
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
}
