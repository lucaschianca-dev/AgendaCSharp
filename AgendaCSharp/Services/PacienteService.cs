﻿using AgendaCSharp.Models;
using AgendaCSharp.Repositories;

namespace AgendaCSharp.Services;

public class PacienteService
{
    private readonly PacienteRepository _pacienteRepository;
    private readonly ConsultaRepository _consultaRepository;

    public PacienteService(PacienteRepository pacienteRepository, ConsultaRepository consultaRepository)
    {
        _pacienteRepository = pacienteRepository;
        _consultaRepository = consultaRepository;
    }

    public void AdicionarPaciente(Paciente paciente)
    {
        var pacienteExistente = _pacienteRepository.BuscarPacienteByCpf(paciente.Cpf);
        if (pacienteExistente != null)
        {
            throw new InvalidOperationException($"|ERRO| - CPF {paciente.Cpf} já está cadastrado!");
        }

        _pacienteRepository.AdicionarPaciente(paciente);
    }

    public List<Paciente> BuscarPacientes()
    {
        return _pacienteRepository.BuscarPacientes();
    }

    public void RemoverPacienteByCpf(string cpf)
    {
        //if (string.IsNullOrWhiteSpace(cpf))
        //{
        //    throw new ArgumentException("O CPF é obrigatório!");
        //}

        //_pacienteRepository.RemoverPacienteByCpf(cpf);

        Paciente paciente = _pacienteRepository.BuscarPacienteByCpf(cpf);

        if (paciente == null)
        {
            throw new InvalidOperationException($"|ERRO| - Paciente com CPF {cpf} não encontrado.");
        }

        var consultasFuturas = paciente.Consultas
                .Where(c => c.Data >= DateTime.Now)
                .ToList();

        if (consultasFuturas.Any())
        {
            throw new InvalidOperationException($"|ERRO| - O paciente com CPF {cpf} possui consultas futuras agendadas.");
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

    public List<Consulta> BuscarTodasConsultas()
    {
        return _consultaRepository.BuscarTodasConsultas();
    }
}