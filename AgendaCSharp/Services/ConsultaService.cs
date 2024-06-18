using AgendaCSharp.DTOs;
using AgendaCSharp.Mappers;
using AgendaCSharp.Models;
using AgendaCSharp.Repositories;
using AgendaCSharp.Views;

namespace AgendaCSharp.Services;

public class ConsultaService
{
    private readonly ConsultaRepository _consultaRepository;
    private readonly PacienteRepository _pacienteRepository;
    private readonly ConsultaView _consultaView;

    public ConsultaService(ConsultaRepository consultaRepository, PacienteRepository pacienteRepository, ConsultaView consultaView)
    {
        _consultaRepository = consultaRepository;
        _pacienteRepository = pacienteRepository;
        _consultaView = consultaView;
    }

    public bool AdicionarConsulta(string cpf, ConsultaDTO consultaDto)
    {
        if (string.IsNullOrEmpty(cpf))
        {
            _consultaView.ExibirMensagemErro("[ERRO] CPF não pode ser nulo ou vazio.\n");
            return false;
        }

        var paciente = _pacienteRepository.BuscarPacienteByCpf(cpf);

        if (paciente == null)
        {
            _consultaView.ExibirMensagemErro($"[ERRO] Paciente com o CPF {cpf} não encontrado!\n");
            return false;
        }

        var consulta = ConsultaMapper.ToEntity(consultaDto);

        if (consulta.Data <= DateTime.Now.Date && consulta.HoraInicial <= DateTime.Now.TimeOfDay)
        {
            _consultaView.ExibirMensagemErro("[ERRO] A consulta deve ser agendada para uma data e hora futura.\n");
            return false;
        }

        if (consulta.HoraInicial < new TimeSpan(8, 0, 0) || consulta.HoraInicial > new TimeSpan(19, 0, 0) ||
            consulta.HoraFinal <= consulta.HoraInicial || consulta.HoraFinal > new TimeSpan(19, 0, 0))
        {
            _consultaView.ExibirMensagemErro("[ERRO] Horário da consulta deve ser entre 08:00 e 19:00 e a hora final deve ser após a hora inicial.\n");
            return false;
        }

        if (paciente.Consultas.Any(c => c.Data == consulta.Data &&
            ((consulta.HoraInicial >= c.HoraInicial && consulta.HoraInicial < c.HoraFinal) ||
            (consulta.HoraFinal > c.HoraInicial && consulta.HoraFinal <= c.HoraFinal))))
        {
            _consultaView.ExibirMensagemErro("[ERRO] Horário de consulta sobreposto com uma consulta existente.\n");
            return false;
        }

        _consultaRepository.AdicionarConsulta(cpf, consulta);
        return true;
    }

    public List<ConsultaDTO> BuscarTodasConsultas()
    {
        return _consultaRepository.BuscarTodasConsultas()
            .Select(ConsultaMapper.ToDTO)
            .ToList();
    }

    public List<ConsultaDTO> BuscarConsultasByCpf(string cpf)
    {
        var paciente = _pacienteRepository.BuscarPacienteByCpf(cpf);
        if (paciente == null)
        {
            _consultaView.ExibirMensagemErro($"[ERRO] Paciente com o CPF {cpf} não encontrado!\n");
        }
        return _consultaRepository.BuscarConsultasByCpf(cpf)
            .Select(ConsultaMapper.ToDTO)
            .ToList();
    }

    public bool RemoverConsultaByCpf(string cpf)
    {
        if (string.IsNullOrEmpty(cpf))
        {
            _consultaView.ExibirMensagemErro("\n[ERRO] CPF não pode ser nulo ou vazio.\n");
            return false;
        }

        var paciente = _pacienteRepository.BuscarPacienteByCpf(cpf);
        if (paciente == null)
        {
            _consultaView.ExibirMensagemErro($"\nPaciente com o CPF {cpf} não encontrado.\n");
            return false;
        }

        _consultaRepository.RemoverConsultasByCpf(cpf);
        return true;
    }
}
