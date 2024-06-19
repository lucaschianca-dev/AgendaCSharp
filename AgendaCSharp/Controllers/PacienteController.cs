using AgendaCSharp.Services;
using AgendaCSharp.Views;
using Colorful;
using Console = Colorful.Console;
using System.Drawing;

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

    public void ListarPadicentesByNome()
    {
        var pacientes = _pacienteService.BuscarTodosPacientesByNome();
        _pacienteView.ExibirPacientes(pacientes);
    }

    public void ListarPadicentesByCpf()
    {
        var pacientes = _pacienteService.BuscarTodosPacientesPorCpf();
        _pacienteView.ExibirPacientes(pacientes);
    }

    public void CadastrarPaciente()
    {
        var pacienteDto = _pacienteView.CapturarDados();

        try
        {
            _pacienteService.AdicionarPaciente(pacienteDto);
            _pacienteView.MensagemSucessoCadastro();

            while (true)
            {
                _pacienteView.MensagemCadastrarOutroPaciente();

                string condicao = Console.ReadLine().ToLower();
                if (condicao == "y")
                {
                    CadastrarPaciente();
                    Console.WriteLine("");
                    break;
                }
                else if (condicao == "n")
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    _pacienteView.MensagemOpcaoInvalida();
                }
            }
        }
        catch (InvalidOperationException ex)
        {
            _pacienteView.ExibirMensagem(ex.Message);
        }
        catch (Exception ex)
        {
            _pacienteView.ExibirMensagemVermelho($"Erro: {ex.Message} \n");
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
                _pacienteView.MensagemRemoverPaciente();
                pacienteRemovido = true;
            }
            catch (InvalidOperationException ex)
            {
                bool entradaValidaOperation = false;
                while (!entradaValidaOperation)
                {
                    _pacienteView.ExibirMensagemVermelho($"\n[ERRO] {ex.Message} \n");
                    _pacienteView.MensagemTentarNovamente();

                    string condicao = Console.ReadLine().ToLower();
                    switch (condicao)
                    {
                        case "y":
                            entradaValidaOperation = true;
                            break;
                        case "n":
                            pacienteRemovido = true;
                            entradaValidaOperation = true;
                            break;
                        default:
                            _pacienteView.MensagemOpcaoInvalida();
                            break;
                    }
                }
            }
            catch (ArgumentException ex)
            {
                bool entradaValidaArgument = false;
                while (!entradaValidaArgument)
                {
                    _pacienteView.ExibirMensagemVermelho($"\n[ERRO] {ex.Message} \n");
                    _pacienteView.MensagemTentarNovamente();


                    string condicao = Console.ReadLine().ToLower();
                    switch (condicao)
                    {
                        case "y":
                            entradaValidaArgument = true;
                            break;
                        case "n":
                            pacienteRemovido = true;
                            entradaValidaArgument = true;
                            break;
                        default:
                            _pacienteView.MensagemOpcaoInvalida();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                _pacienteView.ExibirMensagemVermelho($"\n[ERRO] {ex.Message} \n");
                _pacienteView.MensagemTentarNovamente();

                string condicao = Console.ReadLine().ToLower();
                if (condicao == "n")
                {
                    pacienteRemovido = true;
                }
            }
        }
    }
}
