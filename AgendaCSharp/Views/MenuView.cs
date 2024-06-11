﻿using AgendaCSharp.Controllers;
using System.Security.Cryptography.X509Certificates;

namespace AgendaCSharp.Views;

public class MenuView
{
    public void LogoInicial()
    {
        Console.WriteLine(logoInicial);
    }

    public void ConsoleTitulo()
    {
        Console.Title = "Consultório Odontológico";
    }

    public void ExibirMenu(PacienteController pacienteController, ConsultaController consultaController)
    {
        bool sair = false;
        while (!sair)
        {
            Console.Clear();
            Console.WriteLine(mainMenu);
            OpcoesMenu("1", "Cadastro de pacientes");
            OpcoesMenu("2", "Agenda");
            OpcoesMenu("3", "Sair\n");
            Console.Write("Escolha uma opção: ");
            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Console.Clear();
                    ExibirMenuPacientes(pacienteController);
                    break;
                case "2":
                    Console.Clear();
                    ExibirMenuConsultas(consultaController);
                    break;
                case "3":
                    sair = true;
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("\n|ERRO| - Opção inválida, tente novamente.\n");
                    break;
            }
        }
    }

    public void ExibirMenuPacientes(PacienteController pacienteController)
    {
        bool voltar = false;
        while (!voltar)
        {
            Console.Clear();
            Console.WriteLine(logoCadastroPacientes);
            OpcoesMenu("1", "Cadastrar paciente");
            OpcoesMenu("2", "Excluir paciente");
            OpcoesMenu("3", "Listar pacientes (ordenado por CPF)");
            OpcoesMenu("4", "Listar pacientes (ordenado por nome)");
            OpcoesMenu("5", "Voltar p/ Menu Principal");
            Console.Write("\nEscolha uma opção: ");
            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    pacienteController.CadastrarPaciente();
                    break;
                case "2":
                    pacienteController.ExcluirPacienteByCpf();
                    break;
                case "3":
                    //pacienteController.ListarPacientesByCpf();
                    break;
                case "4":
                    //pacienteController.ListarPacientesByNome();
                    break;
                case "5":
                    voltar = true;
                    break;
                default:
                    Console.WriteLine("\n|ERRO| - Opção inválida, tente novamente.\n");
                    break;
            }
        }
    }

    public void ExibirMenuConsultas(ConsultaController consultaController)
    {
        bool voltar = false;
        while (!voltar)
        {
            Console.WriteLine(agenda);
            OpcoesMenu("1", "Agendar Consulta");
            OpcoesMenu("2", "Cancelar Agendamento");
            OpcoesMenu("3", "Listar Agenda");
            OpcoesMenu("4", "Voltar p/ Menu Principal");
            Console.WriteLine("\nEscolha uma opção: ");
            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    consultaController.CadastrarConsulta();
                    Console.WriteLine("\nPressione qualquer tecla para voltar ao Menu...\n");
                    Console.ReadKey(true);
                    Console.Clear();
                    break;
                case "2":
                    //consultaController.ExcluirConsulta();
                    Console.WriteLine("\nPressione qualquer tecla para voltar ao Menu...\n");
                    Console.ReadKey(true);
                    Console.Clear();
                    break;
                case "3":
                    Console.Clear();
                    consultaController.ListarTodasConsultas();

                    Console.WriteLine("\nPressione qualquer tecla para voltar ao Menu...\n");
                    Console.ReadKey(true);
                    voltar = true;
                    break;
                case "4":
                    voltar = true;
                    break;
                default:
                    Console.WriteLine("\n|ERRO| - Opção inválida, tente novamente.\n");
                    break;
            }
        }
    }

    public void OpcoesMenu(string numero, string mensagem)
    {
        Console.Write("[");
        Console.Write(numero, ConsoleColor.Red);
        Console.WriteLine("] " + mensagem);
    }

    string mainMenu = @"
  __  __                  
 |  \/  | ___ _ __  _   _ 
 | |\/| |/ _ \ '_ \| | | |
 | |  | |  __/ | | | |_| |
 |_|  |_|\___|_| |_|\__,_|
                          
";

    string logoInicial = @"
     _                        _          ___      _             _        _   __        _           
    / \   __ _  ___ _ __   __| | __ _   / _ \  __| | ___  _ __ | |_ ___ | | /_/   __ _(_) ___ __ _ 
   / _ \ / _` |/ _ \ '_ \ / _` |/ _` | | | | |/ _` |/ _ \| '_ \| __/ _ \| |/ _ \ / _` | |/ __/ _` |
  / ___ \ (_| |  __/ | | | (_| | (_| | | |_| | (_| | (_) | | | | || (_) | | (_) | (_| | | (_| (_| |
 /_/   \_\__, |\___|_| |_|\__,_|\__,_|  \___/ \__,_|\___/|_| |_|\__\___/|_|\___/ \__, |_|\___\__,_|
         |___/                                                                   |___/             
";

    string logoCadastroPacientes = @"
   ____          _           _                   _        ____            _            _            
  / ___|__ _  __| | __ _ ___| |_ _ __ ___     __| | ___  |  _ \ __ _  ___(_) ___ _ __ | |_ ___  ___ 
 | |   / _` |/ _` |/ _` / __| __| '__/ _ \   / _` |/ _ \ | |_) / _` |/ __| |/ _ \ '_ \| __/ _ \/ __|
 | |__| (_| | (_| | (_| \__ \ |_| | | (_) | | (_| |  __/ |  __/ (_| | (__| |  __/ | | | ||  __/\__ \
  \____\__,_|\__,_|\__,_|___/\__|_|  \___/   \__,_|\___| |_|   \__,_|\___|_|\___|_| |_|\__\___||___/
                                                                                                    
";

    string agenda = @"
     _                        _       
    / \   __ _  ___ _ __   __| | __ _ 
   / _ \ / _` |/ _ \ '_ \ / _` |/ _` |
  / ___ \ (_| |  __/ | | | (_| | (_| |
 /_/   \_\__, |\___|_| |_|\__,_|\__,_|
         |___/                        
";
}
