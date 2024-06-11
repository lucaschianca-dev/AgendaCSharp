namespace AgendaCSharp.Models;

public class Consulta
{
    public string CpfPaciente { get; set; }
    public DateTime Data { get; set; }
    public TimeSpan HoraInicial { get; set; }
    public TimeSpan HoraFinal { get; set; }

    public Consulta(string cpfPaciente, DateTime data, TimeSpan horaInicial, TimeSpan horaFinal)
    {
        CpfPaciente = cpfPaciente;
        Data = data;
        HoraInicial = horaInicial;
        HoraFinal = horaFinal;
    }
}