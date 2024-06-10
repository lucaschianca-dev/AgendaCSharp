using AgendaCSharp.Controllers;
using AgendaCSharp.Repositories;
using AgendaCSharp.Services;
using AgendaCSharp.Verificadores;
using AgendaCSharp.Views;

class Program
{
    public static void Main(string[] args)
    {
        PacienteRepository pacienteRepository = new PacienteRepository();
        PacienteService pacienteService = new PacienteService(pacienteRepository);
        IsNumerico isNumerico = new IsNumerico();
        PacienteView pacienteView = new PacienteView(isNumerico);
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
                    pacienteController.CadastrarPaciente();
                    break;
                case "2":
                    pacienteController.ListarPadicentes();
                    break;
                case "3":
                    pacienteController.ExcluirPacienteByCpf();
                    break;
                case "4":
                    sair = true;
                    break;
                default:
                    Console.WriteLine("Opção inválida, tente novamente.");
                    break;
            }
        }
    }
}