namespace AgendaCSharp.DTOs;

public class PacienteDTO
{
    public string Cpf { get; set; }
    public string Nome { get; set; }
    public DateTime DataDeNascimento { get; set; }
    public int Idade { get; set; }
    public List<ConsultaDTO> Consultas { get; set; }
}