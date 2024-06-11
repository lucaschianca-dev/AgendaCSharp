using AgendaCSharp.Controllers;
using AgendaCSharp.Repositories;
using AgendaCSharp.Services;
using AgendaCSharp.Verificadores;
using AgendaCSharp.Views;

class Program
{
    public static void Main(string[] args)
    {
        IsNumerico isNumerico = new IsNumerico();

        PacienteRepository pacienteRepository = new PacienteRepository();
        PacienteService pacienteService = new PacienteService(pacienteRepository);
        PacienteView pacienteView = new PacienteView(isNumerico, pacienteService);
        PacienteController pacienteController = new PacienteController(pacienteService, pacienteView);

        var consultaRepository = new ConsultaRepository();
        var consultaService = new ConsultaService(consultaRepository);
        var consultaView = new ConsultaView();
        var consultaController = new ConsultaController(consultaService, consultaView);

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
                    consultaController.CadastraConsulta();
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