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

        try
        {
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
                    Console.WriteLine("");
                    break;
                }
                else if (condicao == "n")
                {
                    Console.WriteLine("");
                    break;
                }
                else
                {
                    _pacienteView.ExibirMensagem("\nOpção inválida. Por favor, digite 'y' para sim ou 'n' para não.\n");
                }
            }
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

    public void ExcluirPacienteByCpf()
    {
        bool pacienteRemovido = false;

        while (!pacienteRemovido)
        {
            try
            {
                var cpf = _pacienteView.CapturarCpfParaRemocao();
                _pacienteService.RemoverPacienteByCpf(cpf);
                _pacienteView.ExibirMensagem("Paciente removido com sucesso!");
                pacienteRemovido = true;
            }
            catch (InvalidOperationException ex)
            {
                _pacienteView.ExibirMensagem(ex.Message);
                _pacienteView.ExibirMensagem("Deseja tentar novamente? (y/n)");

                string condicao = Console.ReadLine().ToLower();
                if (condicao == "n")
                {
                    _pacienteView.ExibirMensagem("");
                    pacienteRemovido = true;
                }
            }
            catch (Exception ex)
            {
                _pacienteView.ExibirMensagem($"Erro: {ex.Message}");
                _pacienteView.ExibirMensagem("Deseja tentar novamente? (y/n)");

                string condicao = Console.ReadLine().ToLower();
                if (condicao == "n")
                {
                    _pacienteView.ExibirMensagem("");
                    pacienteRemovido = true;
                }
            }
        }
    }
}
