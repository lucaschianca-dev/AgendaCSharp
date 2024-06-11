﻿using AgendaCSharp.Controllers;
using AgendaCSharp.Repositories;
using AgendaCSharp.Services;
using AgendaCSharp.Verificadores;
using AgendaCSharp.Views;

class Program
{
    public static void Main(string[] args)
    {
        IsNumerico isNumerico = new IsNumerico();

        var pacienteRepository = new PacienteRepository();
        var consultaRepository = new ConsultaRepository(pacienteRepository);
        var pacienteService = new PacienteService(pacienteRepository, consultaRepository);
        var consultaService = new ConsultaService(consultaRepository, pacienteRepository);

        var pacienteView = new PacienteView(isNumerico, pacienteService);
        var pacienteController = new PacienteController(pacienteService, pacienteView);

        var consultaView = new ConsultaView();
        var consultaController = new ConsultaController(consultaService, pacienteService, consultaView);


        bool sair = false;
        while (!sair)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Cadastrar Paciente");
            Console.WriteLine("2. Listar Pacientes");
            Console.WriteLine("3. Remover Paciente");
            Console.WriteLine("4. Cadastrar Consulta");
            Console.WriteLine("5. Listar Consulta");
            Console.WriteLine("6. Sair");
            Console.Write("Escolha uma opção: ");
            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    pacienteController.CadastrarPaciente();
                    break;
                case "2":
                    pacienteController.ListarPadicentes();
                    break;
                case "3":
                    pacienteController.ExcluirPacienteByCpf();
                    break;
                case "4":
                    Console.WriteLine("--------- Cadastrar Consulta ---------");
                    consultaController.CadastrarConsulta();
                    break;
                case "5":
                    consultaController.ListarTodasConsultas();
                    break;
                case "6":
                    sair = true;
                    break;
                default:
                    Console.WriteLine("Opção inválida, tente novamente.");
                    break;
            }
        }
    }
}