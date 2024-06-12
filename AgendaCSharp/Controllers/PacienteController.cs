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
        Console.WriteLine("");
        _pacienteView.ExibirPacientes(pacientes);
        Console.WriteLine("");
    }

    public void ListarPadicentesByCpf()
    {
        var pacientes = _pacienteService.BuscarTodosPacientesByCpf();
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
            _pacienteView.ExibirMensagemVerde("\n ♦ Paciente cadastrado com sucesso!\n");

            while (true)
            {
                _pacienteView.ExibirMensagemAqua("\n► ");
                _pacienteView.ExibirMensagem("Deseja cadastrar outro paciente? (");
                _pacienteView.ExibirMensagemAqua("y");
                _pacienteView.ExibirMensagem("/");
                _pacienteView.ExibirMensagemAqua("n");
                _pacienteView.ExibirMensagem(")\n");

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
                    _pacienteView.ExibirMensagemVermelho(" ▲ Opção inválida. ");
                    _pacienteView.ExibirMensagem("Por favor, digite '");
                    _pacienteView.ExibirMensagemAqua("y");
                    _pacienteView.ExibirMensagem("' para sim ou '");
                    _pacienteView.ExibirMensagemAqua("n");
                    _pacienteView.ExibirMensagem("' para não.\n");
                }
            }
        }
        catch (InvalidOperationException ex)
        {
            _pacienteView.ExibirMensagem(ex.Message);
        }
        catch (Exception ex)
        {
            _pacienteView.ExibirMensagemVermelho($"Erro: {ex.Message}");
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
                _pacienteView.ExibirMensagemVerde("\n ♦ Paciente removido com sucesso!\n");
                pacienteRemovido = true;
            }
            catch (InvalidOperationException ex)
            {
                bool entradaValidaOperation = false;
                while (!entradaValidaOperation)
                {
                    _pacienteView.ExibirMensagem($"\n|");
                    _pacienteView.ExibirMensagemAqua("ERRO");
                    _pacienteView.ExibirMensagem($"| - {ex.Message}\n");
                    _pacienteView.ExibirMensagemAqua("► ");
                    _pacienteView.ExibirMensagem("Deseja tentar novamente? (");
                    _pacienteView.ExibirMensagemAqua("y");
                    _pacienteView.ExibirMensagem("/");
                    _pacienteView.ExibirMensagemAqua("n");
                    _pacienteView.ExibirMensagem(")\n");

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
                            _pacienteView.ExibirMensagemVermelho(" ▲ Opção inválida. ");
                            _pacienteView.ExibirMensagem("Por favor, digite '");
                            _pacienteView.ExibirMensagemAqua("y");
                            _pacienteView.ExibirMensagem("' para sim ou '");
                            _pacienteView.ExibirMensagemAqua("n");
                            _pacienteView.ExibirMensagem("' para não.\n");
                            break;
                    }
                }
            }
            catch (ArgumentException ex)
            {
                bool entradaValidaArgument = false;
                while (!entradaValidaArgument)
                {
                    _pacienteView.ExibirMensagem($"\n|");
                    _pacienteView.ExibirMensagemAqua("ERRO");
                    _pacienteView.ExibirMensagem($"| - {ex.Message}\n");
                    _pacienteView.ExibirMensagemAqua("► ");
                    _pacienteView.ExibirMensagem("Deseja tentar novamente? (");
                    _pacienteView.ExibirMensagemAqua("y");
                    _pacienteView.ExibirMensagem("/");
                    _pacienteView.ExibirMensagemAqua("n");
                    _pacienteView.ExibirMensagem(")\n");

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
                            _pacienteView.ExibirMensagemVermelho(" ▲ Opção inválida. ");
                            _pacienteView.ExibirMensagem("Por favor, digite '");
                            _pacienteView.ExibirMensagemAqua("y");
                            _pacienteView.ExibirMensagem("' para sim ou '");
                            _pacienteView.ExibirMensagemAqua("n");
                            _pacienteView.ExibirMensagem("' para não.\n");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                _pacienteView.ExibirMensagem($"\n|");
                _pacienteView.ExibirMensagemAqua("ERRO");
                _pacienteView.ExibirMensagem($"| - {ex.Message}\n");
                _pacienteView.ExibirMensagem("Deseja tentar novamente? (y/n)");

                string condicao = Console.ReadLine().ToLower();
                if (condicao == "n")
                {
                    pacienteRemovido = true;
                }
            }
        }
    }
}
