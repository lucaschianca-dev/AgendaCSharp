namespace AgendaCSharp.Models;

public class Paciente
{
    public string Cpf { get; set; }
    public string Nome { get; set; }
    public DateTime DataDeNascimento { get; set; }
    public int Idade => DateTime.Now.Year - DataDeNascimento.Year;
    public List<Consulta> Consultas { get; set; }

    public Paciente(string cpf, string nome, DateTime dataDeNascimento)
    {
        Cpf = cpf;
        Nome = nome;
        DataDeNascimento = dataDeNascimento;
        Consultas = new List<Consulta>();
    }
}