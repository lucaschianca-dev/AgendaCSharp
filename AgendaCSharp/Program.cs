using AgendaCSharp.Controllers;
using AgendaCSharp.Repositories;
using AgendaCSharp.Services;
using AgendaCSharp.Views;

class Program
{
    public static void Main(string[] args)
    {
        PacienteRepository pacienteRepository = new PacienteRepository();
        PacienteService pacienteService = new PacienteService(pacienteRepository);
        PacienteView pacienteView = new PacienteView();
        PacienteController pacienteController = new PacienteController(pacienteService, pacienteView);

        bool sair = false;
        while (!sair)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Cadastrar Paciente");
            Console.WriteLine("2. Listar Pacientes");
            Console.WriteLine("3. Remover Paciente");
            Console.WriteLine("4. Sair");
            Console.Write("Escolha uma opção: ");
            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    pacienteController.CadastraPaciente();
                    break;
                case "2":
                    pacienteController.ListarPadicentes();
                    break;
                case "3":
                    sair = true;
                    break;
                default:
                    Console.WriteLine("Opção inválida, tente novamente.");
                    break;
            }
        }
    }
}