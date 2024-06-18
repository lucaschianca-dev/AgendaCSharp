using AgendaCSharp.Controllers;
using AgendaCSharp.Repositories;
using AgendaCSharp.Services;
using AgendaCSharp.Views;

class Program
{
    public static void Main(string[] args)
    {
        var pacienteRepository = new PacienteRepository();
        var consultaRepository = new ConsultaRepository(pacienteRepository);

        var pacienteView = new PacienteView();

        var pacienteService = new PacienteService(pacienteRepository, consultaRepository, pacienteView);

        pacienteView.SetPacienteService(pacienteService);

        var consultaView = new ConsultaView();

        var consultaService = new ConsultaService(consultaRepository, pacienteRepository, consultaView);

        var pacienteController = new PacienteController(pacienteService, pacienteView);
        var consultaController = new ConsultaController(consultaService, pacienteService, consultaView);

        var menuView = new MenuView();

        menuView.ConsoleTitulo();
        menuView.LogoInicial();
        Console.WriteLine("Bem-vindo(a) de volta!");
        Console.ReadKey(true);
        Console.Clear();
        menuView.ExibirMenu(pacienteController, consultaController);

    }
}