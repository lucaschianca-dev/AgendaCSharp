using AgendaCSharp.Services;
using AgendaCSharp.Views;

namespace AgendaCSharp.Controllers;

public class ConsultaController
{
    private readonly ConsultaService _consultaService;
    private readonly PacienteService _pacienteService;
    private readonly ConsultaView _consultaView;

    public ConsultaController(ConsultaService consultaService, PacienteService pacienteService, ConsultaView consultaView)
    {
        _consultaService = consultaService;
        _pacienteService = pacienteService;
        _consultaView = consultaView;
    }
    public void CadastrarConsulta()
    {
        string cpf;
        while (true)
        {
            cpf = _consultaView.CapturarCpf();
            if (_pacienteService.VerificarCpf(cpf))
            {
                _consultaView.ExibirMensagemVerde("Sucesso!\n");
                break;
            }
            else
            {
                _consultaView.ExibirMensagemErro("ERRO", $" - Paciente com CPF {cpf} não encontrado.\n");
                _consultaView.ExibirMensagem("\nDeseja tentar novamente? (");
                _consultaView.ExibirMensagemAqua("y");
                _consultaView.ExibirMensagem("/");
                _consultaView.ExibirMensagemAqua("n");
                _consultaView.ExibirMensagem(")\n");

                string opcao = Console.ReadLine().ToLower();
                if (opcao == "n")
                {
                    _consultaView.ExibirMensagemAqua("\n ► Operação de agendamento de consulta cancelada.\n");
                    return;
                }
            }
        }

        try
        {
            var consulta = _consultaView.CapturarDadosConsulta();
            _consultaService.AdicionarConsulta(cpf, consulta);
            _consultaView.ExibirMensagemVerde("\nConsulta cadastrada com sucesso!\n");
        }
        catch (InvalidOperationException ex)
        {
            _consultaView.ExibirMensagem($"\n{ex.Message}\n");
        }
        catch (Exception ex)
        {
            _consultaView.ExibirMensagem($"\n{ex.Message}\n");
        }
    }


    public void ListarTodasConsultas()
    {
        var consulta = _consultaService.BuscarTodasConsultas();
        _consultaView.ExibirConsultas(consulta);
    }

    public void ListarConsultaByCpf()
    {
        bool cpfValidado = false;
        while (!cpfValidado)
        {
            try
            {
                var cpf = Console.ReadLine();
                if (!string.IsNullOrEmpty(cpf))
                {
                    var consulta = _consultaService.BuscarConsultasByCpf(cpf);
                    Console.Clear();
                    _consultaView.ExibirConsultas(consulta);
                    cpfValidado = true;
                }
                else
                {
                    _consultaView.ExibirMensagemErro("ERRO", "CPF não pode ser nulo!\n");
                    _consultaView.ExibirMensagemSimboloAqua("\n ▬ ", "Digite o CPF do Paciente: ");
                }
            }
            catch (InvalidOperationException ex)
            {
                _consultaView.ExibirMensagem($"\n|");
                _consultaView.ExibirMensagemVermelho("ERRO");
                _consultaView.ExibirMensagem($"| - {ex.Message}\n");
                _consultaView.ExibirMensagem("\nDeseja tentar novamente? (");
                _consultaView.ExibirMensagemAqua("y");
                _consultaView.ExibirMensagem("/");
                _consultaView.ExibirMensagemAqua("n");
                _consultaView.ExibirMensagem(")\n");
                var resposta = Console.ReadLine();
                switch (resposta)
                {
                    case "y":
                        _consultaView.ExibirMensagemSimboloAqua("\n ▬ ", "Digite o CPF do Paciente: ");
                        break;
                    case "n":
                        Console.Clear();
                        cpfValidado = false;
                        return;
                    default:
                        _consultaView.ExibirMensagemErro("Inválido", "Deve ser digitado apenas 'y' ou 'n'!\n");
                        cpfValidado= true;
                        break;
                }
            }
        }
    }
}
