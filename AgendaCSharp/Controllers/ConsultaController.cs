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
                _consultaView.ExibirMensagemErro($"[ERRO] Paciente com CPF {cpf} não encontrado.\n");
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

        var consultaDto = _consultaView.CapturarDadosConsulta();
        if (_consultaService.AdicionarConsulta(cpf, consultaDto))
        {
            _consultaView.ExibirMensagemVerde("\nConsulta cadastrada com sucesso!\n");
        }
    }


    public void ListarTodasConsultas()
    {
        var consulta = _consultaService.BuscarTodasConsultas();
        _consultaView.ExibirConsultas(consulta);
    }

    public void ListarConsultasByCpf()
    {
        bool cpfValidado = false;
        while (!cpfValidado)
        {
            try
            {
                var cpf = _consultaView.CapturarCpf();
                if (!string.IsNullOrEmpty(cpf))
                {
                    var consultas = _consultaService.BuscarConsultasByCpf(cpf);
                    Console.Clear();
                    _consultaView.ExibirConsultas(consultas);
                    cpfValidado = true;
                }
                else
                {
                    _consultaView.ExibirMensagemErro("[ERRO] CPF não pode ser nulo!\n");
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
                        _consultaView.ExibirMensagemErro("Inválido! Deve ser digitado apenas 'y' ou 'n'!\n");
                        cpfValidado= true;
                        break;
                }
            }
        }
    }

    public void ExcluirConsulta()
    {
        bool consultaRemovida = false;
        while (!consultaRemovida)
        {
            string cpf = _consultaView.CapturarCpfParaRemocao();
            if (_consultaService.RemoverConsultaByCpf(cpf))
            {
                _consultaView.ExibirMensagemVerde("\nConsulta removida com sucesso!\n");
                consultaRemovida = true;
            }
            consultaRemovida = PerguntarSeDesejaTentarNovamente();
        }
    }

    private bool PerguntarSeDesejaTentarNovamente()
    {
        _consultaView.ExibirMensagemAqua("\n► ");
        _consultaView.ExibirMensagem("Deseja tentar novamente? (");
        _consultaView.ExibirMensagemAqua("y");
        _consultaView.ExibirMensagem("/");
        _consultaView.ExibirMensagemAqua("n");
        _consultaView.ExibirMensagem(")\n");

        string condicao = Console.ReadLine().ToLower();
        while (condicao != "y" && condicao != "n")
        {
            _consultaView.ExibirMensagemVermelho(" \n▲ Opção inválida. ");
            _consultaView.ExibirMensagem("Por favor, digite '");
            _consultaView.ExibirMensagemAqua("y");
            _consultaView.ExibirMensagem("' para sim ou '");
            _consultaView.ExibirMensagemAqua("n");
            _consultaView.ExibirMensagem("' para não.\n");
            condicao = Console.ReadLine().ToLower();
        }
        return condicao == "n";
    }
}
