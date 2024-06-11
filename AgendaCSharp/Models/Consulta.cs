namespace AgendaCSharp.Models;

public class Consulta
{
    public DateTime Data { get; set; }
    public TimeSpan HoraInicial { get; set; }
    public TimeSpan HoraFinal { get; set; }

    public Consulta(DateTime data, TimeSpan horaInicial, TimeSpan horaFinal)
    {
        Data = data;
        HoraInicial = horaInicial;
        HoraFinal = horaFinal;
    }
}