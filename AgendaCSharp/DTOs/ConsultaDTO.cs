namespace AgendaCSharp.DTOs;

public class ConsultaDTO
{
    public DateTime Data { get; set; }
    public TimeSpan HoraInicial { get; set; }
    public TimeSpan HoraFinal { get; set; }
}