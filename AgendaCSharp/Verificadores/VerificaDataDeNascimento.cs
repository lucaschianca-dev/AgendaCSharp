namespace AgendaCSharp.Verificadores;

public class VerificaDataDeNascimento
{
    public static int CalcularIdade(DateTime dataDeNascimento)
    {
        DateTime hoje = DateTime.Today;
        int idade = hoje.Year - dataDeNascimento.Year;

        return idade;
    }
}
