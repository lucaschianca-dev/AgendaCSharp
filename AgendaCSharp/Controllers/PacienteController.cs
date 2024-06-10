using AgendaCSharp.Services;
using AgendaCSharp.Views;

namespace AgendaCSharp.Controllers;

public class PacienteController
{
    private readonly PacienteService _pacienteService;
    private readonly PacienteView _pacienteView;

    public PacienteController(PacienteService pacienteService, PacienteView pacienteView)
    {
        _pacienteService = pacienteService;
        _pacienteView = pacienteView;
    }

    public void ListarPadicentes()
    {
        var pacientes = _pacienteService.BuscarPacientes();
        Console.WriteLine("");
        _pacienteView.ExibirPacientes(pacientes);
        Console.WriteLine("");
    }

    public void CadastrarPaciente()
    {
        var paciente = _pacienteView.CapturarDados();
        _pacienteService.AdicionarPaciente(paciente);
        _pacienteView.ExibirMensagem("\n----------------------------------");
        _pacienteView.ExibirMensagem(" Paciente cadastrado com sucesso!");
        _pacienteView.ExibirMensagem("----------------------------------\n");

        while (true)
        {
            _pacienteView.ExibirMensagem("Deseja cadastrar outro paciente? (y / n)");
            string condicao = Console.ReadLine().ToLower();
            if (condicao == "y")
            {
                CadastrarPaciente();
                break;
            }
            else if (condicao == "n")
            {
                break;
            }
            else
            {
                _pacienteView.ExibirMensagem("\nOpção inválida. Por favor, digite 'y' para sim ou 'n' para não.\n");
            }
        }
    }

    public void ExcluirPacienteByCpf()
    {
        try
        {
            var cpf = _pacienteView.CapturarCpfParaRemocao();
            _pacienteService.RemoverPacienteByCpf(cpf);
            _pacienteView.ExibirMensagem("Paciente removido com sucesso!");
        }
        catch (InvalidOperationException ex)
        {
            _pacienteView.ExibirMensagem(ex.Message);
        }
        catch (Exception ex)
        {
            _pacienteView.ExibirMensagem($"Erro: {ex.Message}");
        }
    }
}
