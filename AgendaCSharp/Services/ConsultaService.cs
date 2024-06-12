﻿using AgendaCSharp.Models;
using AgendaCSharp.Repositories;

namespace AgendaCSharp.Services;

public class ConsultaService
{
    private readonly ConsultaRepository _consultaRepository;
    private readonly PacienteRepository _pacienteRepository;

    public ConsultaService(ConsultaRepository consultaRepository, PacienteRepository pacienteRepository)
    {
        _consultaRepository = consultaRepository;
        _pacienteRepository = pacienteRepository;
    }

    public void AdicionarConsulta(string cpf, Consulta consulta)
    {
        var paciente = _pacienteRepository.BuscarPacienteByCpf(cpf);

        if (consulta.Data <= DateTime.Now.Date && consulta.HoraInicial <= DateTime.Now.TimeOfDay)
        {
            throw new InvalidOperationException("|ERRO| - A consulta deve ser agendada para uma data e hora futura.");
        }

        if (paciente.Consultas.Any(c => c.Data == consulta.Data &&
            ((consulta.HoraInicial >= c.HoraInicial && consulta.HoraInicial < c.HoraFinal) ||
            (consulta.HoraFinal > c.HoraInicial && consulta.HoraFinal <= c.HoraFinal))))
        {
            throw new InvalidOperationException("|ERRO| - Horário de consulta sobreposto com uma consulta existente.");
        }

        if (paciente.Consultas.Any(c => c.Data > DateTime.Now || (c.Data == DateTime.Now.Date && c.HoraInicial > DateTime.Now.TimeOfDay)))
        {
            throw new InvalidOperationException($"|ERRO| - Paciente com CPF {cpf} já possui uma consulta futura.");
        }

        if (paciente == null)
        {
            throw new InvalidOperationException($"|ERRO| - Paciente com CPF {cpf} não encontrado.");
        }

        _consultaRepository.AdicionarConsulta(cpf, consulta);
    }

    public List<Consulta> BuscarTodasConsultas()
    {
        return _consultaRepository.BuscarTodasConsultas();
    }

    public List<Consulta> BuscarConsultasByCpf(string cpf)
    {
        var paciente = _pacienteRepository.BuscarPacienteByCpf(cpf);
        if (paciente == null)
        {
            throw new InvalidOperationException($"Paciente com o CPF {cpf} não encontrado!");
        }
        return _consultaRepository.BuscarConsultasByCpf(cpf);
    }
}
