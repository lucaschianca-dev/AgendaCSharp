namespace AgendaCSharp.Models;

public class Paciente
{
    private string _cpf;
    private string _nome;
    private DateTime _dataDeNascimento;

    public Paciente(string cpf, string nome, DateTime dataDeNascimento)
    {
        _cpf = cpf;
        _nome = nome;
        _dataDeNascimento = dataDeNascimento;
    }

    public string Cpf
    {
        get { return _cpf; }
        set { _cpf = value; }
    }

    public string Nome
    {
        get { return _nome; }
        set { _nome = value; }
    }

    public DateTime DataDeNascimento
    {
        get { return _dataDeNascimento; }
        set { _dataDeNascimento = value; }
    }
}