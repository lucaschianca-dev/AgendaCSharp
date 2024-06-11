using AgendaCSharp.Models;
using AgendaCSharp.Verificadores;

namespace AgendaCSharp.Views;

public class ConsultaView
{
    public Consulta CapturarDadosConsulta()
    {
        Console.WriteLine("Digite os dados da consulta");


        string cpfPacienteValidado;
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
                cpfPacienteValidado = cpfSemValidacao;
                break;
            }
        }

        DateTime data;
        while (true)
        {
            Console.Write("Data da Consulta (DDMMAAAA): ");
            var dataInput = Console.ReadLine();
            if (DateTime.TryParseExact(dataInput, "ddMMyyyy", null, System.Globalization.DateTimeStyles.None, out data))
            {
                break;
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

        return new Consulta(cpfPacienteValidado,data, horaInicial, horaFinal);  
    }

    public void exibirConsultas(List<Consulta> consultas)
    {
        foreach (var consulta in consultas)
        {
            Console.WriteLine($"{consulta.CpfPaciente} {consulta.Data} {consulta.HoraInicial} {consulta.HoraFinal}");
        }
    }
}
