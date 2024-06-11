using AgendaCSharp.Models;

namespace AgendaCSharp.Repositories;

public class ConsultaRepository
{
    private List<Consulta> _consultas = new List<Consulta>();

    public void AdicionarConsulta(Consulta consulta)
    {
        _consultas.Add(consulta);
    }

    public List<Consulta> BuscarConsultas(Consulta consulta)
    {
        return _consultas;
    }
}
