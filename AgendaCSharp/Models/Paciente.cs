namespace AgendaCSharp.Models;

public class Paciente
{
    private char[] _cpf = new char[11];
    private string _nome;
    private DateTime _dataDeNascimento;
    private int _idade;
    public List<Consulta> Consultas { get; set; }

    public Paciente(string cpf, string nome, DateTime dataDeNascimento, int idade)
    {
        Cpf = cpf;
        _nome = nome;
        _dataDeNascimento = dataDeNascimento;
        _idade = idade;
        Consultas = new List<Consulta>();
    }

    public string Cpf
    {
        get { return new string(_cpf); }
        set { _cpf = value.ToCharArray(); }
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

    public int Idade
    {
        get { return _idade; }
        set { _idade = value; }
    }
}