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
        var pacientes = _pacienteService.ListarPacientes();
        _pacienteView.ExibirPacientes(pacientes);
    }

    public void CadastraPaciente()
    {
        var paciente = _pacienteView.CapturarDados();
        _pacienteService.AdicionarPaciente(paciente);
        _pacienteView.ExibirMensagem("Paciente cadastrado com sucesso!");
    }
}
