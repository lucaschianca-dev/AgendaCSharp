using AgendaCSharp.Services;
using AgendaCSharp.Views;

namespace AgendaCSharp.Controllers;

public class ConsultaController
{
    private ConsultaService _consultaService;
    private ConsultaView _consultaView;

    public ConsultaController(ConsultaService consultaService, ConsultaView consultaView)
    {
        _consultaService = consultaService;
        _consultaView = consultaView;
    }

    public void CadastraConsulta()
    {
        var consulta = _consultaView.CapturarDadosConsulta();
        _consultaService.AdicionarConsulta(consulta);
    }

    public void ListarTodasConsultas()
    {
        var consulta = _consultaService.BuscarTodasConsultas();
        _consultaView.exibirConsultas(consulta);
    }
}
