using AgendaCSharp.DTOs;
using AgendaCSharp.Mappers;
using AgendaCSharp.Models;
using AgendaCSharp.Repositories;
using AgendaCSharp.Verificadores;
using AgendaCSharp.Views;

namespace AgendaCSharp.Services;

public class PacienteService
{
    private readonly PacienteRepository _pacienteRepository;
    private readonly ConsultaRepository _consultaRepository;
    private readonly PacienteView _pacienteView;

    public PacienteService(PacienteRepository pacienteRepository, ConsultaRepository consultaRepository, PacienteView pacienteView)
    {
        _pacienteRepository = pacienteRepository;
        _consultaRepository = consultaRepository;
        _pacienteView = pacienteView;
    }

    public void AdicionarPaciente(PacienteDTO pacienteDto)
    {

        if (!ValidarPaciente(pacienteDto))
        {
            pacienteDto = _pacienteView.CapturarDados();
            AdicionarPaciente(pacienteDto);
        }
        else
        {
            var paciente = PacienteMapper.ToEntidade(pacienteDto);
            _pacienteRepository.AdicionarPaciente(paciente);
        }
    }

    public List<PacienteDTO> BuscarTodosPacientes()
    {
        return _pacienteRepository.BuscarTodosPacientes()
            .Select(PacienteMapper.ToDTO)
            .ToList();
    }

    public List<PacienteDTO> BuscarTodosPacientesPorCpf()
    {
        return _pacienteRepository.BuscarTodosPacientesPorCpf()
            .Select(PacienteMapper.ToDTO)
            .ToList();
    }

    public List<PacienteDTO> BuscarTodosPacientesByNome()
    {
        return _pacienteRepository.BuscarTodosPacientesPorNome()
            .Select(PacienteMapper.ToDTO)
            .ToList();
    }

    public void RemoverPacienteByCpf(string cpf)
    {
        Paciente paciente = _pacienteRepository.BuscarPacienteByCpf(cpf);

        if (string.IsNullOrEmpty(cpf))
        {
            throw new ArgumentException("CPF não pode ser nulo ou vazio.\n");
        }

        if (paciente == null)
        {
            throw new InvalidOperationException($"Paciente com CPF {cpf} não encontrado.\n");
        }

        var consultasFuturas = paciente.Consultas
                .Where(c => c.Data >= DateTime.Now)
                .ToList();

        if (consultasFuturas.Any())
        {
            throw new InvalidOperationException($"O paciente com CPF {cpf} possui consultas futuras agendadas.\n");
        }
        _pacienteRepository.RemoverPacienteByCpf(cpf);
    }

    public bool VerificarCpf(string cpf)
    {
        return _pacienteRepository.BuscarPacienteByCpf(cpf) != null;
    }

    public void AdicionarConsulta(string cpf, Consulta consulta)
    {
        _consultaRepository.AdicionarConsulta(cpf, consulta);
    }

    private bool ValidarPaciente(PacienteDTO pacienteDto)
    {
        if (string.IsNullOrEmpty(pacienteDto.Cpf))
        {
            _pacienteView.ExibirErro("ERRO", " CPF não pode ser nulo ou vazio.");
            return false;
        }

        if (pacienteDto.Cpf.Length != 11)
        {
            _pacienteView.ExibirErro("ERRO", " CPF deve conter 11 dígitos.");
            return false;
        }

        if (!IsNumerico.isAllDigits(pacienteDto.Cpf))
        {
            _pacienteView.ExibirErro("ERRO", " CPF deve conter apenas números.");
            return false;
        }

        if (_pacienteRepository.BuscarPacienteByCpf(pacienteDto.Cpf) != null)
        {
            _pacienteView.ExibirErro("ERRO", $" CPF {pacienteDto.Cpf} já está cadastrado.");
            return false;
        }

        if (string.IsNullOrEmpty(pacienteDto.Nome))
        {
            _pacienteView.ExibirErro("ERRO", " Nome não pode ser nulo ou vazio.");
            return false;
        }

        if (pacienteDto.Nome.Length < 5 || pacienteDto.Nome.Length > 32)
        {
            _pacienteView.ExibirErro("ERRO", "Nome deve conter entre 5 e 32 caracteres.");
            return false;
        }

        int idade = VerificaDataDeNascimento.CalcularIdade(pacienteDto.DataDeNascimento);
        if (idade < 13)
        {
            _pacienteView.ExibirErro("ERRO", "A idade mínima para cadastro é 13 anos.");
            return false;
        }

        return true;
    }
}