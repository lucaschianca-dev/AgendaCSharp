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
                _consultaView.ExibirMensagem("Sucesso!");
                break;
            }
            else
            {
                _consultaView.ExibirMensagem($"\n|ERRO| - Paciente com CPF {cpf} não encontrado.\n");
                _consultaView.ExibirMensagem("Deseja tentar novamente? (y/n)");

                string opcao = Console.ReadLine().ToLower();
                if (opcao == "n")
                {
                    _consultaView.ExibirMensagem("\nOperação de agendamento de consulta cancelada.\n");
                    return;
                }
            }
        }

        try
        {
            var consulta = _consultaView.CapturarDadosConsulta();
            _consultaService.AdicionarConsulta(cpf, consulta);
            _consultaView.ExibirMensagem("\nConsulta cadastrada com sucesso!\n");
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
}
